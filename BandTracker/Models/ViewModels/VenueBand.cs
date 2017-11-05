using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class VenueBand
  {
    private List<Venue>_venues=new List<Venue>();
    private List<Band> _bands=new List<Band>();

    public List<Venue> GetVenues()
    {return _venues;}
    public void SetVenues(List<Venue> vlist)
    {_venues = vlist;}

    public List<Band> GetBands()
    {return _bands;}
    public void SetBands(List<Band> blist)
    {_bands = blist;}

    public VenueBand()
    {
      SetVenues(Venue.GetAll());
      SetBands(Band.GetAll());
    }
    public VenueBand(Venue v)
    {
      _venues.Add(v);
      SetBands(v.GetSchedule());
    }
    public VenueBand(Band b)
    {
      SetVenues(b.GetVenueList());
      _bands.Add(b);
    }
    public VenueBand(Venue v , Band b)
    {
      _venues.Add(v);
      _bands.Add(b);
    }


  }
}
