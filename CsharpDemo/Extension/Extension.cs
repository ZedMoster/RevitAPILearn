using Autodesk.Revit.UI;
using CsharpDemo.Utils;
using System.Windows.Interop;
using System.Windows;

namespace CsharpDemo.Extension
{
    public static class Extension
    {
        /// <summary>
        /// init 初始化
        /// </summary>
        /// <param name="commandData"></param>
        public static UIDocument Init(this ExternalCommandData commandData)
        {
            XmlDoc.Instance.UIdoc = commandData.Application.ActiveUIDocument;
            XmlDoc.Instance.Task = new RevitTask();
            return XmlDoc.Instance.UIdoc;
        }

        /// <summary>
        /// 设置窗口显示在Revit上
        /// </summary>
        /// <param name="window"></param>
        public static void MainWindowHandle(this Window window)
        {
            new WindowInteropHelper(window).Owner = XmlDoc.Instance.UIdoc.Application.MainWindowHandle;
        }
    }
}
