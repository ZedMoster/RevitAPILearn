using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建平面视图", "标高生成指定类型的视图")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateViewplan : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var levels = doc.OfClass<Level>();
            var viewFamilyType = doc.OfClass<ViewFamilyType>().FirstOrDefault(o => o.ViewFamily == ViewFamily.FloorPlan);
            doc.Transaction(t =>
            {
                foreach (var level in levels)
                {
                    ViewPlan.Create(doc, viewFamilyType.Id, level.Id);
                }
            }, "Csharp viewplan");
            return Result.Succeeded;
        }
    }
}
