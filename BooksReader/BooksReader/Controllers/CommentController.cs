using BooksReader.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
	public class CommentController : Controller
	{
		private ApplicationDbContext DBCtx = new ApplicationDbContext();

		public ActionResult Index(int? groupId)
		{
			KeyValuePair<int, List<Comment>> comments;
			if (User.Identity.IsAuthenticated)
			{
				string userId = User.Identity.GetUserId();

				if (groupId.HasValue && groupId.Value > 0)
				{
					bool joinedGroup = DBCtx.UserGroup.FirstOrDefault(item => item.User.Id == userId && item.Group.Id == groupId) != null;
					if (joinedGroup)
					{
						List<Comment> comms = DBCtx.Comments.Include("User").Where(item => item.User.Id == userId && item.Group.Id == groupId).OrderByDescending(item1 => item1.DateAdded).ToList();
						comments = new KeyValuePair<int, List<Comment>>(groupId.Value, comms);
						return View(comments);
					}
				}
			}

			comments = new KeyValuePair<int, List<Comment>>();

			return View(comments);
		}

		public ActionResult SaveMessage(int? groupId, string comment)
		{
			KeyValuePair<int, List<Comment>> comments;
			if (User.Identity.IsAuthenticated)
			{
				string userId = User.Identity.GetUserId();

				if (groupId.HasValue && groupId.Value > 0)
				{
					bool joinedGroup = DBCtx.UserGroup.FirstOrDefault(item => item.User.Id == userId && item.Group.Id == groupId) != null;
					if (joinedGroup)
					{
						List<Comment> comms = DBCtx.Comments.Include("User").Where(item => item.User.Id == userId && item.Group.Id == groupId).OrderByDescending(item1 => item1.DateAdded).ToList();
						comments = new KeyValuePair<int, List<Comment>>(groupId.Value, comms);

						if (!string.IsNullOrEmpty(comment))
						{
							Comment comm = new Comment();
							comm.Message = comment;

							comm.User = DBCtx.Users.FirstOrDefault(item => item.Id == userId);
							comm.Group = DBCtx.Groups.FirstOrDefault(item => item.Id == groupId);

							comm.DateAdded = DateTime.Now;

							DBCtx.Comments.Add(comm);
							DBCtx.SaveChanges();
						}

						return View("Index", comments);
					}	
				}
			}

			comments = new KeyValuePair<int, List<Comment>>();

			return View("Index", comments);
		}
	}
}