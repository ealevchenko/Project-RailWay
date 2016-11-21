using EFRailWay.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MetallurgTransController : Controller
    {
        private IMTRepository repository;
        public MetallurgTransController(IMTRepository mt_repository)
        {
            this.repository = mt_repository;
        }
        
        // GET: MetallurgTrans
        public ActionResult Index()
        {
            return View(repository.MTSostav);
        }
    }
}