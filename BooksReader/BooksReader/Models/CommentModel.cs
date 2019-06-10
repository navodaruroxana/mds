using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BooksReader.Models
{
	public class Comment
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public Group Group { get; set; }

		public DateTime? DateAdded { get; set; }

		public string Message { get; set; }
	}
}