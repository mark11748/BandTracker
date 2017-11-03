using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace BandTracker.Models
{
  public class Band
  {
    private int _id;
    private string _name;

    public void SetId(int id){this._id=id;}
    public int GetId(){return this._id;}
    public void SetName(string name){this._name=name;}
    public string GetName(){return this._name;}

    public Band(string name)
    {
      this.GetName() = name;
    }

    public override bool Equals(System.Object otherBand)
    {
      if (!(otherBand is Band))
      {
          return false;
      }
      else
      {
          Band newBand = (Band) otherBand;
          bool idEquality = (this.GetId() == newBand.GetId());
          bool nameEquality = (this.GetName() == newBand.GetName());
          return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
        return this.GetId().GetHashCode();
    }

  }
}
