using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using CsharpDemo.Extension;
using System.Collections.Generic;
using System;
using RevitAPI.Toolkit.Attributes;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("房间填充区域","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateRoom2FilledRegion : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            // 全部房间
            var rooms = doc.OfClass<SpatialElement>(doc.ActiveView).Where(o => o.Category.Name == "房间").ToList();
            if (rooms.Count == 0)
            {
                TaskDialog.Show("提示", "当前视图房间个数为0");
                return Result.Cancelled;
            }
            // 填充区域类型
            var types = doc.OfClass<FilledRegionType>();
            var opt = new SpatialElementBoundaryOptions() { SpatialElementBoundaryLocation = SpatialElementBoundaryLocation.Finish };
            doc.Transaction(t =>
            {
                foreach (var room in rooms)
                {
                    var random = new Random();
                    var type = types.ElementAtOrDefault(random.Next(types.Count));

                    var curveloop = new CurveLoop();
                    var boundaries = room.GetBoundarySegments(opt);
                    foreach (var segments in boundaries)
                    {
                        foreach (var item in segments)
                        {
                            var curve = item.GetCurve();
                            curveloop.Append(curve);
                        }
                    }
                    var bd = new List<CurveLoop>() { curveloop };
                    FilledRegion.Create(doc, type.Id, doc.ActiveView.Id, bd);
                }
            }, "c# FilledRegion");

            return Result.Succeeded;
        }
    }
}
