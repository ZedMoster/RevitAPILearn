using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CsharpDemo.SelectionFilter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CsharpDemo.Extension
{
    public static class DocumentExtension
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<T> OfClass<T>(this Document doc) where T : Element
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<T> OfClass<T>(this Document doc, BuiltInCategory builtInCategory) where T : Element
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).OfCategory(builtInCategory).Cast<T>().ToList();
        }

        /// <summary>
        /// 获取视图可见实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<T> OfClass<T>(this Document doc, View view) where T : Element
        {
            return new FilteredElementCollector(doc, view.Id).OfClass(typeof(T)).Cast<T>().ToList();
        }

        /// <summary>
        /// 事务
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="action"></param>
        /// <param name="name"></param>
        public static void Transaction(this Document doc, Action<Transaction> action, string name = null)
        {
            using (var t = new Transaction(doc, name ?? System.Guid.NewGuid().ToString()))
            {
                try
                {
                    t.Start();
                    action(t);
                    if (t.HasStarted() && t.GetStatus() == TransactionStatus.Started)
                    {
                        t.Commit();
                    }
                }
                catch (System.Exception ex)
                {
                    t.RollBack();
                    TaskDialog.Show("异常", $"事务运行异常:{ex.Message}");
                }
            }

        }

        /// <summary>
        /// 过滤器
        /// </summary>
        /// <param name="_"></param>
        /// <param name="func1"></param>
        /// <param name="func2"></param>
        /// <returns></returns>
        public static ISelectionFilter FilterElement(this Document _, Func<Element, bool> func1, Func<Reference, bool> func2 = null)
        {
            return new SelectionFilterBase(func1, func2);
        }
    }
}
