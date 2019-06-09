using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BooksReader.Models
{
	public class Group
	{
		public int Id { get; set; }

		public ApplicationUser Moderator { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public DateTime CreatedDate { get; set; }

		public string Rules { get; set; }

		public virtual DbSet<ApplicationUser> Users { get; set; }

		public virtual DbSet<ApplicationUser> Comments { get; set; }
	}
	
}