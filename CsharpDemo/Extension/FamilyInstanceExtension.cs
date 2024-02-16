using Autodesk.Revit.DB;

namespace CsharpDemo.Extension
{
    public static class FamilyInstanceExtension
    {
        /// <summary>
        /// 获取分析模型定位点
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="index"> 1</param>
        /// <returns></returns>
        public static XYZ GetTopPoint(this FamilyInstance instance, int index = 1)
        {
            var mod = instance.GetAnalyticalModel();
            if (mod == null) return default;
            var curve = mod.GetCurve();
            return curve.GetEndPoint(index);
        }
    }
}
