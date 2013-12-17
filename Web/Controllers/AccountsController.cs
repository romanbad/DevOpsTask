using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Config.Dao;
using log4net;

namespace Web.Controllers
{
	public class AccountsController : BootstrapBaseController
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(AccountsController));

		private readonly AccountRepository _accountRepository = new AccountRepository();

		[BasicAuthentication]
		public ActionResult Index()
		{
			var accounts = _accountRepository.All();
			return View(accounts);
		}

		[BasicAuthentication]
		public ActionResult Tp3(string id)
		{
			Information("TP3 acess provided for " + id + ", button will appear in the next few minutes.");
			return RedirectToAction("Index");
		}
	}
}