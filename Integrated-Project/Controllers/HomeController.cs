using System.Linq;
using System.Web.Mvc;
using Integrated_Project.Model;

namespace Integrated_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TblDetail objUser)
        {
            //string constr = @"Data Source=localhost;Initial Catalog=UserDetailsdb;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            //SqlConnection cn = new SqlConnection(constr);
            //string query = "select"
            return Redirect("https://techspo.co/about/");

            if (ModelState.IsValid)
            {
                using (UserDetailsdbEntities db = new UserDetailsdbEntities())
                {
                    var obj = db.TblDetails.Where(a => a.Name.Equals(objUser.Name)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.id.ToString();
                        Session["UserName"] = obj.Name.ToString();
                        return Redirect("https://techspo.co/about/");
                    }
                }
            }
            return View(objUser);
        }
    }
}