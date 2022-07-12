///************************************************************************************
///   Author:十五
///   CretaeTime:2022/7/11 23:34:30
///   Mail:1012201478@qq.com
///   Github:https://github.com/shichuyibushishiwu
///
///   Description:
///
///************************************************************************************

using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RevitExternalEvent
{
    public class App : IExternalApplication
    {
        internal const string _tabName = "Shiwu";

        internal static App? Current;

        internal Services.ExternalEventService? _service;

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            Current = this;
            _service=new Services.ExternalEventService();

            application.CreateRibbonTab(_tabName);
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(_tabName, "测试");
            var type = typeof(Command.ModelessCommand);
            PushButtonData button = new PushButtonData("modeless", "非模态", type.Assembly.Location, type.FullName);
            button.LargeImage = ConvertToBitmapSource(Properties.Resources.Discover);
            ribbonPanel.AddItem(button);
            return Result.Succeeded;
        }

        public BitmapSource ConvertToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
