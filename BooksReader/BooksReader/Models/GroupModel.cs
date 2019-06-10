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

		public virtual ICollection<ApplicationUser> Users { get; set; }

		public virtual ICollection<ApplicationUser> Comments { get; set; }
	}

	public class UserGroup
	{
		public int Id { get; set; }

		public ApplicationUser User { get; set; }

		public Group Group { get; set; }
	}

}