using ConventionsAndConstraints.Infrastructure;
using ConventionsAndConstraints.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConventionsAndConstraints.Controllers
{
    //[AdditionalActions]
    public class HomeController: Controller
    {
        public IActionResult Index() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Index)
        });

        [ActionName("Index")]
        [UserAgent("Edge")]
        public IActionResult Other() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(Other)
        });

        //[ActionName("Details")]
        //[ActionNamePrefix("Do")]
        //[AddAction("Details")]
        [UserAgent("Edge")]
        public IActionResult List() => View("Result", new Result
        {
            Controller = nameof(HomeController),
            Action = nameof(List)
        });
    }
}
