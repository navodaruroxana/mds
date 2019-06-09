using BooksReader.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
	public class ProfileController : Controller
	{
		public const int Reading = 2;
		public const int WantToRead = 4;
		public const int Read = 3;

		private ApplicationDbContext DBCtx = new ApplicationDbContext();

		public ActionResult Index()
		{
			UserProfile userProfile = null;
			List<IEnumerable<Book>> books = null;
			if (User.Identity.IsAuthenticated)
			{
				string userId = User.Identity.GetUserId();
				userProfile = DBCtx.UserProfiles.Where(item => item.UserId == userId).FirstOrDefault();

				IEnumerable<Book> booksToRead = DBCtx.Books.Where(item => DBCtx.UserBookStatuses.Where(bookSt => bookSt.Book.Id == item.Id && bookSt.User.Id == userId && bookSt.BookStatus.Id == WantToRead).Count() > 0).ToList();
				IEnumerable<Book> booksRead = DBCtx.Books.Where(item => DBCtx.UserBookStatuses.Where(bookSt => bookSt.Book.Id == item.Id && bookSt.User.Id == userId && bookSt.BookStatus.Id == Read).Count() > 0).ToList();
				IEnumerable<Book> booksReading = DBCtx.Books.Where(item => DBCtx.UserBookStatuses.Where(bookSt => bookSt.Book.Id == item.Id && bookSt.User.Id == userId && bookSt.BookStatus.Id == Reading).Count() > 0).ToList();
				books = new List<IEnumerable<Book>>();
				books.Add(booksToRead);
				books.Add(booksReading);
				books.Add(booksRead);

				if (userProfile == null)
				{
					userProfile = new UserProfile();
					userProfile.UserId = userId;
					DBCtx.UserProfiles.Add(userProfile);
					DBCtx.SaveChanges();
				}
			}

			if (userProfile == null || books == null)
			{
				userProfile = new UserProfile();
				books = new List<IEnumerable<Book>>();
			}

			return View(new KeyValuePair<UserProfile, List<IEnumerable<Book>>>(userProfile, books));
		}
	}
}