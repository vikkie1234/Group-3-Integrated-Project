using System.Linq;
using System.Web.Mvc;
using Integrated_Project.Model;
using System.ComponentModel.DataAnnotations;

namespace Integrated_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] != null)
            {
                Session.Clear();
                return Redirect("https://techspo.co/about/");
            } else
            {
                Session.Clear();
                return View();
            }
        }

        public ActionResult About()
        {
            if (Session["AdminID"] != null)
            {
                return RedirectToAction("Edit");
            }
            else
            {
                return View();
            }
        }

        //Post About
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult About(TblAdmin objAdmin)
        {
            {
                //if (ModelState.IsValid)
                //{
                using (UserDetailsdbEntities db = new UserDetailsdbEntities())
                {
                    var obj = db.TblAdmins.Where(a => a.Name.Equals(objAdmin.Name) && a.Password.Equals(objAdmin.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AdminID"] = obj.id;
                        Session["AdminName"] = obj.Name;
                        return RedirectToAction("About");
                    }
                    else
                    {
                        ViewBag.error = "Invalid Name or Passowrd";
                        return View();
                    }
                }
                //}
            }
        }

        public ActionResult Change(int id)
        {
            using (UserDetailsdbEntities db = new UserDetailsdbEntities())
            {
                var obj = db.TblDetails.Where(a => a.id.Equals(id)).FirstOrDefault();
                return View(obj);
            }
        }
        //Post Change
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(TblDetail objUser)
        {
            using (UserDetailsdbEntities db = new UserDetailsdbEntities())
            {
                var obj = db.TblDetails.Where(a => a.id.Equals(objUser.id)).FirstOrDefault();
                obj.Name = objUser.Name;
                obj.Email = objUser.Email;
                obj.CellNo = objUser.CellNo;
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
        }

        //Get:
        public ActionResult Delete(int id)
        {
            using (UserDetailsdbEntities db = new UserDetailsdbEntities())
            {
                var obj = db.TblDetails.Where(a => a.id.Equals(id)).FirstOrDefault();
                db.TblDetails.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Edit");
            }
        }

        public ActionResult Edit()
        {
            using (UserDetailsdbEntities db = new UserDetailsdbEntities())
            {
                var Model = db.TblDetails.ToList();
                return View(Model);
            }

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            ViewBag.Message = "Register page.";

            return View();
        }


        //Post Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TblDetail objUser)
        {
            if (ModelState.IsValid)
            {
                using (UserDetailsdbEntities db = new UserDetailsdbEntities())
                {
                    var obj = db.TblDetails.Where(a => a.Name.Equals(objUser.Name)).FirstOrDefault();
                    if (obj == null)
                    {
                        db.TblDetails.Add(objUser);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ViewBag.error = "Name already exists";
                return View();
            }
            return View();
        }


        //Post Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TblDetail objUser)
        {
            //if (ModelState.IsValid)
            //{
            using (UserDetailsdbEntities db = new UserDetailsdbEntities())
            {
                var obj = db.TblDetails.Where(a => a.Name.Equals(objUser.Name)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.id.ToString();
                    Session["UserName"] = obj.Name.ToString();
                    return RedirectToAction("Index");
                }
            }
            //}
            return View(objUser);
        }
    }
}