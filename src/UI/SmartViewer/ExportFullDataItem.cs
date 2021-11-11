using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFlow.Viewer
{
    internal class ExportFullDataItem
    {
        public string Template { get; set; }
        public string[] Parameters { get; set; }
        public DateTimeOffset Time { get; set; }
        public string FormattedText { get; set; }
    }
}
