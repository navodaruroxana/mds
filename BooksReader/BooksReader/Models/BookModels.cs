using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksReader.Models
{
	public class Book
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Author { get; set; }

		public string Description { get; set; }

		public string Genres { get; set; }

		public int? Pages { get; set; }

		public decimal? Rating { get; set; }

		public Int64? RatingCount { get; set; }

		public Int64? ReviewCount { get; set; }

		public string ImageUrl { get; set; }
	}

	public class BookStatus
	{
		public int Id { get; set; }

		public string Name { get; set; }
	}

	public class UserBookStatus
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public Book Book { get; set; }

		public BookStatus BookStatus { get; set; }
	}

	public class UserBookRating
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public Book Book { get; set; }

		public int Rating { get; set; }
	}

	public class UserBookReview
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public Book Book { get; set; }

		public string Review { get; set; }
	}
}