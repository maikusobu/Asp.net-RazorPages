using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContosoUniversity.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(ContosoUniversity.Data.ContosoUniversityContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        //[BindProperty]
        public Student Student { get; set; } = default!;

        // Preventing overposting using TryUpdateModelAsync
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    var emptyStudent = new Student();

        //    if (await TryUpdateModelAsync<Student>(
        //        emptyStudent,
        //        "student",
        //        s => s.FirstMidName, s => s.LastName, s => s.EnrollmentDate))
        //    {
        //        _context.Students.Add(emptyStudent);
        //        await _context.SaveChangesAsync();
        //        return RedirectToPage("./Index");
        //    }

        //    return Page();
        //}
        [BindProperty]
        public StudentVM StudentVM { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            _logger.LogInformation(StudentVM.LastName);
            var entry = _context.Add(new Student());
            entry.CurrentValues.SetValues(StudentVM);
            _logger.LogInformation("Saving a new student record");
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
