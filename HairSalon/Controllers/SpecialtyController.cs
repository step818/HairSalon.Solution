using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{

    public class SpecialtyController : Controller
    {

      [HttpGet("/specialties")]
      public ActionResult Index()
      {
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View(allSpecialtys);
      }

      [HttpGet("/specialties/new")]
      public ActionResult New()
      {
      return View();
      }
      //
      [HttpPost("/specialties")]
      public ActionResult Create(string type)
      {
      Specialty newSpecialty = new Specialty(type);
      newSpecialty.Save();
      List<Specialty> allSpecialtys = Specialty.GetAll();
      return View("Index", allSpecialtys);
      }

      [HttpGet("/specialties/{id}")]
      public ActionResult Show(int id)
      {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Specialty selectedSpecialty = Specialty.Find(id);
          List<Stylist> specialtyStylists = selectedSpecialty.GetStylists();
          List<Stylist> allStylists = Stylist.GetAll();
          model.Add("specialty", selectedSpecialty);
          model.Add("specialtyStylists", specialtyStylists);
          model.Add("allStylists", allStylists);
          return View(model);
      }

      [HttpPost("/specialties/{specialtyId}/stylists/new")]
      public ActionResult AddStylist(int specialtyId, int stylistId)
      {
        Specialty specialty = Specialty.Find(specialtyId);
        Stylist stylist = Stylist.Find(stylistId);
        specialty.AddStylist(stylist);
        return RedirectToAction("Show",  new { id = specialtyId });
      }

    }
}
