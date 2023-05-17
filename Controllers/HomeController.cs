using LogisticsMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace LogisticsMS.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;

		}

		public IActionResult Index()
		{
			SqlContext context = new SqlContext();
			UserRole text = new UserRole();
			context.Add(text);
			context.SaveChanges();

			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult LoginSubmit(string username, string password)
		{
			//这种方法也是可行的,如果不想用asp.net的数据绑定就可以这样
			/*
            string username = Request.Form["username"];
            string password = Request.Form["password"];
            */

			//判是传入的账号和密码是否为空
			if (string.IsNullOrEmpty(username) || password.IsNullOrEmpty())
			{
				ModelState.AddModelError("username or password", "账号或者密码错误.");
			}

			//连接数据库,判断传入的账号和密码是否正确

			if (ModelState.IsValid)
			{
				// 处理登录成功
				return View("Index");
			}
			else
			{
				// 返回包含错误消息的视图
				return View("Login");
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}