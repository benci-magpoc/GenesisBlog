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
using GenesisBlog.Services;
using GenesisBlog.Services.Interfaces;

namespace GenesisBlog.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;

        public BlogPostsController(ApplicationDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }

        // GET: BlogPosts
        public async Task<IActionResult> Index()
        {
            //var posts = await _context.BlogPost.ToListAsync();
            var posts = await _context.BlogPost
                                                  .Include(b => b.Tags)
                                                  .ToListAsync();

            return View(posts);
        }

        // GET: BlogPosts/Details/5
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

        // GET: BlogPosts/Create
        public IActionResult Create()
        {
            //1: What data does the select list contain
            //2. The property that gets transmitted to HttpPost
            //3. The property that gets shown to the user
            ViewData["TagIds"] = new MultiSelectList(_context.Tag, "Id", "Text");
          
            return View();
        }

        // POST: BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Abstract,Content,Created,Updated,Slug,IsDeleted,BlogPostState")] BlogPost blogPost, IFormFile theImage, List<int> tagIds)
        {
            if (ModelState.IsValid)
            {
                //Before I try interacting with the IFormFile
                //I should make sure it's present
                if(theImage is not null)
                {
                    blogPost.ImageData = await _imageService.ConvertFileToByteArrayAsync(theImage);
                    blogPost.ImageType = theImage.ContentType;
                }

                //Associate any/all selected tags with the BlogPost
                foreach(var tagId in tagIds)
                {               
                    blogPost.Tags.Add(await _context.Tag.FindAsync(tagId));                
                }

                _context.Add(blogPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }

        // GET: BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await _context.BlogPost
                                                      .Include(b => b.Tags)
                                                      .FirstOrDefaultAsync(b => b.Id == id);

            if (blogPost == null)
            {
                return NotFound();
            }

            //4th parameter in a multiSelect list is a List<int> representing the current selection
            var tagPks = blogPost.Tags.Select(b => b.Id).ToList();
            ViewData["TagIds"] = new MultiSelectList(_context.Tag, "Id", "Text", tagPks);

            return View(blogPost);
        }

        // POST: BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Abstract,Content,Created,Updated,Slug,IsDeleted,BlogPostState,ImageData,ImageType")] BlogPost blogPost, IFormFile file, List<int> tagIds)
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


                    //Step 1: Delete all of the Tags associated with this blogPost
                    //Somehow remove or delete all of the existing tags for this post

                    //Step 2: Add all the selected tags back
                    foreach (var tagId in tagIds)
                    {
                        blogPost.Tags.Add(await _context.Tag.FindAsync(tagId));
                    }

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

            ViewData["TagIds"] = new MultiSelectList(_context.Tag, "Id", "Text", tagIds);
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
