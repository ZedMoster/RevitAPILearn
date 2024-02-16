using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using CsharpDemo.Extension;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建窗","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateWindow : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            // 过滤出主体墙
            var walls = doc.OfClass<Wall>();
            // 窗类型
            var windowType = doc.OfClass<FamilySymbol>().Where(o => o.Category.Name == "窗").FirstOrDefault(o => o.Name == "0915 x 0610mm");
            doc.Transaction(t =>
            {
                foreach (var wall in walls)
                {
                    var curve = (wall.Location as LocationCurve).Curve;
                    var loc1 = curve.Evaluate(0.25, true);
                    var loc2 = curve.Evaluate(0.75, true);
                    if (!windowType.IsActive) windowType.Activate();
                    var ins1 = doc.Create.NewFamilyInstance(loc1, windowType, wall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    var ins2 = doc.Create.NewFamilyInstance(loc2, windowType, wall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                    // 设置底标高 INSTANCE_SILL_HEIGHT_PARAM
                    ins1.get_Parameter(BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM).Set(900 / 304.8);
                    ins2.get_Parameter(BuiltInParameter.INSTANCE_SILL_HEIGHT_PARAM).Set(900 / 304.8);
                }
            }, "csharp window");
            return Result.Succeeded;
        }
    }
}
