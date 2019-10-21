using Ryan.WebAPI.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Ryan.WebAPI.Controllers
{
    public class UsersController : Controller
    {
        private RyanWebAPIContext db = new RyanWebAPIContext();

        // GET: Users
        public ActionResult Index()
        {
            //return View(db.Users.ToList());
            return View();
        }

        [HttpGet]
        public JsonResult GetAll(int page, int limit)
        {
            JsonResult json = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            List<User> users = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                User user = new User()
                {
                    UserID = 101 + i,
                    Username = "user-" + i.ToString(),
                    Sex = "男",
                    City = "城市-" + i.ToString(),
                    Sign = "签名-" + i.ToString(),
                    Experience = 646,
                    Logins = 107,
                    Wealth = 974506 + i,
                    Classify = "酱油",
                    Score = 77 + i
                };

                users.Add(user);
            }

            IQueryable<User> res = (IQueryable<User>)null;

            page = page == 0 ? 1 : page;
            {
                res = users.OrderBy(x => x.UserID).Skip((page - 1) * limit).Take(limit).AsQueryable();
            }
            
            ResultParameter<User> resultParameter = new ResultParameter<User>()
            {
                code = 0,
                msg = "",
                count = users.Count(),
                data = res
            };

            json.Data = resultParameter;
            return json;

        }


        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Sex,City,Sign,Experience,Logins,Wealth,Classify,Score")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,Sex,City,Sign,Experience,Logins,Wealth,Classify,Score")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
