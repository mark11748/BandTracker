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




  }
}
