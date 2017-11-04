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

    public Band(string name , int id=0)
    {
      this.SetName(name);
      this.SetId(id);
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

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"INSERT INTO bands (name) VALUES (@name);";

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
      List<Band> allBands = new List<Band>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM bands;";

      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int    bandNumb = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        allBands.Add(new Band(bandName,bandNumb));
      }
      conn.Close();
      if (conn != null)
      {conn.Dispose();}

      return allBands;
    }

    public static Band Find(int id)
    {
      Band searchResult = new Band("ERR",-1);

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM bands WHERE id=@id;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@id";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int    bandNumb = rdr.GetInt32(0);
        string bandName = rdr.GetString(1);
        searchResult = new Band(bandName,bandNumb);
      }
      conn.Close();
      if (conn != null)
      {conn.Dispose();}

      return searchResult;
    }

    public void Update(string newName)
    {
      if(!String.IsNullOrEmpty(newName))
      {this.SetName(newName);}

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"UPDATE bands SET name=@name WHERE id=@id;";

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
      cmd.CommandText = @"DELETE FROM bands; DELETE FROM bands_venues; ALTER TABLE bands AUTO_INCREMENT = 1;";

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
      cmd.CommandText = @"DELETE FROM bands WHERE id=@id; DELETE FROM bands_venues WHERE band_id=@id;";

      MySqlParameter targetId = new MySqlParameter();
      targetId.ParameterName = "@id";
      targetId.Value = id;
      cmd.Parameters.Add(targetId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {conn.Dispose();}
    }

    public void AddHost(int hostId)
    {
      if (hostId>0)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"INSERT INTO bands_venues (band_id , venue_id) VALUES (@bandId , @venueId);";

        MySqlParameter band = new MySqlParameter();
        band.ParameterName = "@bandId";
        band.Value = this.GetId();
        cmd.Parameters.Add(band);
        MySqlParameter hostVenue = new MySqlParameter();
        hostVenue.ParameterName = "@venueId";
        hostVenue.Value = hostId;
        cmd.Parameters.Add(hostVenue);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {conn.Dispose();}
      }
    }

    public List<Venue> GetVenueList()//!!!
    {
      List<Venue> venueList = new List<Venue>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM bands_venues JOIN venues ON(bands_venues.venue_id=venues.id) WHERE bands_venues.band_id=@bandId;";

      MySqlParameter bandId = new MySqlParameter();
      bandId.ParameterName = "@bandId";
      bandId.Value = this.GetId();
      cmd.Parameters.Add(bandId);

      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int    venueNumb = rdr.GetInt32(3);
        string venueName = rdr.GetString(4);
        venueList.Add(new Venue(venueName,venueNumb));
      }
      conn.Close();
      if (conn != null)
      {conn.Dispose();}

      return venueList;
    }

    public void RemoveVenue(int venueId)
    {
      if (venueId>0)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        MySqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"DELETE FROM bands_venues JOIN venues ON(bands_venues.venue_id=venues.id) WHERE bands_venues.band_id=@bandId AND bands_venues.venue_id=@hostId;";

        MySqlParameter targetBand = new MySqlParameter();
        targetBand.ParameterName = "@bandId";
        targetBand.Value = this.GetId();
        cmd.Parameters.Add(targetBand);
        MySqlParameter sourceHost = new MySqlParameter();
        sourceHost.ParameterName = "@hostId";
        sourceHost.Value = venueId;
        cmd.Parameters.Add(sourceHost);

        cmd.ExecuteNonQuery();

        conn.Close();
        if (conn != null)
        {conn.Dispose();}
      }
    }

  }
}
