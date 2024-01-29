using BuildGame.Types.Enums;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.UI;
internal class CloseButton : Button {
  public CloseButton(Rectangle rect, Color rectColor, string text, Color textColor, int textSize)
    : base(rect, rectColor, text, textColor, textSize) { }

  public void Update(ref bool exit) {
    if(IsMouseButtonPressed(MouseButton.Left)
      && CheckCollisionPointRec(GetMousePosition(), rect)) {
      exit = true;
    }
  }
}

