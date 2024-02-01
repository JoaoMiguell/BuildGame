using BuildGame.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.UI;

internal class Input {
  public Rectangle rect;
  public string? text;

  public Input(Rectangle rect) {
    this.rect = rect;
  }

  public void Draw() {
    DrawRectangleRec(rect, Color.LightGray);
    DrawText(text, (int)rect.X+5, (int)rect.Y+10, 30, Color.White);
  }
}