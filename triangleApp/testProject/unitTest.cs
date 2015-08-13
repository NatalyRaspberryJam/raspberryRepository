using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using triangleApp;

namespace testProject
{
  [TestClass]
  public class unitTest
  {
    [TestMethod]
    public void testIntegers()
    {
      triangle t = new triangle("3", "4", "5");
        Assert.AreEqual(6, t.getTriangleSquare());
    }

    [TestMethod]
    public void testNegativeInt()
    {
      triangle t = new triangle("-3", "-4", "-5");
        Assert.AreEqual(6, t.getTriangleSquare());
    }

    [TestMethod]
    public void testStrings()
    {
      triangle t = new triangle("2a", "10", "re");
        Assert.AreEqual(0, t.getTriangleSquare());
    }

    [TestMethod]
    public void testDouble()
    {
      triangle t = new triangle("4.35", "7", "10");
        Assert.AreEqual(0, t.getTriangleSquare());
    }

  }
}
