using BooksReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BooksReader.Controllers
{
	public class HomeController : Controller
	{
		private ApplicationDbContext DBCtx = new ApplicationDbContext();

		public ActionResult Index(string searchInput = null)
		{
			List<Book> books = new List<Book>();

			if (string.IsNullOrWhiteSpace(searchInput))
				books = DBCtx.Books.Take(15).ToList();
			else
			{
				books = DBCtx.Books.Where(item => item.Title.Contains(searchInput) || item.Author.Contains(searchInput)).Take(15).ToList();
			}

			return View(books);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}