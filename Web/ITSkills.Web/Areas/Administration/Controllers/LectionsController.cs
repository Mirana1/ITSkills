namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data;
    using ITSkills.Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [Area("Administration")]
    public class LectionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LectionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/Lections
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lections.Include(l => l.Course).Include(l => l.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Lections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections
                .Include(l => l.Course)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lection == null)
            {
                return NotFound();
            }

            return View(lection);
        }

        // GET: Administration/Lections/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/Lections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,Title,Url,Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Lection lection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lection.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lection.UserId);
            return View(lection);
        }

        // GET: Administration/Lections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections.FindAsync(id);
            if (lection == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lection.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lection.UserId);
            return View(lection);
        }

        // POST: Administration/Lections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,Title,Url,Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Lection lection)
        {
            if (id != lection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LectionExists(lection.Id))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Id", lection.CourseId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", lection.UserId);
            return View(lection);
        }

        // GET: Administration/Lections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lection = await _context.Lections
                .Include(l => l.Course)
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lection == null)
            {
                return NotFound();
            }

            return View(lection);
        }

        // POST: Administration/Lections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lection = await _context.Lections.FindAsync(id);
            _context.Lections.Remove(lection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LectionExists(int id)
        {
            return _context.Lections.Any(e => e.Id == id);
        }
    }
}
