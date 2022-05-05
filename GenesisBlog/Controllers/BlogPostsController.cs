#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GenesisBlog.Data;
using GenesisBlog.Models;
using System.Text;

namespace GenesisBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        //private readonly IConfiguration _configuration;

        //public BlogPostsController(ApplicationDbContext context, IConfiguration configuration)
        public BlogPostsController(ApplicationDbContext context)
        {
            _context = context;
            //_configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogPost.ToListAsync());
        }
   
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Abstract,Content")] BlogPost blogPost)
        {
            //Ill check with the Model annotations to see if anything has been violated
            if (ModelState.IsValid)
            {

                //Because we can't properly encode the validation of the incoming Quill data
                //we have to rely on using custom error handling or we have to add a custom error

                //var badContent = _configuration["DefaultSettings:QuillContent"];
                //if (blogPost.Content == badContent)
                if (blogPost.Content == "<p><br></p>")
                {
                    //The next two lines are for displaying errors in the validation summary
                    //I know this because there isn't a property used in the method
                    ModelState.AddModelError("", "Errors in the Content have been detected!");
                    ModelState.AddModelError("", "Here I am...");

                    //This line of code displays an error message inside of the span
                    //used for displaying error messgaes associated with the Content property
                    ModelState.AddModelError("Content", "Hey I saw that you tried to sneak in the default .....");
                    return View(blogPost);
                }
                else if(blogPost.Content == "<p>.</p>")
                {
                    ModelState.AddModelError("", "Errors in the Content have been detected!");
                    ModelState.AddModelError("Content", "Can you please give me something more than that...");
                    return View(blogPost);
                }

                blogPost.Created = DateTime.UtcNow;
                _context.Add(blogPost);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }
           
            return View(blogPost);
        }
    
        public async Task<IActionResult> Edit(int? id)
        {
                      
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost.FindAsync(id);
            if (blogPost == null)
            {
                return NotFound();
            }
            return View(blogPost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Abstract,Content,Created")] BlogPost blogPost)
        {
            if (id != blogPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    blogPost.Created = DateTime.SpecifyKind(blogPost.Created, DateTimeKind.Utc);
                    blogPost.Updated = DateTime.UtcNow;
                    _context.Update(blogPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogPostExists(blogPost.Id))
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
            return View(blogPost);
        }

        // GET: BlogPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // POST: BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogPost = await _context.BlogPost.FindAsync(id);
            _context.BlogPost.Remove(blogPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return _context.BlogPost.Any(e => e.Id == id);
        }
    }
}
