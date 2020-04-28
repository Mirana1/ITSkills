namespace ITSkills.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ITSkills.Data;
    using ITSkills.Data.Models;
    using ITSkills.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Authorize]
    [Area("Administration")]
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICoursesService coursesService;
        private readonly ICategoriesService categoriesService;

        public CoursesController(ApplicationDbContext context, ICoursesService coursesService, ICategoriesService categoriesService)
        {
            _context = context;
            this.coursesService = coursesService;
            this.categoriesService = categoriesService;
        }

        // GET: Administration/Courses
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Courses.Include(c => c.Category).Include(c => c.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Administration/Courses/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        //public IActionResult Create()
        //{
        //    var categories = this.categoriesService.GetAll<CategoryDropDownViewModel>();
        // var user = this.
        //    var viewModel = new CreateCourseViewModel
        //    {
        //        Categories = categories,
        //    };
        //    return this.View();
        //}

        // POST: Administration/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,ImageUrl,UserId,Requirements,AcquiredKnowledge,CategoryId,Id,CreatedOn,ModifiedOn")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", course.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", course.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // POST: Administration/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Description,Price,ImageUrl,UserId,Requirements,AcquiredKnowledge,CategoryId,Id,CreatedOn,ModifiedOn")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", course.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", course.UserId);
            return View(course);
        }

        // GET: Administration/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Administration/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
