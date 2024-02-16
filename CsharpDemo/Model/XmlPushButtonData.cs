using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Extension;
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace CsharpDemo.Model
{
    public class XmlPushButtonData
    {
        private Type _dtreetype;
        private string inName = Guid.NewGuid().ToString();
        private string buttonName = "qq群:17075104";
        private string nameSpace;
        private string assemblyName;
        private string tooltip;
        private string description;
        private BitmapSource image;
        private BitmapSource stackedImage;
        private BitmapSource tooltipImage;

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type
        {
            get { return _dtreetype; }
            set
            {
                _dtreetype = value;
                if (Type != null)
                {
                    NameSpace = Type.FullName;
                    AssemblyName = Type.Assembly.Location;
                    ButtonName = GetXmlAttribute(Type)?.Name ?? Type.Name;
                    Tooltip = GetXmlAttribute(Type)?.ToolTip ?? "qq群:17075104";
                    Description = GetXmlAttribute(Type)?.Description ?? "微信:zedmoster";
                    Image = GetResourcesByImageName($"CsharpDemo.Resources.{ButtonName}.png");
                    StackedImage = GetResourcesByImageName($"CsharpDemo.Resources.{ButtonName}_16.png");
                    TooltipImage = GetResourcesByImageName($"CsharpDemo.Resources.{ButtonName}_355.png");
                }
            }
        }

        /// <summary>
        /// 内部名称
        /// </summary>
        public string InName
        {
            get { return inName; }
            private set { inName = value; }
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        public string ButtonName
        {
            get { return buttonName; }
            private set { buttonName = value; }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace
        {
            get { return nameSpace; }
            private set { nameSpace = value; }
        }

        /// <summary>
        /// 程序集路径
        /// </summary>
        public string AssemblyName
        {
            get { return assemblyName; }
            private set { assemblyName = value; }
        }

        /// <summary>
        /// 提示文字
        /// </summary>
        public string Tooltip
        {
            get { return tooltip; }
            private set { tooltip = value; }
        }

        /// <summary>
        /// 长提示
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// 大图名称
        /// 
        /// 32*32
        /// </summary>
        public BitmapSource Image
        {
            get { return image; }
            set { image = value; }
        }

        /// <summary>
        /// 小图名称
        /// 
        /// 16*16
        /// </summary>
        public BitmapSource StackedImage
        {
            get { return stackedImage; }
            set { stackedImage = value; }
        }

        /// <summary>
        /// 提示图片
        /// 
        /// 355*355
        /// </summary>
        public BitmapSource TooltipImage
        {
            get { return tooltipImage; }
            set { tooltipImage = value; }
        }

        /// <summary>
        /// 获取标签属性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private XmlAttribute GetXmlAttribute(Type type)
        {
            return type.GetCustomAttribute<XmlAttribute>();
        }

        /// <summary>
        /// 获取资源图片数据
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        private BitmapSource GetResourcesByImageName(string fullname)
        {
            try
            {
                return BitmapFrame.Create(Assembly.GetExecutingAssembly().GetManifestResourceStream(fullname));
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// 创建 PushButtonData 模板
        /// </summary>
        /// <returns></returns>
        public PushButtonData GetPushButtonData()
        {
            var buttonData = new PushButtonData(InName, ButtonName, AssemblyName, NameSpace);
            try
            {
                buttonData.ToolTip = Tooltip;
                buttonData.LongDescription = Description;
                buttonData.LargeImage = Image ?? Properties.Resources.cmd.GetImageBitmapSource();
                buttonData.Image = StackedImage ?? Properties.Resources.cmd_16.GetImageBitmapSource();
                buttonData.ToolTipImage = TooltipImage;
                buttonData.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, @"https://space.bilibili.com/81888284"));
            }
            catch (Exception ex)
            {
                TaskDialog.Show("错误", $"功能添加失败:{ex.Message}");
            }
            return buttonData;
        }


        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="type"></param>
        public XmlPushButtonData(Type type)
        {
            Type = type;
        }
    }
}
