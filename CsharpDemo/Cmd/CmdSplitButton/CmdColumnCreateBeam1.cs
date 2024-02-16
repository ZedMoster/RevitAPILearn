using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using CsharpDemo.Views;

namespace CsharpDemo.Cmd.CmdSplitButton
{
    [Xml("柱顶成梁1", "选择两个结构柱,柱子顶端自动创建梁(模态窗口)")]
    [Transaction(TransactionMode.Manual)]
    public class CmdColumnCreateBeam1 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var window = new ColumnCreateBeamWindow(uidoc);
            window.ShowDialog();
            if (window.DialogResult.Value)
            {
                var symbol = window.BeamTypeList.SelectedItem as FamilySymbol;
                var filter = uidoc.Document.FilterElement(el => el.Category.Name == "结构柱", null);
                try
                {
                    while (true)
                    {
                        var ref1 = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filter, "请选择第一个结构柱,Esc退出创建");
                        var ref2 = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filter, "请选择第二个结构柱,Esc退出创建");
                        var doc = uidoc.Document;
                        var col1 = doc.GetElement(ref1) as FamilyInstance;
                        var col2 = doc.GetElement(ref2) as FamilyInstance;
                        var p1 = col1.GetTopPoint();
                        var p2 = col2.GetTopPoint();
                        var level = doc.GetElement(col1.LevelId) as Level;
                        doc.Transaction(t =>
                        {
                            if (!symbol.IsActive) symbol.Activate();
                            var beam = doc.Create.NewFamilyInstance(Line.CreateBound(p1, p2), symbol, level, StructuralType.Beam);
                            beam.LookupParameter("Z 轴偏移值").SetValueString(window.Offset.Text);
                        }, "柱顶成梁");
                    }
                }
                catch (OperationCanceledException) { }
            }
            return Result.Succeeded;
        }
    }
}
