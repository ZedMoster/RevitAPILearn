using Autodesk.Revit.UI;

namespace CsharpDemo.Utils
{
    internal class XmlDoc
    {
        #region 饿汉式单例
        private XmlDoc() { }
        private static readonly XmlDoc Global = new XmlDoc();
        public static XmlDoc Instance => Global ?? new XmlDoc();

        #endregion

        /// <summary>
        /// uidoc
        /// </summary>
        public UIDocument UIdoc { get; set; }

        /// <summary>
        /// IExternalEventHandler
        /// </summary>
        public RevitTask Task { get; set; }
    }
}
