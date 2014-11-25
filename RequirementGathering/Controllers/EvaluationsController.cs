﻿using System;
using System.Data.Entity;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using RequirementGathering.Helpers;
using RequirementGathering.Models;
using RequirementGathering.Reousrces;

namespace RequirementGathering.Controllers
{
    [Authorize]
    public class EvaluationsController : BaseController
    {
        // GET: Evaluations
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public async Task<ActionResult> Index()
        {
            return View(await RgDbContext.Evaluations.ToListAsync());
        }

        // GET: Evaluations/Details/5
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        // GET: Evaluations/Create
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(RgDbContext.Products, "Id", "Name");
            return View(new Evaluation());
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Version,Description,IsActive,ProductId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                RgDbContext.Evaluations.Add(evaluation);
                await RgDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(RgDbContext.Products, "Id", "Name", evaluation.ProductId);
            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(id);

            if (evaluation == null)
                return HttpNotFound();

            ViewBag.ProductId = new SelectList(RgDbContext.Products, "Id", "Name", evaluation.ProductId);
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Version,Description,IsActive,ProductId")] Evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                RgDbContext.Entry(evaluation).State = EntityState.Modified;
                await RgDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(RgDbContext.Products, "ProductId", "Name", evaluation.ProductId);
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        //[Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(id);
        //    if (evaluation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(evaluation);
        //}

        // POST: Evaluations/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Researcher,Administrator,Super Administrator")]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(id);
        //    RgDbContext.Evaluations.Remove(evaluation);
        //    await RgDbContext.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                RgDbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        //
        // GET: /Account/SendInvitation
        [Authorize(Roles = "Administrator,Super Administrator,Researcher")]
        public async Task<ActionResult> SendInvitation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return HttpNotFound();
            }
            return View(evaluation);
        }

        //
        // POST: /Account/SendInvitation
        [HttpPost]
        [Authorize(Roles = "Administrator,Super Administrator,Researcher")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendInvitation(int? Id, string email, string firstName, string lastName, string description)
        {
            if (Id == null)
            {
                ModelState.AddModelError("", "Evalution Id cannot be null");
                return View();
            }

            Evaluation evaluation = await RgDbContext.Evaluations.FindAsync(Id);

            if (evaluation == null)
            {
                ModelState.AddModelError("", "Evaluation not found");
                return View();
            }

            User user = await UserManager.FindByEmailAsync(email);

            if (user != null)
            {
                await UserManager.SendEmailAsync(
                      user.Id,
                      string.Format(CultureInfo.CurrentCulture, Resources.InvitationEmailSubject, evaluation.Product.Name, evaluation.Version),
                      string.Format(CultureInfo.CurrentCulture, Resources.InvitationEmailBodyExisting, Request.Url.GetLeftPart(UriPartial.Authority), Thread.CurrentThread.CurrentUICulture, description)
                );
            }
            else
            {
                user = new User { FirstName = firstName, LastName = lastName, Email = email, UserName = email };
                string password = PasswordHelper.GeneratePassword();
                IdentityResult result = await UserManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await UserManager.SendEmailAsync(
                      user.Id,
                      string.Format(CultureInfo.CurrentCulture, Resources.InvitationEmailSubject, evaluation.Product.Name, evaluation.Version),
                      string.Format(CultureInfo.CurrentCulture, Resources.InvitationEmailBodyNew, Request.Url.GetLeftPart(UriPartial.Authority), Thread.CurrentThread.CurrentUICulture, password, description)
                    );
                }
                else
                {
                    ModelState.AddModelError("", "Sorry the user cannot be created, try again later");
                    return View();
                }
            }

            return RedirectToAction("Index", "Evaluations");
        }

    }
}
