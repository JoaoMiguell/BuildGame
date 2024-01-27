using BuildGame.Types.Enums;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.UI;
internal class StartButton : Button {
  public StartButton(Rectangle rect, Color rectColor, string text, Color textColor, int textSize) 
    : base(rect, rectColor, text, textColor, textSize) {}

  public void Update(ref MainMenuState state) {
    if(IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) 
      && CheckCollisionPointRec(GetMousePosition(),rect)) {
      state = MainMenuState.Start;
    }
  }
}
