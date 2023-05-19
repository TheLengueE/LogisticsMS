using LogisticsMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LogisticsMS.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;

		}

		public IActionResult Business()
		{
			return View();
		}

		public IActionResult Dispatch()
		{
			return View();
		}

		public IActionResult Transport()
		{
			return View();
		}

		public IActionResult Finance()
		{
			return View();

		}

		public IActionResult Index()
		{
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

			//判断是传入的账号和密码是否为空
			if (string.IsNullOrEmpty(username) || password.IsNullOrEmpty())
			{
				ModelState.AddModelError("username or password", "账号或者密码错误.");
			}

			//连接数据库,判断传入的账号和密码是否正确
			SqlContext db = new SqlContext();

			var user = db.UserRoles.FirstOrDefault(u => u.account == username && u.password == password);

			if (user == null)
			{
				ModelState.AddModelError("username or password", "账号或者密码错误.");
				return View("Login");
			}
			else
			{
				// 处理登录成功
				string userInfoJson = JsonConvert.SerializeObject(user, Formatting.Indented);
				HttpContext.Session.SetString("UserInfo", userInfoJson);
			}
			return View("Index");
		}

		[HttpPost]
		public ActionResult UpdateProfile(int userId, string PhoneNumber, string Email, string Password)
		{
			using (var db = new SqlContext())
			{
				// 查询现有用户
				var userinfo = db.UserRoles.FirstOrDefault(u => u.Id == userId);
				if (userinfo != null)
				{
					// 更新用户资料
					if (PhoneNumber != null) userinfo.Phone = PhoneNumber;
					if (Email != null) userinfo.Email = Email;
					if (Password != null) userinfo.password = Password;
					db.SaveChanges();

					// 更新会话
					string userInfoJson = JsonConvert.SerializeObject(userinfo, Formatting.Indented);
					HttpContext.Session.SetString("UserInfo", userInfoJson);
				}
			}

			return View("Index");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}