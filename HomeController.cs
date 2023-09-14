using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Repository;
using Haberler.Filter;
using Haberler.Models;

namespace Haberler.Controllers
{
    public class HomeController : Controller
    {
        NewsRepository newsRepository;
        EditorRepository editorRepository;
        public HomeController(NewsRepository rep,EditorRepository repository)
        {
            newsRepository = rep;
            editorRepository = repository;

        }

        public ActionResult Index()
        {
            return View();
        }
       

        public ActionResult Error()
        {
            return View();
        }
    }
}
