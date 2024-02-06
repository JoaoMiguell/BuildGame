using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.Screens;

namespace BuildGame.Types;

class Option {
  public CelType type;
  public Rectangle rect;
  public Color color;
  public Color textColor;

  public Option(CelType type, Rectangle rect, Color color, Color textColor) {
    this.type = type;
    this.rect = rect;
    this.color = color;
    this.textColor = textColor;
  }
}

class Pencil {
  public CelType type = CelType.Floor;
  List<Option> options = [];

  public Pencil() {
    options.Add(new(CelType.Floor, new(screenW / 2 - 200, screenH / 2 - 80, 200, 160), Color.Gray, Color.Black));
    options.Add(new(CelType.Player, new(screenW / 2, screenH / 2 - 80, 200, 160), Color.Red, Color.White));
  }

  public void Draw() {
    foreach(var option in options) {
      DrawRectangleRec(option.rect, option.color);
      DrawText(option.type.ToString(),
        (int)option.rect.X + MeasureText(option.type.ToString(), 30) - 20,
        (int)option.rect.Y + (int)option.rect.Height / 2 - 15, 30, option.textColor);
    }
  }

  public void Update() {
    foreach(var option in options) {
      if(IsMouseButtonPressed(MouseButton.Left) && CheckCollisionPointRec(GetMousePosition(), option.rect)) {
        type = option.type;
      }
    }
  }
}
