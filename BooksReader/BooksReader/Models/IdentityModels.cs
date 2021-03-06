﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BooksReader.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

		public virtual ICollection<Group> Groups { get; set; }

		public virtual ICollection<Comment> Comments { get; set; }
	}

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public DbSet<Book> Books { get; set; }

		public DbSet<Group> Groups { get; set; }

		public DbSet<Comment> Comments { get; set; }

		public DbSet<UserProfile> UserProfiles { get; set; }

		public DbSet<BookStatus> BookStatuses { get; set; }

		public DbSet<UserBookStatus> UserBookStatuses { get; set; }

		public DbSet<UserBookRating> UserBookRatings { get; set; }

		public DbSet<UserBookReview> UserBookReviews { get; set; }
		
		public DbSet<UserGroup> UserGroup { get; set; }
	}
}