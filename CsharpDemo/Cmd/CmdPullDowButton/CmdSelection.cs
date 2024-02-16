using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using CsharpDemo.Utils;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("选择方式","")]
    [Transaction(TransactionMode.Manual)]
    class CmdSelection : RevitCommand
    {
        public override void Action()
        {
            var uidoc = Uidoc;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            //var box = sel.PickBox(Autodesk.Revit.UI.Selection.PickBoxStyle.Directional);
            //MessageBox.Show($"Min:{box.Min},Max:{box.Max}");

            //var els = sel.PickElementsByRectangle(doc.FilterElement(o=> o is Wall),"我是提示,建议从左往右框选");
            //MessageBox.Show(els.Count.ToString());

            //var ref1 = sel.PickObject(Autodesk.Revit.UI.Selection.ObjectType.LinkedElement);
            //MessageBox.Show(doc.GetElement(ref1).Id.ToString());

            //var refs = sel.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            //MessageBox.Show(refs.Count.ToString());

            //var pnt = sel.PickPoint();
            //MessageBox.Show(pnt.ToString());

            //var ids = sel.GetElementIds();
            //MessageBox.Show(ids.Count.ToString());

            var walls = doc.OfClass<Wall>(doc.ActiveView);
            var ids = walls.Select(o => o.Id).ToList();
            sel.SetElementIds(ids);

        }
    }

}
