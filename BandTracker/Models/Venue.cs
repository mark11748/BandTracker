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

    public Venue(string name, int id=0)
    {
      this.SetName(name);
      this.SetId(id);
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO venues (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this.GetName();
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      this.SetId((int)cmd.LastInsertedId);

      conn.Close();
      if (conn != null)
      {conn.Dispose();}
    }

    public static List<Band> GetAll()
    {
      List<Venue> allVenues = new List<Venue>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM venues;";

      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int    venueNumb = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        allVenues.Add(new Venue(venueName,venueNumb));
      }
      conn.Close();
      if (conn != null)
      {conn.Dispose();}

      return allVenues;
    }

    public static Venue Find(int id)
    {
      Venue searchResult = new Venue("ERR",-1);

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM venues WHERE bands.id=@id;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@id";
      searchId.Value = id();
      cmd.Parameters.Add(searchId);

      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int    venueNumb = rdr.GetInt32(0);
        string venueName = rdr.GetString(1);
        searchResult = new Venue(venueName,venueNumb));
      }
      conn.Close();
      if (conn != null)
      {conn.Dispose();}

      return searchResult;
    }

    public void Update(string newName)
    {
      if(!String.IsNullOrEmpty(newName))
      {this.Name = newName;}

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SET venues.name=@name FROM venues WHERE venues.id=@id;";

      MySqlParameter targetName = new MySqlParameter();
      targetName.ParameterName = "@name";
      targetName.Value = this.GetName();
      cmd.Parameters.Add(targetName);
      MySqlParameter targetId = new MySqlParameter();
      targetId.ParameterName = "@id";
      targetId.Value = this.GetId();
      cmd.Parameters.Add(targetId);

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {conn.Dispose();}
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"DELETE FROM venues; ALTER TABLE venues AUTO_INCREMENT = 1;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {conn.Dispose();}
    }
    public static void DeleteOne(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"DELETE FROM venues WHERE venus.id=@id;";

      MySqlParameter targetId = new MySqlParameter();
      targetId.ParameterName = "@id";
      targetId.Value = id;
      cmd.Parameters.Add(targetId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {conn.Dispose();}
    }


  }
}
