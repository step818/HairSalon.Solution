using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
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

    [HttpGet("/stylists/{stylistId}/clients/{clientsId}")]
    public ActionResult Show(int clientId)
    {
      Client myClient = Client.Find(clientId);
      return View(myClient);
    }

    [HttpPost("/stylists/{stylistId}/clients/create")]
    public ActionResult Create(string clientName, int stylistId)
    {
      Client myClient = new Client(clientName, stylistId);
      myClient.Save();
      return View("Show", myClient);
    }

  }
}
