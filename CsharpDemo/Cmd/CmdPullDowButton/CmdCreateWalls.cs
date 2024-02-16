using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Collections.Generic;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建墙","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateWalls : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var levels = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .WhereElementIsNotElementType()
                .ToElements();
            // 指定名称:标高 1
            var levelId = levels.FirstOrDefault(o => o.Name == "标高 1").Id;
            doc.Transaction(t =>
            {
                var lines = new List<Line>();
                var p0 = new XYZ(0, 0, 0);
                var p1 = new XYZ(10000 / 304.8, 0, 0);
                var p2 = new XYZ(10000 / 304.8, 10000 / 304.8, 0);
                var p3 = new XYZ(0, 10000 / 304.8, 0);
                lines.Add(Line.CreateBound(p0, p1));
                lines.Add(Line.CreateBound(p1, p2));
                lines.Add(Line.CreateBound(p2, p3));
                lines.Add(Line.CreateBound(p3, p0));
                foreach (var line in lines)
                {
                    Wall.Create(doc, line, levelId, true);
                }
            }, "Csharp wall");
            return Result.Succeeded;
        }
    }
}
