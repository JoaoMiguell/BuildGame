using Raylib_cs;
namespace BuildGame.Types;

internal struct Levels {
  public Rectangle rect;
  public string path;
  public string name;

  public Levels(Rectangle rect, string path, string name) {
    this.rect = rect;
    this.path = path;
    this.name = name;
  }
}

