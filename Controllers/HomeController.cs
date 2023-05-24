using LogisticsMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using LogisticsMS.Migrations;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
					.Where(s => s.state == 1)
					.OrderBy(s => s.DeliveryDate)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				if (shippingOrders.IsNullOrEmpty())
				{
					return View("Dispatch", shippingOrders);
				}

				if (shippingOrders[0].DeliveryDate == DateTime.MinValue)
				{
					shippingOrders[0].DeliveryDate = DateTime.Now;
				}

				ViewBag.PageNumber = page;
				ViewBag.PageCount = (int)Math.Ceiling((double)db.ShippingOrders
					.Where(o => o.state == 1)
					.Count() / pageSize);

				return View("Dispatch", shippingOrders);

			}
		}

		public IActionResult Transport()
		{
			return View();
		}

		public IActionResult Finance(DateTime startDate, DateTime endDate, string Shippers, string Delivery, int page = 1)
		{
			ViewBag.startDate = startDate;
			ViewBag.endDate = endDate;
			ViewBag.shippers = Shippers;
			ViewBag.delivery = Delivery;

			using (var db = new SqlContext())
			{
				int pageSize = 1;
				List<Statistics> results = (from so in db.ShippingOrders
											where so.state == 3
											join cc in db.ContainerCargo on so.ContainerCargoId equals cc.Id
											join rf in db.ReimbursementForms on cc.Id equals rf.ContainerCargoId into rfs
											from rf in rfs.DefaultIfEmpty()
											group new { so, cc, rf } by new { so.ContainerCargoId, so.DeliveryPerson, cc.ShippersName, cc.ShipmentName, so.DeliveryDate, cc.ContainersCost } into g
											select new Statistics
											{
												ContainerCargoId = g.Key.ContainerCargoId,
												DeliveryPerson = g.Key.DeliveryPerson,
												ShippersName = g.Key.ShippersName,
												ShipmentName = g.Key.ShipmentName,
												DeliveryDate = g.Key.DeliveryDate,
												ContainersCost = g.Key.ContainersCost,
												AllCost = g.Sum(x => x.rf != null && x.rf.stage == 2 ? Convert.ToDecimal(x.rf.Amount) : 0).ToString(),
												Profit = (Convert.ToDecimal(g.Key.ContainersCost) - g.Sum(x => x.rf != null && x.rf.stage == 2 ? Convert.ToDecimal(x.rf.Amount) : 0)).ToString()
											})
								.ToList();

				if (!startDate.Equals(null) && startDate != DateTime.MinValue)
				{
					results = results.Where(x => x.DeliveryDate >= startDate).ToList();
				}
				if (!endDate.Equals(null) && endDate != DateTime.MinValue)
				{
					results = results.Where(x => x.DeliveryDate <= endDate).ToList();
				}
				if (!string.IsNullOrEmpty(Shippers))
				{
					results = results.Where(x => x.ShippersName == Shippers).ToList();
				}
				if (!string.IsNullOrEmpty(Delivery))
				{
					results = results.Where(x => x.DeliveryPerson == Delivery).ToList();
				}

				//分页
				ViewBag.PageNumber = page;
				var pagedResults = results.Skip((page - 1) * pageSize).Take(pageSize);

				//计算总页数
				int totalCount = results.Count;
				int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
				ViewBag.PageCount = totalPages;

				return View("Finance", pagedResults.ToList());
			}

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

		public IActionResult Judge(int page = 1)
		{
			using (var db = new SqlContext())
			{
				int pageSize = 1;
				var Judge = db.ReimbursementForms
					.Where(s => s.stage == 1)
					.Skip((page - 1) * pageSize)
					.Take(pageSize)
					.ToList();

				if (Judge.IsNullOrEmpty())
				{
					return View("Judge", Judge);
				}

				ViewBag.PageNumber = page;
				ViewBag.PageCount = (int)Math.Ceiling((double)db.ReimbursementForms
					.Where(o => o.stage == 1)
					.Count() / pageSize);

				return View("Judge", Judge);
			}
		}

		public IActionResult MyOrders(int page = 1)
		{
			string userInfoJson = HttpContext.Session.GetString("UserInfo")!;
			UserRole user = JsonConvert.DeserializeObject<UserRole>(userInfoJson)!;

			using (var db = new SqlContext())
			{
				int pageSize = 1;
				var shippingOrders = db.ShippingOrders
				.Include(o => o.ContainerCargo)
				.Where(o => o.state == 2 && o.UserRoleId == user.Id)
				.OrderBy(o => o.DeliveryDate)
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToList();


				if (shippingOrders.IsNullOrEmpty())
				{
					return View("MyOrders", shippingOrders);
				}

				ViewBag.PageNumber = page;
				//这个是查询所有
				//ViewBag.PageCount = (int)Math.Ceiling((double)db.ShippingOrders.Count() / pageSize);
				ViewBag.PageCount = (int)Math.Ceiling((double)db.ShippingOrders
					.Where(o => o.state == 2 && o.UserRoleId == user.Id).Count() / pageSize);

				return View("MyOrders", shippingOrders);
			}
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

		[HttpPost]
		public ActionResult OrderSumbit()
		{
			var shippingOrderId = Request.Form["ShippingOrder.Id"];
			if (Guid.TryParse(shippingOrderId, out var id))
			{
				using (var db = new SqlContext())
				{
					var shippingOrder = db.ShippingOrders.FirstOrDefault(o => o.Id == id);
					if (shippingOrder != null)
					{
						shippingOrder.state = 3;
						db.SaveChanges();
					}
				}
			}
			return RedirectToAction("MyOrders");
		}

		[HttpPost]
		public ActionResult RFSubmit(ReimbursementForm reimbursementForm)
		{
			using (var db = new SqlContext())
			{
				if (reimbursementForm != null)
				{
					db.ReimbursementForms.Add(reimbursementForm);
					db.SaveChanges();
				}
			}
			return RedirectToAction("Transport");
		}

		[HttpPost]
		public ActionResult JudgeSubmit(ReimbursementForm reimbursementForm, bool IsApproved)
		{
			using (var db = new SqlContext())
			{
				if (IsApproved)
				{
					reimbursementForm.stage = 2;
				}
				else
				{
					reimbursementForm.stage = 3;
				}

				if (reimbursementForm != null)
				{
					db.ReimbursementForms.Update(reimbursementForm);
					db.SaveChanges();
				}
			}
			return RedirectToAction("Judge");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}