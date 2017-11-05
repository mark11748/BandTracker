using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BandTracker.Models;


namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Venue.DeleteAll();
      Band.DeleteAll();

      Venue venueA = new Venue("venueA");
      venueA.Save();
      Venue venueB = new Venue("venueB");
      venueB.Save();
      Venue venueC = new Venue("venueC");
      venueC.Save();

      Band bandA = new Band("bandA");
      bandA.Save();
      Band bandB = new Band("bandB");
      bandB.Save();
      Band bandC = new Band("bandC");
      bandC.Save();

      venueA.AddBand(bandA.GetId());
      venueA.AddBand(bandB.GetId());
      venueB.AddBand(bandB.GetId());
      venueC.AddBand(bandC.GetId());

      VenueBand model = new VenueBand();
      return View(model);
    }

    [HttpGet("/clientList/{venueId}")]
    public ActionResult VenuesClients(int venueId)
    {
      VenueBand model = new VenueBand(Venue.Find(venueId));
      return View(model);
    }

    [HttpGet("/clientList/{venueId}/clientDetails/{bandId}/")]
    public ActionResult ClientDetails(int venueId,int bandId)
    {
      VenueBand model = new VenueBand(Venue.Find(venueId),Band.Find(bandId));
      return View(model);
    }

  }
}
