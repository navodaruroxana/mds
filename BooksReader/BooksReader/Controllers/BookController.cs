using BooksReader.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
	public class BookController : Controller
	{
		private ApplicationDbContext DBCtx = new ApplicationDbContext();

		public ActionResult Index(int? bookId)
		{
			Book book = new Book();
			ViewBag.Rating = null;
			ViewBag.BookStatuses = null;
			ViewBag.SelectedStatusId = null;
			if (bookId.HasValue && bookId.Value > 0)
			{
				book = DBCtx.Books.FirstOrDefault(x => x.Id == bookId.Value);

				if (User.Identity.IsAuthenticated)
				{
					string userId = User.Identity.GetUserId();
					UserBookRating rating = DBCtx.UserBookRatings.FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);
					if (rating != null)
						ViewBag.Rating = rating.Rating;

					UserBookStatus bookStatus = DBCtx.UserBookStatuses.Include("BookStatus").FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);

					ViewBag.BookStatuses = DBCtx.BookStatuses.Select(item => new { StatusName = item.Name, StatusId = item.Id }).Distinct().ToDictionary(obj => obj.StatusName, obj => obj.StatusId);
					if (bookStatus != null)
						ViewBag.SelectedStatusId = bookStatus.BookStatus.Id;
				}
			}
			return View(book);
		}

		public ActionResult GiveRating(int? givenRating, int? bookId)
		{
			Book book = null;
			UserBookRating rating = null;
			ViewBag.Rating = null;
			ViewBag.BookStatuses = null;
			ViewBag.SelectedStatusId = null;
			if (bookId.HasValue && bookId.Value > 0)
			{
				book = DBCtx.Books.FirstOrDefault(x => x.Id == bookId.Value);

				if (User.Identity.IsAuthenticated)
				{
					string userId = User.Identity.GetUserId();
					rating = DBCtx.UserBookRatings.FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);
					if (rating != null)
						ViewBag.Rating = rating.Rating;

					UserBookStatus bookStatus = DBCtx.UserBookStatuses.Include("BookStatus").FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);

					ViewBag.BookStatuses = DBCtx.BookStatuses.Select(item => new { StatusName = item.Name, StatusId = item.Id }).Distinct().ToDictionary(obj => obj.StatusName, obj => obj.StatusId);
					if (bookStatus != null)
						ViewBag.SelectedStatusId = bookStatus.BookStatus.Id;
				}

			}

			if (User.Identity.IsAuthenticated)
			{
				if (givenRating.HasValue && givenRating.Value > 0 && book != null)
				{
					if (rating == null)
					{
						rating = new UserBookRating();
						rating.Book = book;
						string userId = User.Identity.GetUserId();
						rating.User = DBCtx.Users.FirstOrDefault(user => user.Id == userId);
						rating.Rating = givenRating.Value;

						book.RatingCount += 1;
						DBCtx.UserBookRatings.Add(rating);
						DBCtx.SaveChanges();
						ViewBag.Rating = givenRating.Value;
					}
					else if (rating.Rating != givenRating.Value)
					{
						rating.Rating = givenRating.Value;
						DBCtx.SaveChanges();
						ViewBag.Rating = givenRating.Value;
					}
				}
			}

			if (book == null)
				book = new Book();

			return View("Index", book);
		}


		public ActionResult ChangeStatus(int? status, int? bookId)
		{
			Book book = null;
			UserBookRating rating = null;
			UserBookStatus bookStatus = null;
			ViewBag.Rating = null;
			ViewBag.BookStatuses = null;
			ViewBag.SelectedStatusId = null;
			if (bookId.HasValue && bookId.Value > 0)
			{
				book = DBCtx.Books.FirstOrDefault(x => x.Id == bookId.Value);

				if (User.Identity.IsAuthenticated)
				{
					string userId = User.Identity.GetUserId();
					rating = DBCtx.UserBookRatings.FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);
					if (rating != null)
						ViewBag.Rating = rating.Rating;

					bookStatus = DBCtx.UserBookStatuses.Include("BookStatus").FirstOrDefault(item => item.User.Id == userId && item.Book.Id == bookId.Value);

					ViewBag.BookStatuses = DBCtx.BookStatuses.Select(item => new { StatusName = item.Name, StatusId = item.Id }).Distinct().ToDictionary(obj => obj.StatusName, obj => obj.StatusId);
					if (bookStatus != null)
						ViewBag.SelectedStatusId = bookStatus.BookStatus.Id;
				}

			}

			if (User.Identity.IsAuthenticated)
			{
				if (status.HasValue && status.Value > 0 && book != null)
				{
					if (bookStatus == null)
					{
						bookStatus = new UserBookStatus();
						bookStatus.Book = book;
						string userId = User.Identity.GetUserId();
						bookStatus.User = DBCtx.Users.FirstOrDefault(user => user.Id == userId);
						bookStatus.BookStatus = DBCtx.BookStatuses.FirstOrDefault(item => item.Id == status.Value);

						DBCtx.UserBookStatuses.Add(bookStatus);
						DBCtx.SaveChanges();
						ViewBag.SelectedStatusId = status.Value;
					}
					else if (bookStatus.BookStatus.Id != status.Value)
					{
						bookStatus.BookStatus = DBCtx.BookStatuses.FirstOrDefault(item => item.Id == status.Value);
						DBCtx.SaveChanges();
						ViewBag.SelectedStatusId = status.Value;
					}
				}
			}

			if (book == null)
				book = new Book();

			return View("Index", book);
		}




	}
}