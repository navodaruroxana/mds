using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BooksReader.Models
{
	public class UserProfile
	{
		[Key, ForeignKey("User")]
		public string UserId { get; set; }

		public ApplicationUser User { get; set; }

		public string FirstName { get; set; }

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string ImageUrl { get; set; }

		public string Description { get; set; }

		public string Blog { get; set; }

		public string Gender { get; set; }

		public string Interests { get; set; }
	}
}