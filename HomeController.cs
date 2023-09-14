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

        public ActionResult Index(int? id,string search)
        {


            if (id!=null)
                return View(newsRepository.List().Where(x=>x.CatId==id).Where(X => X.Status == true).OrderBy(x => x.CreateDate).ToList());

            if(search!=null)
                return View(newsRepository.List().Where(X => X.Status == true && (X.Title.ToUpper().Contains(search.ToUpper()) || X.Content.ToUpper().Contains(search.ToUpper()) || X.Spot.ToUpper().Contains(search.ToUpper()))).OrderBy(x => x.CreateDate).ToList());

            return View(newsRepository.List().Where(X => X.Status == true ).OrderBy(x => x.CreateDate).ToList());
        }
        public ActionResult Index2(int? id)
        {
            if(id!=null)
                return View(newsRepository.List().Where(x=>x.CatId==id).Where(X => X.Status == true).OrderBy(x => x.CreateDate).ToList());

            return View(newsRepository.List().Where(X => X.Status == true).OrderBy(x => x.CreateDate).ToList());
        }

        [ReadCounter]
        public ActionResult NewsDetails(int id)
        {
            NewsT news = newsRepository.FindById(id);
            return View(news);
        }

        public PartialViewResult PartialNewsDetail(int id)
        {
            return PartialView(newsRepository.FindById(id));
        }

        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel editor)
        {
            Editor edt = editorRepository.Login(new Editor() { UserName = editor.UserName,Password = editor.Password});
            if (edt ==null)
            {
                editor.Hata = "Hatalı giriş.";
                return View("Login", editor);
            }
            Session["Editor"] = edt;

            return View("Index", newsRepository.List().Where(X => X.Status == true).OrderBy(x => x.CreateDate).ToList());
        }
        public ActionResult Logout()
        {
            Session["Editor"] = null;
            return View("Index",newsRepository.List().Where(X => X.Status == true).OrderBy(x => x.CreateDate).ToList());
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}