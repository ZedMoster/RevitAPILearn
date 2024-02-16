using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;

namespace CsharpDemo.SelectionFilter
{
    internal class SelectionFilterBase : ISelectionFilter
    {
        private readonly Func<Element, bool> Func1;
        private readonly Func<Reference, bool> Func2;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="func1"></param>
        /// <param name="func2"></param>
        public SelectionFilterBase(Func<Element, bool> func1, Func<Reference, bool> func2)
        {
            Func1 = func1;
            Func2 = func2;
        }

        public bool AllowElement(Element elem)
        {
            return Func1(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return Func2(reference);
        }
    }
}
