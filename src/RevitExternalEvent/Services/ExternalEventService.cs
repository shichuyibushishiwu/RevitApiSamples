///************************************************************************************
///   Author:十五
///   CretaeTime:2022/7/12 22:22:00
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

namespace RevitExternalEvent.Services
{
    internal class ExternalEventService : IExternalEventHandler
    {
        private readonly ExternalEvent _externalEvent;
        private Action<UIApplication>? _action;

        public ExternalEventService()
        {
            _externalEvent = ExternalEvent.Create(this);
        }

        public void Execute(UIApplication app)
        {
            if (_action == null)
            {
                return;
            }
            _action.Invoke(app);
        }

        public string GetName() => "External Event Services";

        public void Raise(Action<UIApplication> action)
        {
            _action = action;
            _externalEvent.Raise();
        }
    }
}
