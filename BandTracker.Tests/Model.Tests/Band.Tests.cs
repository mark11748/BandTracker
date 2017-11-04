using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }
    public void Dispose()
    {
      Band.DeleteAll();
      Band.DeleteAll();
    }

    [TestMethod]
    public void Save_SaveBand()
    {
      Band newBand = new Band("test");
      newBand.Save();
    }

    [TestMethod]
    public void GetAll_RetriveAllBands()
    {
      Band newBandA = new Band("test1");
      newBandA.Save();
      Band newBandB = new Band("test2");
      newBandB.Save();
      Band newBandC = new Band("test3");
      newBandC.Save();

      // Console.WriteLine(newBandA.GetName()+" : "+newBandA.GetId()+" | "+Band.GetAll()[0].GetName()+" : "+Band.GetAll()[0].GetId()+" | "+(newBandA.Equals(Band.GetAll()[0])).ToString());
      // Console.WriteLine(newBandA.GetName()+" : "+newBandA.GetId()+" | "+Band.GetAll()[0].GetName()+" : "+Band.GetAll()[0].GetId()+" | "+(newBandA.Equals(Band.GetAll()[0])).ToString());

      Assert.AreEqual(true, Band.GetAll().Count==3);
      Assert.AreEqual(true, Band.GetAll()[0].Equals(newBandA));
      Assert.AreEqual(true, Band.GetAll()[1].Equals(newBandB));
      Assert.AreEqual(true, Band.GetAll()[2].Equals(newBandC));
    }

    [TestMethod]
    public void Find_RetriveBandById()
    {
      Band newBandA = new Band("test1");
      newBandA.Save();
      Band newBandB = new Band("test2");
      newBandB.Save();
      Band newBandC = new Band("test3");
      newBandC.Save();
      Band newBandD = new Band("test1");
      // Console.WriteLine(newBandA.GetName()+" : "+newBandA.GetId()+" | "+Band.GetAll()[0].GetName()+" : "+Band.GetAll()[0].GetId());
      Assert.AreEqual(false, Band.Find(1).Equals(newBandB));
      Assert.AreEqual(false, Band.Find(1).Equals(newBandC));
      Assert.AreEqual(false, Band.Find(1).Equals(newBandD));
      Assert.AreEqual(true , Band.Find(1).Equals(newBandA));
    }

    [TestMethod]
    public void Update_UpdateBand()
    {
      Band oldBand = new Band("test1");
      oldBand.Save();
      Band newBand = new Band("test2");

      Assert.AreEqual(false,newBand.Equals(oldBand));

      oldBand.Update("test2");

      newBand = Band.Find(oldBand.GetId());

      Assert.AreEqual(true ,newBand.Equals(oldBand));
    }

    [TestMethod]
    public void DeleteOne_DeleteOneBand()
    {
      Band newBandA = new Band("test1");
      newBandA.Save();
      Band newBandB = new Band("test2");
      newBandB.Save();
      Band newBandC = new Band("test3");
      newBandC.Save();

      // Console.WriteLine(newBandC.GetName()+" : "+newBandC.GetId()+" | "+Band.GetAll()[1].GetName()+" : "+Band.GetAll()[1].GetId());

      Assert.AreEqual(true, Band.GetAll().Count == 3);
      Band.DeleteOne(newBandB.GetId());

      // Console.WriteLine(newBandC.GetName()+" : "+newBandC.GetId()+" | "+Band.GetAll()[1].GetName()+" : "+Band.GetAll()[1].GetId());

      Assert.AreEqual(true, Band.GetAll().Count == 2);
      Assert.AreEqual(true, Band.GetAll()[1].Equals(newBandC));
    }

    [TestMethod]
    public void AddHost_AssosiateBandWithVenute()
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

      newBandA.AddHost(newVenueA.GetId());
      newBandB.AddHost(newVenueB.GetId());
      newBandB.AddHost(newVenueC.GetId());

      // Console.WriteLine(newBandA.GetName()+" : "+newBandA.GetId()+" : "+newBandA.GetVenueList().Count);
      Assert.AreEqual(true,newBandA.GetVenueList().Count==1);
      Assert.AreEqual(true,newBandB.GetVenueList().Count==2);
      Assert.AreEqual(true,newBandC.GetVenueList().Count==0);
    }

    [TestMethod]
    public void GetVenueList_GetListOfAllVenutesAssosiatedWithBand()
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

      //Bands A&B use the same venues
      newBandA.AddHost(newVenueA.GetId());
      newBandA.AddHost(newVenueB.GetId());
      newBandB.AddHost(newVenueA.GetId());
      newBandB.AddHost(newVenueB.GetId());
      //Bands C&D use differing venues
      newBandC.AddHost(newVenueB.GetId());
      newBandD.AddHost(newVenueC.GetId());

      Assert.AreEqual(newBandA.GetVenueList()[0],newVenueA);
      Assert.AreEqual(newBandA.GetVenueList()[1],newBandB.GetVenueList()[1]);
      Assert.AreEqual(true, newBandC.GetVenueList()[0]!=newBandD.GetVenueList()[0]);
    }


  }
}
