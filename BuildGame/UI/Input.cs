using BuildGame.Screens;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.UI;

internal class Input {
  Rectangle rect;
  Rectangle rectText;
  string? text;
  bool isFocus = false;

  public Input(Rectangle rect) {
    this.rect = rect;
    rectText = new(rect.X+10,rect.Y+5,rect.Width - 20, 40);
  }

  public void Update(ref EditLevel edit) {

  }

  public void Draw() {
    DrawRectangleRec(rect, Color.DarkGray);
    DrawRectangleRec(rectText, Color.LightGray);
  }
}