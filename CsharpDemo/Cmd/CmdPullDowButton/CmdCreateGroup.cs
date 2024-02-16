using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System.Collections.Generic;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("创建模型组","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateGroup : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            try
            {
                var references = uidoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
                var ids = new List<ElementId>();
                foreach (var item in references)
                {
                    var el = doc.GetElement(item);
                    ids.Add(el.Id);
                }
                if (ids.Count == 0)
                {
                    message = "未获取到选择的图元";
                    return Result.Failed;
                }
                doc.Transaction(t =>
                {
                    var group = doc.Create.NewGroup(ids);
                    var grouptype = group.GroupType;
                    var origin = new XYZ(30000 / 304.8, 0, 0);
                    for (int i = 0; i < 10; i++)
                    {
                        var loc = origin * i;
                        doc.Create.PlaceGroup(loc, grouptype);
                    }
                }, "创建组");
                return Result.Succeeded;
            }
            catch (OperationCanceledException)
            {
                return Result.Cancelled;
            }
        }
    }
}
