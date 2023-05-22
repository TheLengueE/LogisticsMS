using LogisticsMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

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

		public IActionResult Dispatch(int page = 1)
		{
			using (var db = new SqlContext())
			{
				int pageSize = 1;
				var shippingOrders = db.ShippingOrders
					.Include(s => s.ContainerCargo)
					.OrderBy(s => s.DeliveryDate)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				shippingOrders[0].DeliveryDate = DateTime.Now;

				ViewBag.PageNumber = page;
				ViewBag.PageCount = (int)Math.Ceiling((double)db.ShippingOrders.Count() / pageSize);
				return View("Dispatch", shippingOrders);

			}
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
			using (SqlContext db = new SqlContext())
			{
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

		[HttpPost]
		public ActionResult BusinessSubmit(ContainerCargo containerCargo)
		{
			using (var db = new SqlContext())
			{
				if (containerCargo != null)
				{
					//运货单表进行添加
					containerCargo.Id = Guid.NewGuid();
					db.ContainerCargo.Add(containerCargo);
					db.SaveChanges();

					//调度单表添加
					ShippingOrders orders = new ShippingOrders();
					orders.Id = Guid.NewGuid();
					orders.ContainerCargoId = containerCargo.Id;
					//orders.ContainerCargo = containerCargo;
					orders.state = 1;
					db.ShippingOrders.Add(orders);
					db.SaveChanges();
				}
			}

			return View("Business");
		}

		[HttpPost]
		public ActionResult DispatchSubmit(ShippingOrders ShippingOrder)
		{
			using (var db = new SqlContext())
			{
				if (ShippingOrder != null)
				{
					ShippingOrder.state = 2;
					var deliveryPerson = db.UserRoles.FirstOrDefault(u => u.Name == ShippingOrder.DeliveryPerson &&
					u.Phone == ShippingOrder.DeliveryPhone);
					if (deliveryPerson != null)
					{
						ShippingOrder.UserRoleId = deliveryPerson.Id;
					}
					db.ShippingOrders.Update(ShippingOrder);
					db.SaveChanges();
				}
			}
			//这种做法规范得多
			return RedirectToAction("Dispatch");
			//return View("Dispatch");
			//如果你要返回一个视图请想想它是否需要额外的参数，在这它还需要额外的数据
			//在这你需要        return View("Dispatch", shippingOrders);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}