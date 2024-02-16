using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System;
using System.Linq;

namespace CsharpDemo.Cmd.CmdPullDowButton
{
    [Xml("幕墙随机参数","")]
    [Transaction(TransactionMode.Manual)]
    public class CmdCreateMQ : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var doc = commandData.Application.ActiveUIDocument.Document;
            var els = doc.OfClass<FamilyInstance>().Where(o => o.Category.Name == "幕墙嵌板");
            var random = new Random();
            doc.Transaction(t =>
            {
                foreach (var item in els)
                {
                    // 更新厚度实例参数
                    var p1 = item.LookupParameter("厚度");
                    p1.SetValueString(random.Next(10, 100).ToString());

                    // 替换材质
                    var m = NewMaterial(doc, p1.AsValueString(), random);
                    var p2 = item.LookupParameter("材质");
                    p2.Set(m.Id);

                    // 标记 = 厚度
                    item.LookupParameter("标记").Set(p1.AsValueString());
                }
            }, "C# 幕墙嵌板随机");
            return Result.Succeeded;
        }

        /// <summary>
        /// 创建随机材质
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Material NewMaterial(Document doc, string name, Random random)
        {
            var material = doc.OfClass<Material>().FirstOrDefault(o => o.Name == name);
            if (material == null)
            {
                ElementId materialId = Material.Create(doc, name);
                material = doc.GetElement(materialId) as Material;
            }
            material.Transparency = random.Next(0, 101);
            material.Color = new Color((byte)random.Next(0, 256), (byte)random.Next(0, 256), (byte)random.Next(0, 256));
            return material;
        }
    }
}
