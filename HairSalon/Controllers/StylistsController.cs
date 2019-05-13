using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpGet("/stylists/{id}")]
    public ActionResult Show(int id)
    {
      Stylist myStylist = Stylist.Find(id);
      return View(myStylist);
    }

    [HttpPost("/stylists/create")]
    public ActionResult Create(string name)
    {
      Stylist myStylist = new Stylist(name);
      myStylist.Save();
      //List<Stylist> allStylists = Stylist.GetAll();
      return RedirectToAction("Index");
    }

  }
}
