using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建门","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateDoor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var walls = new FilteredElementCollector(doc).OfClass(typeof(Wall)).ToElements();
            var doorType = new FilteredElementCollector(doc).OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_Doors)
                .Cast<FamilySymbol>()
                .FirstOrDefault(o => o.Name == "0915 x 2134mm");
            doc.Transaction(t =>
            {
                if (!doorType.IsActive)
                {
                    doorType.Activate();
                }
                foreach (var wall in walls)
                {
                    var location = (wall.Location as LocationCurve).Curve.Evaluate(0.5, true);
                    doc.Create.NewFamilyInstance(location, doorType, wall, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                }
            }, "c# door");
            return Result.Succeeded;
        }
    }
}
