using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Venue
  {
    private int _id;
    private string _name;

    public void SetId(int id){this._id=id;}
    public int GetId(){return this._id;}
    public void SetName(string name){this._name=name;}
    public string GetName(){return this._name;}

    public Venue(string name)
    {
      this.GetName() = name;
    }

    public override bool Equals(System.Object otherVenue)
    {
      if (!(otherVenue is Venue))
      {
          return false;
      }
      else
      {
          Venue newVenue = (Venue) otherVenue;
          bool idEquality = (this.GetId() == newVenue.GetId());
          bool nameEquality = (this.GetName() == newVenue.GetName());
          return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }
  }
}
