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
      VenueBand model = new VenueBand();
      return View(model);
    }

    [HttpPost("/delete/{venueId}")]
    public ActionResult DeleteOneVenue(int venueId)
    {
      Venue.DeleteOne(venueId);
      VenueBand model = new VenueBand();
      return View("Index",model);
    }

    [HttpPost("/add_venue")]
    public ActionResult AddVenue()
    {
      if ( !String.IsNullOrEmpty(Request.Form["venue-name"]) )
      {
        Venue newVenue = new Venue(Request.Form["venue-name"]);
        newVenue.Save();
      }
      VenueBand model = new VenueBand();
      return View("Index",model);
    }

    [HttpPost("/update/{venueId}")]
    public ActionResult UpdateVenue(int venueId)
    {
      VenueBand model = new VenueBand(Venue.Find(venueId));
      return View(model);
    }

    [HttpPost("/updated/{venueId}")]
    public ActionResult UpdatedList(int venueId)
    {
      if  ( !String.IsNullOrEmpty(Request.Form["new-name"]) )
      { Venue.Find(venueId).Update(Request.Form["new-name"]); }
      int val = 0;
      if  (Int32.TryParse(Request.Form["new-client"], out val) && val >0)
      {
        Venue.Find(venueId).AddBand(Int32.Parse((Request.Form["new-client"])));
      }
      VenueBand model = new VenueBand();
      return View("Index",model);
    }

    [HttpPost("/delete_venues")]
    public ActionResult DeleteAllVenues()
    {
      Venue.DeleteAll();
      VenueBand model = new VenueBand();
      return View("Index",model);
    }

    [HttpGet("/clientList/{venueId}")]
    public ActionResult VenuesClients(int venueId)
    {
      VenueBand model = new VenueBand(Venue.Find(venueId));
      return View(model);
    }

    [HttpPost("/clientList/{venueId}/add_band")]
    public ActionResult AddBand(int venueId)
    {
      if ( !String.IsNullOrEmpty(Request.Form["band-name"]) )
      {
        Band newBand = new Band(Request.Form["band-name"]);
        newBand.Save();
        newBand.AddHost(venueId);
      }
      VenueBand model = new VenueBand(Venue.Find(venueId));
      return View("VenuesClients",model);
    }

    [HttpPost("/clientList/{venueId}/delete_bands")]
    public ActionResult DeleteVenuesBands(int venueId)
    {
      Band.DeleteAll();
      VenueBand model = new VenueBand();
      return View("VenuesClients",model);
    }

    [HttpGet("/clientList/{venueId}/clientDetails/{bandId}/")]
    public ActionResult ClientDetails(int venueId,int bandId)
    {
      VenueBand model = new VenueBand(Venue.Find(venueId),Band.Find(bandId));
      return View(model);
    }

  }
}
