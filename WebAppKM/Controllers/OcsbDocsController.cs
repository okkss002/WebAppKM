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
using System.IO;
using System.Data.SqlClient;

//https://www.youtube.com/watch?v=Slw-gs2vcWo

namespace WebAppKM.Controllers
{
    public class OcsbDocsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OcsbDocs
        public async Task<ActionResult> Index()
        {
            string doctype = Request.QueryString["DocType"];
            string docgroup = Request.QueryString["DocGroup"];
            string pName = Request.QueryString["Pname"];
            ViewBag.doctype = doctype;
            ViewBag.docgroup = docgroup;
            ViewBag.pname = pName;
            if (string.IsNullOrEmpty(docgroup))
            {
                var od = await (from d in db.OcsbDoc
                                where d.DocType == doctype
                                && d.Status.Equals(false)
                                orderby d.DocID
                                select d).ToListAsync();
                return View(od);
            }
            else
            {
                var od = await (from d in db.OcsbDoc
                                where d.DocType == doctype && d.DocGroup == docgroup
                                && d.Status.Equals(false)
                                orderby d.DocID
                                select d).ToListAsync();
                return View(od);
            }
        }

        public async Task<ActionResult> SearchOcsbDoc(string txtFilter)
        {
            string doctype = Request.QueryString["DocType"];
            string docgroup = Request.QueryString["DocGroup"];
            string pName = Request.QueryString["Pname"];
            ViewBag.doctype = doctype;
            ViewBag.docgroup = docgroup;
            ViewBag.pname = pName;
            if (pName == "ทั้งหมด")
            {
                if (string.IsNullOrEmpty(txtFilter))
                {
                    return View("Index", await (from d in db.OcsbDoc
                                                where d.Status.Equals(false)
                                                orderby d.DocID
                                                select d).ToListAsync());
                }
                else
                {
                    return View("Index", await (from d in db.OcsbDoc
                                                where d.DocName.Contains(txtFilter)
                                                && d.Status.Equals(false)
                                                orderby d.DocID
                                                select d).ToListAsync());
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtFilter))
                {
                    if (string.IsNullOrEmpty(docgroup))
                    {
                        return View("Index", await (from d in db.OcsbDoc
                                                    where d.DocType == doctype
                                                    && d.Status.Equals(false)
                                                    orderby d.DocID
                                                    select d).ToListAsync());
                    }
                    else
                    {
                        return View("Index", await (from d in db.OcsbDoc
                                                    where d.DocType == doctype && d.DocGroup == docgroup
                                                    && d.Status.Equals(false)
                                                    orderby d.DocID
                                                    select d).ToListAsync());
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(docgroup))
                    {
                        return View("Index", await (from d in db.OcsbDoc
                                                    where d.DocName.Contains(txtFilter) && d.DocType == doctype
                                                    && d.Status.Equals(false)
                                                    orderby d.DocID
                                                    select d).ToListAsync());
                    }
                    else
                    {
                        return View("Index", await (from d in db.OcsbDoc
                                                    where d.DocName.Contains(txtFilter) && d.DocType == doctype && d.DocGroup == docgroup
                                                    && d.Status.Equals(false)
                                                    orderby d.DocID
                                                    select d).ToListAsync());
                    }

                }
            }
            //return View("Index", await db.OcsbDoc.Where(p =>
            // p.DocName.Contains(txtFilter)).ToListAsync());
        }

        //[Authorize(Roles = "Administrator")]
        // GET: OcsbDocs/Details/5
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

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        //[Authorize(Roles = "Administrator")]
        // GET: OcsbDocs/Create
        public ActionResult Create()
        {
            string doctype = Request.QueryString["DocType"];
            string docgroup = Request.QueryString["DocGroup"];
            string pName = Request.QueryString["Pname"];
            ViewBag.doctype = doctype;
            ViewBag.docgroup = docgroup;
            ViewBag.pname = pName;
            return View();
        }

        //[Authorize(Roles = "Administrator")]
        // POST: OcsbDocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(HttpPostedFileBase file, [Bind(Include = "DocID,DocType,DocGroup,DocName,Details,Keyword,FileName,Links,Status")] OcsbDoc ocsbDoc)
        {
            if (ModelState.IsValid)
            {
                db.OcsbDoc.Add(ocsbDoc);
                if (file != null && file.ContentLength > 0)
                    try
                    {
                        string sdate = DateTime.Now.Day.ToString("00") + DateTime.Now.Month.ToString("00") + DateTime.Now.Year.ToString("0000") + "-";
                        string filename = sdate + file.FileName;
                        //string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(file.FileName));
                        //string url = "http://km.ocsb.go.th/Uploads/" + Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(filename));
                        string url = "http://km.ocsb.go.th/Uploads/" + Path.GetFileName(filename);
                        file.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                        ocsbDoc.FileName = file.FileName.ToString();
                        ocsbDoc.Links = url.ToString();
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                //else
                //{
                //    ViewBag.Message = "You have not specified a file.";
                //}
                // return View();
                //ocsbDoc.Links = path.ToString();
                //db.OcsbDoc.Add(ocsbDoc).FileName = file.FileName.ToString();
                //db.OcsbDoc.Add(ocsbDoc).Links = path;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ocsbDoc);
        }

        //[Authorize(Roles = "Administrator")]
        // GET: OcsbDocs/Edit/5
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

        //[Authorize(Roles = "Administrator")]
        // POST: OcsbDocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

        //[Authorize(Roles = "Administrator")]
        // GET: OcsbDocs/Delete/5
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

        //[Authorize(Roles = "Administrator")]
        // POST: OcsbDocs/Delete/5
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
