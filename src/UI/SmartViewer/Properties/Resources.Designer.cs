﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogFlow.Viewer.Properties {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LogFlow.Viewer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   重写当前线程的 CurrentUICulture 属性，对
        ///   使用此强类型资源类的所有资源查找执行重写。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &apos;{0}&apos; is not a valid goto Id. 的本地化字符串。
        /// </summary>
        internal static string FailedToParseGotoId {
            get {
                return ResourceManager.GetString("FailedToParseGotoId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Didn&apos;t find the item Id {0} in the current view, try to switch to parent or root.  的本地化字符串。
        /// </summary>
        internal static string GotoFailed {
            get {
                return ResourceManager.GetString("GotoFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Hello, you will see a dialog box for filter/tag/search/count operations. This is to be implemented, if you want this feature, you can send feedback to us. 的本地化字符串。
        /// </summary>
        internal static string NotImplementedText {
            get {
                return ResourceManager.GetString("NotImplementedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 To be implemented 的本地化字符串。
        /// </summary>
        internal static string NotImplementedTitle {
            get {
                return ResourceManager.GetString("NotImplementedTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The search failed with an exception {0}. 的本地化字符串。
        /// </summary>
        internal static string SearchFailedText {
            get {
                return ResourceManager.GetString("SearchFailedText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Something went wrong 的本地化字符串。
        /// </summary>
        internal static string SomethingWrong {
            get {
                return ResourceManager.GetString("SomethingWrong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Wow~ You got so many log entries! (&gt;100,000,000) Please consider opening with filters, or doing a preloading search to narrow down the logs you want to focus on. We only show the first 100,000,000 entries. 的本地化字符串。
        /// </summary>
        internal static string TooManyRowsText {
            get {
                return ResourceManager.GetString("TooManyRowsText", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 Wow~ You got so many log entries! 的本地化字符串。
        /// </summary>
        internal static string TooManyRowsTitle {
            get {
                return ResourceManager.GetString("TooManyRowsTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 The version history file is stolen by someone. Please report! 的本地化字符串。
        /// </summary>
        internal static string VersionFileMissingText {
            get {
                return ResourceManager.GetString("VersionFileMissingText", resourceCulture);
            }
        }
    }
}
