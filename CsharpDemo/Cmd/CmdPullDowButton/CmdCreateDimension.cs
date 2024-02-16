using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建尺寸标注", "")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateDimension : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            var filter = doc.FilterElement(el => !(el is Dimension));
            var els = uidoc.Selection.PickElementsByRectangle(filter, "请选择标注对象");
            if (els.Count == 0)
            {
                return Result.Cancelled;
            }
            var filter2 = doc.FilterElement(el => el is Grid);
            var grid = default(Grid);
            try
            {
                var refg = uidoc.Selection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.Element, filter2, "请选择标注定位线(轴网)");
                grid = doc.GetElement(refg) as Grid;
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled;
            }

            var line = grid.Curve as Line;
            if (line != null)
            {
                var references = new ReferenceArray();
                foreach (var el in els)
                {
                    var refer = new Reference(el);
                    references.Append(refer);
                }
                if (references.Size >= 2)
                {
                    doc.Transaction(t =>
                    {
                        doc.Create.NewDimension(doc.ActiveView, line, references);
                    }, "C# dimension");
                }
            }
            return Result.Succeeded;
        }
    }
}
