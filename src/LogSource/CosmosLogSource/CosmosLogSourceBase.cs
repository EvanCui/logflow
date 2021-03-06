﻿using System.Diagnostics;

namespace LogFlow.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using LogFlow.DataModel.Algorithm;

    public abstract class CosmosLogSourceBase : LogSourceCompressedBase<CosmosDataItem>
    {
        protected CosmosLogSourceBase(LogSourceProperties properties) : base(properties) { }
        protected List<CosmosLogFileBase> LogFiles;

        public override string Name => this.LogFiles.Count == 1 ? this.LogFiles[0].FileName :
            (this.LogFiles.Count == 0 ? "No File Loaded" : $"{this.LogFiles[0].FileName} .. {this.LogFiles[this.LogFiles.Count - 1].FileName}");

        private bool isInProgress;
        private readonly CancellationTokenSource cts = new CancellationTokenSource();

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.cts?.Cancel();
                while (this.isInProgress) { Debug.WriteLine("SpinWait dispose!"); Thread.SpinWait(100); }
                Debug.WriteLine("CosmosLogSource disposed");
                this.cts?.Dispose();
                this.LogFiles?.ForEach(f => f?.Dispose());
                this.LogFiles = null;
            }
        }

        private IEnumerable<int> CancellableProgress(Func<CancellationToken, IEnumerable<int>> action,
            CancellationToken token)
        {
            var currentCts = CancellationTokenSource.CreateLinkedTokenSource(token, this.cts.Token);
            token = currentCts.Token;

            try
            {
                this.isInProgress = true;
                foreach (var p in action(token))
                {
                    yield return p;
                }
            }
            finally
            {
                this.isInProgress = false;
                currentCts.Dispose();
            }
        }

        public override IEnumerable<int> Peek(IFilter filter, int peekCount, CancellationToken token)
        {
            return this.CancellableProgress(t => this.PeekInternal(filter, peekCount, t), token);
        }

        public IEnumerable<int> PeekInternal(IFilter filter, int peekCount, CancellationToken token)
        {
            this.isInProgress = true;
            var lastReportedProgress = 0;
            yield return lastReportedProgress;
            lastReportedProgress += 20;

            int[] lastPercents = new int[this.LogFiles.Count];

            var merged = HeapMerger.Merge(
                token,
                Comparer<FullCosmosDataItem>.Create((d1, d2) => d1.Item.Time.CompareTo(d2.Item.Time)),
                this.LogFiles.Cast<IEnumerable<FullCosmosDataItem>>().ToArray());

            for (var i = 0; i < this.LogFiles.Count; i++)
            {
                var fileIndex = this.files.Put(this.LogFiles[i].FileName);
                this.FileMetaData.Put(new FileCompressMetaData());
                Debug.Assert(fileIndex == i, $"The file index {fileIndex} doesn't equal to i {i}");
            }

            var count = 0;

            foreach (var item in merged)
            {
                if (token.IsCancellationRequested) yield break;
                if (++count > peekCount && peekCount >= 0)
                {
                    yield break;
                }

                if (filter.Match(item.Item.Item, item.Item.Template))
                {
                    item.Item.Item.FileIndex = item.SourceIndex;
                    this.AddItem(item.Item);

                    yield break;
                }

                lastPercents[item.SourceIndex] = item.Item.Percent;

                var totalPercent = (int)lastPercents.Average();
                if (totalPercent < lastReportedProgress) continue;

                yield return lastReportedProgress;
                lastReportedProgress += 20;
            }
        }

        protected override IEnumerable<int> LoadFirst(IFilter filter, CancellationToken token)
        {
            return this.Load(filter, token);
        }

        protected override IEnumerable<int> LoadIncremental(IFilter filter, CancellationToken token)
        {
            return this.Load(filter, token);
        }

        public new IEnumerable<int> Load(IFilter filter, CancellationToken token)
        {
            return this.CancellableProgress(t => this.LoadInternal(filter, t), token);
        }

        private IEnumerable<int> LoadInternal(IFilter filter, CancellationToken token)
        {
            var lastReportedProgress = 0;
            yield return lastReportedProgress;

            if (this.LogFiles.Count == 0) { yield return 100; yield break; }

            // 1 file split to 5 steps. n file to 5 * n.
            var reportInterval = Math.Max(1, 100 / (this.LogFiles.Count * 5));
            var firstReportCount = 100;
            int count = 0;

            lastReportedProgress += reportInterval;

            int[] lastPercents = new int[this.LogFiles.Count];

            IEnumerable<MergedItem<FullCosmosDataItem>> merged;
            if (filter == null)
            {
                merged = HeapMerger.Merge(
                    token,
                    Comparer<FullCosmosDataItem>.Create((d1, d2) => d1.Item.Time.CompareTo(d2.Item.Time)),
                    this.LogFiles.Cast<IEnumerable<FullCosmosDataItem>>().ToArray());
            }
            else
            {
                merged = HeapMerger.Merge(
                    token,
                    Comparer<FullCosmosDataItem>.Create((d1, d2) => d1.Item.Time.CompareTo(d2.Item.Time)),
                    this.LogFiles.Select(f => f.TakeWhile(i => !token.IsCancellationRequested)
                        .Where(i => filter.Match(i.Item, i.Template))).ToArray());
            }

            for (var i = 0; i < this.LogFiles.Count; i++)
            {
                var fileIndex = this.files.Put(this.LogFiles[i].FileName);
                this.FileMetaData.Put(new FileCompressMetaData());
                Debug.Assert(fileIndex == i, $"The file index {fileIndex} doesn't equal to i {i}");
            }

            // each time we iterate through the merged, it will refresh all files underneath and load in new InternalItems;
            foreach (var item in merged)
            {
                if (token.IsCancellationRequested) yield break;

                count++;

                item.Item.Item.FileIndex = item.SourceIndex;
                this.AddItem(item.Item);

                lastPercents[item.SourceIndex] = item.Item.Percent;
                var totalPercent = (int)lastPercents.Average();
                if (totalPercent < lastReportedProgress && count != firstReportCount) continue;

                yield return lastReportedProgress;
                lastReportedProgress += reportInterval;
            }

            yield return 100;
        }
    }
}
