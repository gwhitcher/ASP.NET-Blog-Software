using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcBlog.Models;
using PagedList;

namespace blog.Controllers
{
    public class BlogController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        // GET: /Blog/
        public ActionResult Index(string sortOrder, int? page, string currentFilter, string searchString, string category, string categoryFilter)
        {
            if (searchString != null || category != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
                category = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            ViewBag.categoryFilter = category;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var blog = from s in db.Blog
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                blog = blog.Where(s => s.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            if (!String.IsNullOrEmpty(category))
            {
                blog = blog.Where(s => s.Category.ToUpper().Contains(category.ToUpper()));
            }
            switch (sortOrder)
            {
                default:
                    blog = blog.OrderByDescending(s => s.ID);
                    break;
            }
            int pageSize = 1;
            int pageNumber = (page ?? 1);
            return View(blog.ToPagedList(pageNumber, pageSize));
            //CATEGORY DISTINCT CALL
            var categoryquery = "SELECT DISTINCT category FROM Blog";
        }

        // GET: /Blog/Details/5
        public ActionResult Details(int? id, string Slug)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: /Blog/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: /Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Category,Slug,Date,Body")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: /Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                return View(blog);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: /Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Category,Slug,Date,Body")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: /Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            if (User.Identity.IsAuthenticated)
            {
                return View(blog);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // POST: /Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
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