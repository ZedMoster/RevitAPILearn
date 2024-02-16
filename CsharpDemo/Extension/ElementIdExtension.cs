using Autodesk.Revit.DB;

namespace CsharpDemo.Extension
{
    public static class ElementIdExtension
    {
        /// <summary>
        /// id 2 Element
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static T ToElement<T>(this ElementId id, Document doc) where T : Element
        {
            return doc.GetElement(id) as T;
        }
    }
}
