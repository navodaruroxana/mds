using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace BooksReader.Controllers
{
	public class HomeController : Controller
	{
		public async System.Threading.Tasks.Task<ActionResult> Index()
		{
			HttpClient client = new HttpClient();

			UriBuilder uri = new UriBuilder("https://www.goodreads.com/search/index.xml");
			NameValueCollection parameters = HttpUtility.ParseQueryString(string.Empty);
			parameters["key"] = "a7Xzks3Cp5M9LxHNgJQ8RA";
			parameters["q"] = "Ender";
			parameters["page"] = "2";
			uri.Query = parameters.ToString();


			var resp = await client.GetAsync(uri.Uri);
			string result = await resp.Content.ReadAsStringAsync();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}