using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Canteen.Models;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using System.IO;

namespace Canteen.Controllers
{
    [Authorize]
    public class LunchController : Controller
    {
        /// <summary>
        /// Loads the list of the menu Items
        /// </summary>
        /// <returns></returns>
        /// <remarks>Celimpilo Mncwango</remarks>
        public ActionResult Index()
        {
            using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
            {
                try
                {
                    List<LunchMenuModel> menu = db.context.FindAll().ToList<LunchMenuModel>();
                    return View(menu);
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction("Index");
                }
            }

        }
        /// <summary>
        /// Creates the new entry of the Lunch Menu
        /// </summary>
        /// <param name="menuObj"></param>
        /// <returns></returns>
        /// <remarks>Celimpilo Mncwango</remarks>
        [HttpPost]
        public ActionResult CreateMenu(LunchMenu menuObj)
        {
            LunchMenuModel model = null;
            if (ModelState.IsValid)
            {
                try
                {
                    if (menuObj.ImagePath != null)
                    {
                        string fileName = Saveimage(menuObj);
                        model = new LunchMenuModel
                        {
                            DishName = menuObj.DishName,
                            Price = menuObj.Price,
                            ServedAt = menuObj.ServedAt,
                            ImagePath = fileName
                        };

                    }
                    using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
                    {
                        db.context.Insert(model);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return View();
                }
            }
            return View();
        }

        /// <summary>
        /// Loads the Menu Creating Page
        /// </summary>
        /// <returns></returns>
        /// <remarks>Celimpilo Mncwango</remarks>
        public ActionResult CreateMenu()
        {

            return View();
        }
        public ActionResult Edit(string Id)
        {
            try
            {
                using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
                {
                    var menu = db.context.FindOneById(new ObjectId(Id));
                    return View(menu);
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = e.Message;
                return RedirectToAction("Index");

            }
        }



        [HttpPost]
        public ActionResult Edit(LunchMenuModel menu)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    menu.ImagePath = GetPreviosImageName(menu.Id);


                    using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
                    {
                        db.context.Save(menu);
                        return RedirectToAction("Index");

                    }
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        /// <summary>
        /// Loads the confirmation page for deleting the Object
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>celimpilo Mncwango</remarks>
        public ActionResult Delete(string Id)
        {
            using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
            {
                try
                {
                    var objTodelete = db.context.FindOneById(new ObjectId(Id));
                    return View(objTodelete);
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction("Index");
                }
            }
        }
        /// <summary>
        /// Deletes the Menu Item
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <remarks>Celimpilo Mncwango</remarks>

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string Id)
        {
            using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
            {
                try
                {
                    var rental = db.context.Remove(Query.EQ("_id", new ObjectId(Id)));
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewBag.Error = e.Message;
                    return RedirectToAction("Index");
                }
            }
        }



        private string Saveimage(LunchMenu menuObj)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetFileName(menuObj.ImagePath.FileName);
                var path = Path.Combine(Server.MapPath("~/images"), fileName);
                menuObj.ImagePath.SaveAs(path);
                return fileName;
            }
            catch (Exception e)
            {

                return string.Empty;
            }
        }

        private string GetPreviosImageName(string id)
        {
            try
            {
                using (MongoContext<LunchMenuModel> db = new MongoContext<LunchMenuModel>("LunchMenu"))
                {
                    var menu = db.context.FindOneById(new ObjectId(id));
                    return menu.ImagePath;
                }
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
    }
}