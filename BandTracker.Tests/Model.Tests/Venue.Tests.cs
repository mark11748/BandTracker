using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Venue.DeleteAll();
    }

    [TestMethod]
    public void Save_SaveVenue()
    {
      Venue newVenue = new Venue("test");
      newVenue.Save();
    }

    [TestMethod]
    public void GetAll_RetriveAllVenues()
    {
      Venue newVenueA = new Venue("test1");
      newVenueA.Save();
      Venue newVenueB = new Venue("test2");
      newVenueB.Save();
      Venue newVenueC = new Venue("test3");
      newVenueC.Save();

      // Console.WriteLine(newVenueA.GetName()+" : "+newVenueA.GetId()+" | "+Venue.GetAll()[0].GetName()+" : "+Venue.GetAll()[0].GetId()+" | "+(newVenueA.Equals(Venue.GetAll()[0])).ToString());
      // Console.WriteLine(newVenueA.GetName()+" : "+newVenueA.GetId()+" | "+Venue.GetAll()[0].GetName()+" : "+Venue.GetAll()[0].GetId()+" | "+(newVenueA.Equals(Venue.GetAll()[0])).ToString());

      Assert.AreEqual(true, Venue.GetAll().Count==3);
      Assert.AreEqual(true, Venue.GetAll()[0].Equals(newVenueA));
      Assert.AreEqual(true, Venue.GetAll()[1].Equals(newVenueB));
      Assert.AreEqual(true, Venue.GetAll()[2].Equals(newVenueC));
    }

    [TestMethod]
    public void Find_RetriveVenueById()
    {
      Venue newVenueA = new Venue("test1");
      newVenueA.Save();
      Venue newVenueB = new Venue("test2");
      newVenueB.Save();
      Venue newVenueC = new Venue("test3");
      newVenueC.Save();
      Venue newVenueD = new Venue("test1");
      // Console.WriteLine(newVenueA.GetName()+" : "+newVenueA.GetId()+" | "+Venue.GetAll()[0].GetName()+" : "+Venue.GetAll()[0].GetId());
      Assert.AreEqual(false, Venue.Find(1).Equals(newVenueB));
      Assert.AreEqual(false, Venue.Find(1).Equals(newVenueC));
      Assert.AreEqual(false, Venue.Find(1).Equals(newVenueD));
      Assert.AreEqual(true , Venue.Find(1).Equals(newVenueA));
    }

    [TestMethod]
    public void Update_UpdateVenue()
    {
      Venue oldVenue = new Venue("test1");
      oldVenue.Save();
      Venue newVenue = new Venue("test2");

      Assert.AreEqual(false,newVenue.Equals(oldVenue));

      oldVenue.Update("test2");

      newVenue = Venue.Find(oldVenue.GetId());

      Assert.AreEqual(true ,newVenue.Equals(oldVenue));
    }

    [TestMethod]
    public void DeleteOne_DeleteOneVenue()
    {
      Venue newVenueA = new Venue("test1");
      newVenueA.Save();
      Venue newVenueB = new Venue("test2");
      newVenueB.Save();
      Venue newVenueC = new Venue("test3");
      newVenueC.Save();

      // Console.WriteLine(newVenueC.GetName()+" : "+newVenueC.GetId()+" | "+Venue.GetAll()[1].GetName()+" : "+Venue.GetAll()[1].GetId());

      Assert.AreEqual(true, Venue.GetAll().Count == 3);
      Venue.DeleteOne(newVenueB.GetId());

      // Console.WriteLine(newVenueC.GetName()+" : "+newVenueC.GetId()+" | "+Venue.GetAll()[1].GetName()+" : "+Venue.GetAll()[1].GetId());

      Assert.AreEqual(true, Venue.GetAll().Count == 2);
      Assert.AreEqual(true, Venue.GetAll()[1].Equals(newVenueC));
    }

    [TestMethod]
    public void AddBand_AssosiateVenuteWithBand()
    {
      //Venues
      Venue newVenueA = new Venue("test1");
      newVenueA.Save();
      Venue newVenueB = new Venue("test2");
      newVenueB.Save();
      Venue newVenueC = new Venue("test3");
      newVenueC.Save();
      //Bands
      Band newBandA = new Band("Journey");
      newBandA.Save();
      Band newBandB = new Band("AC/DC");
      newBandB.Save();
      Band newBandC = new Band("Def Leopard");
      newBandC.Save();

      newVenueA.AddBand(newBandA.GetId());
      newVenueB.AddBand(newBandB.GetId());
      newVenueB.AddBand(newBandC.GetId());

      // Console.WriteLine(newVenueA.GetName()+" : "+newVenueA.GetId()+" : "+newVenueA.GetSchedule().Count);
      Assert.AreEqual(true,newVenueA.GetSchedule().Count==1);
      Assert.AreEqual(true,newVenueB.GetSchedule().Count==2);
      Assert.AreEqual(true,newVenueC.GetSchedule().Count==0);
    }

    [TestMethod]
    public void GetSchedule_GetListOfAllVenutesAssosiatedWithBand()
    {
      //Venues
      Venue newVenueA = new Venue("test1");
      newVenueA.Save();
      Venue newVenueB = new Venue("test2");
      newVenueB.Save();
      Venue newVenueC = new Venue("test3");
      newVenueC.Save();
      //Bands
      Band newBandA = new Band("Journey");
      newBandA.Save();
      Band newBandB = new Band("AC/DC");
      newBandB.Save();
      Band newBandC = new Band("Def Leopard");
      newBandC.Save();
      Band newBandD = new Band("Styx");
      newBandD.Save();
      Band newBandE = new Band("AudioSlave");
      newBandE.Save();

      // //Bands A&B use the same venues
      // newBandA.AddHost(newVenueA.GetId());
      // newBandA.AddHost(newVenueB.GetId());
      // newBandB.AddHost(newVenueA.GetId());
      // newBandB.AddHost(newVenueB.GetId());
      // //Bands C&D use differing venues
      // newBandC.AddHost(newVenueB.GetId());
      // newBandD.AddHost(newVenueC.GetId());
      // //Band E has no venues recorded as having hosted them
      //
      // CollectionAssert.AreEqual(newBandA.GetSchedule(),newBandB.GetSchedule());
      // CollectionAssert.AreEqual(newBandC.GetSchedule(),newBandD.GetSchedule());
      // Assert.AreEqual(true,newBandC.GetSchedule().Count==0);



    }



  }
}
