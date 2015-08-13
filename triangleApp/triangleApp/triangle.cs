using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace triangleApp
{
  public class triangle
  {
    int side1;
    int side2;
    int side3;

    public triangle(string first, string second, string third)
    {
      try
      {
        side1 = int.Parse(first);
        side2 = int.Parse(second);
        side3 = int.Parse(third);
      }
      catch { }
    }

    public double getTriangleSquare()  //площадь треугольника, принимает на вход длину каждой из сторон
    {
      var prmtr = (side1 + side2 + side3) / 2;
      return Math.Sqrt(prmtr * (prmtr - side1) * (prmtr - side2) * (prmtr - side3));
    }
  }
}
