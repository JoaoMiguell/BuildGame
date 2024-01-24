using BuildGame.Types.Enums;
using Raylib_cs;
namespace BuildGame.Types;

internal class Cel {
  public Rectangle rect;
  public CelType CelType;

  public Cel(Rectangle rect,CelType type) {
    this.rect = rect;
    CelType = type;
  }
}

