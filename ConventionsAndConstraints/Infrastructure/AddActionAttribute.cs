using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AddActionAttribute : Attribute
    {
        public string AdditionalName { get; }

        public AddActionAttribute(string name)
        {
            AdditionalName = name;
        }
    }

    public class AdditionalActionsAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var actions = controller.Actions.Select(x => new
            {
                Action = x,
                Names = x.Attributes.Select(attr => (attr as AddActionAttribute)?.AdditionalName)
            });

            foreach (var item in actions.ToList())
            {
                foreach (var name in item.Names)
                {
                    controller.Actions.Add(new ActionModel(item.Action)
                    {
                        ActionName = name
                    });
                }
            }
        }
    }
}
