using Autodesk.Revit.UI;
using System;
using System.Diagnostics;

namespace CsharpDemo.Utils
{
    internal class RevitTask
    {
        /// <summary>
        /// 外部事件接口封装类
        /// </summary>
        private ExternalEventHandler Handler { get; }

        /// <summary>
        /// 外部事件
        /// </summary>
        private ExternalEvent ExternalEvent { get; }

        /// <summary>
        /// 构造
        /// </summary>
        public RevitTask()
        {
            Handler = new ExternalEventHandler();
            ExternalEvent = ExternalEvent.Create(Handler);
        }

        /// <summary>
        /// 外部事件实现
        /// </summary>
        /// <param name="action"></param>
        public void Run(Action<UIApplication> action)
        {
            Handler.Action = action;
            ExternalEvent.Raise();
        }

        #region IExternalEventHandler
        internal class ExternalEventHandler : IExternalEventHandler
        {
            /// <summary>
            /// 委托方法
            /// </summary>
            public Action<UIApplication> Action { get; set; }

            /// <summary>
            /// execute
            /// </summary>
            /// <param name="app"></param>
            public void Execute(UIApplication app)
            {
                try
                {
                    Action(app);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message, ex.StackTrace, ex.Source);
                }
            }

            public string GetName()
            {
                return "Revit Task";
            }
        }
        #endregion

    }
}
