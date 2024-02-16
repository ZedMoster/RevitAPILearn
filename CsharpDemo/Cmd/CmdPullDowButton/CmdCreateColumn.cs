using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建结构柱","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateColumn : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;

            // HW400x400x13x21
            var level = doc.OfClass<Level>().FirstOrDefault(o => o.Name == "标高 1");
            var type = doc.OfClass<FamilySymbol>().FirstOrDefault(o => o.FamilyName == "热轧H型钢柱" && o.Name == "HW400x400x13x21");
            doc.Transaction(t =>
            {
                if (!type.IsActive) type.Activate();
                doc.Create.NewFamilyInstance(XYZ.Zero, type, level, Autodesk.Revit.DB.Structure.StructuralType.Column);
            }, "c# column");

            return Result.Succeeded;
        }
    }
}
