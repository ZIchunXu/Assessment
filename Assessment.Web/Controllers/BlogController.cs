using Assessment.Data;
using Assessment.Services;
using Assessment.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SynicTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Web.Controllers
{
    public class BlogController : SynicController
    {
        private readonly IBlogService BlogService;

        public BlogController(IBlogService blogService)
        {
            this.BlogService = blogService;
        }

        public IActionResult Index()
        {
            var blogs = BlogService.Get();
            return View(blogs);
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Details(Blog model)
        {
            return View();
        }

        public IActionResult Create()
        {
            var ViewModel = new BlogViewModel(); 
            var blogs = BlogService.Get().ToList();
            ViewBag.authors = blogs.Where((x, i) => blogs.FindIndex(nameof => nameof.AuthorID == x.AuthorID) == i).Select(x => new {
                AuthorID = x.AuthorID,
                FullName = x.Author.FirstName + " " + x.Author.LastName
            }).ToArray();

            return View(ViewModel);
        }
        public IActionResult onPostCreate(Blog model)
        {
            var result = BlogService.CreateUpdate(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(this.Index));
            }
            else
            {
                return View();
            }
        }
        public IActionResult Edit(int Id)
        {
            var blog = BlogService.GetById(Id);
            var ViewModel = new BlogViewModel()
            {
                Id = Id,
                Title = blog.Object.Title,
                Body = blog.Object.Body,
                AuthorId = blog.Object.AuthorID,
                CreatedDate = blog.Object.Created
            };

            var blogs = BlogService.Get().ToList();
            ViewBag.authors = blogs.Where((x, i) => blogs.FindIndex(nameof => nameof.AuthorID == x.AuthorID) == i).Select(x => new {
                AuthorID = x.AuthorID,
                FullName = x.Author.FirstName + " " + x.Author.LastName
            }).ToArray();
            return View(ViewModel);
        }

        public IActionResult onPostEdit(Blog model)
        {
            var result = BlogService.CreateUpdate(model);
            if (result.Success)
            {
                return RedirectToAction(nameof(this.Index));
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            if (Id != 0)
            {
                var res = BlogService.DeleteById(Id);
                return RedirectToAction(nameof(this.Index));
            }
            else
            {

                return View();
            }
        }
    }
}
