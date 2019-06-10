using BooksReader.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Books.Controllers
{
	public class GroupController : Controller
	{
		private ApplicationDbContext DBCtx = new ApplicationDbContext();

		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				List<Group> groups = DBCtx.Groups.Include("Moderator").ToList();
				List<Tuple<Group, bool>> groupJoinState = new List<Tuple<Group, bool>>();

				string userId = User.Identity.GetUserId();
				List<int> userGroups = DBCtx.UserGroup.Where(item => item.User.Id == userId).Select(item => item.Group.Id).ToList();

				foreach (Group group in groups)
				{
					bool joined = userGroups.Contains(group.Id);
					groupJoinState.Add(new Tuple<Group, bool>(group, joined));
				}
				return View(groupJoinState);
			}

			return View();
		}

		public ActionResult Create(string Name = null, string Description = null, string Rules = null)
		{
			if (User.Identity.IsAuthenticated && ModelState.IsValid && !string.IsNullOrEmpty(Name))
			{
				Group group = new Group();
				group.Name = Name;
				group.Description = Description;
				group.CreatedDate = DateTime.Now;
				group.Rules = Rules;
				string userId = User.Identity.GetUserId();
				group.Moderator = DBCtx.Users.FirstOrDefault(item => item.Id == userId);
				DBCtx.Groups.Add(group);
				DBCtx.SaveChanges();

				List<Group> groups = DBCtx.Groups.Include("Moderator").ToList();
				List<Tuple<Group, bool>> groupJoinState = new List<Tuple<Group, bool>>();
				
				List<int> userGroups = DBCtx.UserGroup.Where(item => item.User.Id == userId).Select(item => item.Group.Id).ToList();

				foreach (Group tempGroup in groups)
				{
					bool joined = userGroups.Contains(tempGroup.Id);
					groupJoinState.Add(new Tuple<Group, bool>(tempGroup, joined));
				}

				return View("Index", groupJoinState);
			}

			return View();
		}

		public ActionResult Edit()
		{
			return View();
		}

		public ActionResult Join(int? groupId)
		{
			if (User.Identity.IsAuthenticated)
			{
				List<Group> groups = DBCtx.Groups.Include("Moderator").ToList();
				List<Tuple<Group, bool>> groupJoinState = new List<Tuple<Group, bool>>();

				string userId = User.Identity.GetUserId();
				List<int> userGroups = DBCtx.UserGroup.Where(item => item.User.Id == userId).Select(item => item.Group.Id).ToList();

				if (groupId.HasValue && groupId > 0 && !userGroups.Contains(groupId.Value))
				{
					UserGroup userGroup = new UserGroup();
					userGroup.Group = DBCtx.Groups.FirstOrDefault(item => item.Id == groupId.Value);
					userGroup.User = DBCtx.Users.FirstOrDefault(item => item.Id == userId);
					DBCtx.UserGroup.Add(userGroup);
					DBCtx.SaveChanges();
					userGroups.Add(groupId.Value);
				}

				foreach (Group group in groups)
				{
					bool joined = userGroups.Contains(group.Id);
					groupJoinState.Add(new Tuple<Group, bool>(group, joined));
				}
				return View("Index", groupJoinState);
			}

			return View("Index");
		}

		public ActionResult Conversation()
		{
			return View();
		}
	}
}