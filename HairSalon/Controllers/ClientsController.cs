using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
      Stylist myStylist = Stylist.Find(stylistId);
      return View(myStylist);
    }
  }
}
