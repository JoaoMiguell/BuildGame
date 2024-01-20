using BuildGame.Types.Enums;
using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace BuildGame.Types;

internal class Cel {
  public Rectangle cords;
  public CelType CelType;

  public Cel(Rectangle cords,CelType type) {
    this.cords = cords;
    CelType = type;
  }
}

