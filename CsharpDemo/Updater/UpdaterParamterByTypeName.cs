using Autodesk.Revit.DB;
using CsharpDemo.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpDemo.Updater
{
    internal class UpdaterParamterByTypeName : IUpdater
    {
        /// <summary>
        /// UpdaterId
        /// </summary>
        static UpdaterId updaterId;

        public static UpdaterParamterByTypeName Instance;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public static void Init(AddInId id)
        {
            Instance = new UpdaterParamterByTypeName(id);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="id"></param>
        private UpdaterParamterByTypeName(AddInId id)
        {
            updaterId = new UpdaterId(id, new Guid("f3f76a93-7b3d-4d11-bca0-890dad721d3e"));
        }


        #region 注册关闭

        /// <summary>
        /// 启动
        /// </summary>
        public void OnStartup()
        {
            UpdaterRegistry.RegisterUpdater(Instance);
            // 结构柱,结构梁
            var filters = new ElementMulticategoryFilter(
                new List<BuiltInCategory> { BuiltInCategory.OST_StructuralColumns, BuiltInCategory.OST_StructuralFraming });
            UpdaterRegistry.AddTrigger(updaterId, filters, Element.GetChangeTypeElementAddition());
            UpdaterRegistry.AddTrigger(updaterId, filters, Element.GetChangeTypeParameter(new ElementId(BuiltInParameter.ALL_MODEL_TYPE_NAME)));
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void OnShutdown()
        {
            UpdaterRegistry.UnregisterUpdater(updaterId);
        }

        #endregion



        #region IUpdater

        public void Execute(UpdaterData data)
        {
            var doc = data.GetDocument();
            var addels = data.GetAddedElementIds().Select(o => o.ToElement<Element>(doc)).ToList();
            var els = data.GetModifiedElementIds().Select(o => o.ToElement<Element>(doc)).ToList();
            els.AddRange(addels);
            if (els.Any())
            {
                foreach (var item in els)
                {
                    if (!(item is FamilySymbol symbol)) symbol = (item as FamilyInstance).Symbol;
                    if (symbol != null)
                    {
                        // 200x500  200X500   =>  [200, 500]
                        var name = symbol.Name.ToLower();
                        var values = name.Split('x');
                        if (values.Length == 2)
                        {
                            symbol.LookupParameter("b")?.SetValueString(values[0]);
                            symbol.LookupParameter("h")?.SetValueString(values[1]);
                        }
                    }
                }
            }
        }

        public string GetAdditionalInformation()
        {
            return "通过结构梁柱类型名称自动更新尺寸参数功能.eg:200x500,b=200,h=500";
        }

        public ChangePriority GetChangePriority()
        {
            return ChangePriority.Structure;
        }

        public UpdaterId GetUpdaterId()
        {
            return updaterId;
        }

        public string GetUpdaterName()
        {
            return "通过类型名称更新参数";
        }
        #endregion
    }
}
