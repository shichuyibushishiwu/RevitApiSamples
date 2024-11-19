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
using Tuna.Revit.Extension;

namespace RevitExternalEvent;

public class App : IExternalApplication
{
    internal const string _tabName = "Shiwu";

    internal static App? Current;

    internal Tuna.Revit.Extension.IExternalEventService? _service;

    public Result OnShutdown(UIControlledApplication application)
    {
        return Result.Succeeded;
    }

    public Result OnStartup(UIControlledApplication application)
    {
        Current = this;
        _service = new Tuna.Revit.Extension.ExternalEventService();

        IRibbonTab ribbonTab = application.AddRibbonTab(_tabName);
        ribbonTab.AddRibbonPanel("测试", panel =>
        {
            panel.AddPushButton<Command.ModelessCommand>(data =>
            {
                data.Title = "非模态";
                data.LargeImage = Properties.Resources.Discover;
            });
        });


        return Result.Succeeded;
    }


}
