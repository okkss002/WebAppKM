using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppKM.Models;

namespace WebAppKM.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            //return View(await db.OcsbDoc.ToListAsync());
            var od = await (from d in db.OcsbDoc
                            orderby d.DocID
                            select d).ToListAsync();
            return View(od);
        }

        public async Task<ActionResult> SearchOcsbDoc(string txtFilter)
        {
            if (string.IsNullOrEmpty(txtFilter))
            {
                return View("Index", await (from d in db.OcsbDoc
                                            orderby d.DocID
                                            select d).ToListAsync());
            }
            else
            {
                return View("Index", await (from d in db.OcsbDoc
                                            where d.DocName.Contains(txtFilter)
                                            orderby d.DocID
                                            select d).ToListAsync());
            }
            //return View("Index", await db.OcsbDoc.Where(p =>
            // p.DocName.Contains(txtFilter)).ToListAsync());
        }

        // GET: Admin/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcsbDoc ocsbDoc = await db.OcsbDoc.FindAsync(id);
            if (ocsbDoc == null)
            {
                return HttpNotFound();
            }
            return View(ocsbDoc);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DocID,DocType,DocGroup,DocName,Details,Keyword,FileName,Links,Status")] OcsbDoc ocsbDoc)
        {
            if (ModelState.IsValid)
            {
                db.OcsbDoc.Add(ocsbDoc);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ocsbDoc);
        }

        // GET: Admin/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcsbDoc ocsbDoc = await db.OcsbDoc.FindAsync(id);
            if (ocsbDoc == null)
            {
                return HttpNotFound();
            }
            return View(ocsbDoc);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DocID,DocType,DocGroup,DocName,Details,Keyword,FileName,Links,Status")] OcsbDoc ocsbDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ocsbDoc).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ocsbDoc);
        }

        // GET: Admin/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OcsbDoc ocsbDoc = await db.OcsbDoc.FindAsync(id);
            if (ocsbDoc == null)
            {
                return HttpNotFound();
            }
            return View(ocsbDoc);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            OcsbDoc ocsbDoc = await db.OcsbDoc.FindAsync(id);
            db.OcsbDoc.Remove(ocsbDoc);
            await db.SaveChangesAsync();
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
