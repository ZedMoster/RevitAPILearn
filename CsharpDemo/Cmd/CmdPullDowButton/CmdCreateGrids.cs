using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建轴网","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateGrids : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var p0 = XYZ.Zero;
            var count = 5;
            var length = 30000.0;
            var distance = length / count;

            doc.Transaction(t =>
            {
                var p1 = new XYZ((length + distance) / 304.8, 0, 0);
                for (int i = 0; i < count; i++)
                {
                    var di = XYZ.BasisY * i * distance / 304.8;
                    var line = Line.CreateBound(p0 + di, p1 + di);
                    var g = Grid.Create(doc, line);
                    if (i == 0) g.get_Parameter(BuiltInParameter.DATUM_TEXT).Set("A");
                }
                var p2 = new XYZ(0, length / 304.8, 0);
                p0 = new XYZ(0, -distance / 304.8, 0);
                for (int i = 0; i < count; i++)
                {
                    var di = XYZ.BasisX * (i + 1) * distance / 304.8;
                    var line = Line.CreateBound(p0 + di, p2 + di);
                    var g = Grid.Create(doc, line);
                    // 设置起始名称
                    if (i == 0) g.get_Parameter(BuiltInParameter.DATUM_TEXT).Set("1");
                }
            }, "Csharp grid");
            return Result.Succeeded;
        }
    }
}
