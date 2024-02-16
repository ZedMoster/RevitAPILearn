using Autodesk.Revit.UI;
using RevitAPI.Toolkit.Attributes;
using CsharpDemo.Model;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace CsharpDemo.Extension
{
    public static class XmlPushButtonDataExtension
    {
        /// <summary>
        /// 通过类型获取PushButtonData
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static PushButtonData GetPushButtonData(this Type type)
        {
            var data = new XmlPushButtonData(type);
            return data.GetPushButtonData();
        }

        /// <summary>
        /// Image 转换 BitmapSource
        /// </summary>
        /// <param name="image"> 资源图片</param>
        /// <returns>BitmapSource</returns>
        public static BitmapSource GetImageBitmapSource(this Image image)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.Png);
                    stream.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.UriSource = null;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                }
                return bitmapImage;
            }
            catch
            {
                return default;
            }
        }

    }
}
