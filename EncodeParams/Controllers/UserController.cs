using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EncodeParams.Data;
using EncodeParams.Models;
using EncodeParams.Helper;
using System.Text.Encodings.Web;
using System.Web;

namespace EncodeParams.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Role
        public async Task<IActionResult> Index()
        {
            var data = await _context.User.ToListAsync();
            //data.ForEach(x => x.Id = EncodeHelper.Encrypt(x.Id));
            return View(data);
        }

        // GET: Role/Details/5
        public async Task<IActionResult> Details(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ", "+");
            id = EncodeHelper.Decrypt(id);
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var role = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User model)
        {
            if (ModelState.IsValid)
            {
                _context.User.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Role/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ","+");
            id = EncodeHelper.Decrypt(id);
            if (id == null || _context.User == null)
            {
                return NotFound();
            }
            
            var model = await _context.User.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, User model)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ", "+");
            id = EncodeHelper.Decrypt(id);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.User.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ", "+");
            id = EncodeHelper.Decrypt(id);
            if (id == null || _context.User == null)
            {
                return NotFound();
            }
            
            var model = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ", "+");
            id = EncodeHelper.Decrypt(id);
            if (_context.User == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Roles'  is null.");
            }
            var model = await _context.User.FindAsync(id);
            if (model != null)
            {
                _context.User.Remove(model);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            id = HttpUtility.UrlDecode(id).Replace(" ", "+");
            id = EncodeHelper.Decrypt(id);
            return _context.User.Any(e => e.Id == id);
        }
    }
}
