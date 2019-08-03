using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Infrastructure
{
    public class UserAgentAttribute : Attribute, IActionConstraintFactory
    {
        private string substring;

        public UserAgentAttribute(string sub)
        {
            substring = sub.ToLower();
        }

        public bool IsReusable => false;

        public IActionConstraint CreateInstance(IServiceProvider services)
        {
            return new UserAgentConstraint(services.GetService<UserAgentComparer>(), substring);
        }
    }
    
    public class UserAgentConstraint: IActionConstraint
    {
        private UserAgentComparer comparer;
        private string substring;

        public UserAgentConstraint(UserAgentComparer comp, string sub)
        {
            comparer = comp;
            substring = sub.ToLower();
        }

        public int Order { get; set; } = 0;

        public bool Accept(ActionConstraintContext context)
        {
            return context.RouteContext.HttpContext.Request.Headers["User-Agent"].Any(x => x.ToLower().Contains(substring)) || context.Candidates.Count() == 1;
        }
    }    
}
