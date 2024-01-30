using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
using BuildGame.UI;
using BuildGame.Types.Enums;
namespace BuildGame.Screens;

internal class BackButton : Button {
  public BackButton(Rectangle rect, Color rectColor, string text, Color textColor, int textSize)
    : base(rect, rectColor, text, textColor, textSize) {}

  public void Update(ref MainMenuState state) {
    if(IsMouseButtonPressed(MouseButton.Left)
      && CheckCollisionPointRec(GetMousePosition(), rect)) {
      state = MainMenuState.Main;
    }
  }
}

internal class SelectLevel {
  List<Levels> levels = [];
  Rectangle rect;
  Rectangle scrollBar;
  BackButton backButton;

  public SelectLevel() {
    List<string> temp = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Levels").ToList();
    int auxHeight = 10;
    for(int i = 0; i < temp.Count; i++) {
      levels.Add(new(new(10, auxHeight, screenW - 40, 40), temp[i], temp[i].Split('\\').Last()));
      auxHeight += 40;
    }
    rect = new(10, 10, screenW - 30, screenH - 80);
    scrollBar = new(screenW - 30, 10, 20, screenH - 20);
    backButton = new(new(screenW / 2 - 95, screenH - 60, 190, 50),
                         Color.Blue, "Back", Color.White, 30);
  }

  public void Update(ref Game game, ref MainMenuState mainMenuState) {
    foreach(Levels level in levels) {
      if(CheckCollisionPointRec(GetMousePosition(), level.rect) && IsMouseButtonPressed(MouseButton.Left)) {
        game = new(level.path);
      }
    }

    backButton.Update(ref mainMenuState);
  }

  public void Draw() {
    DrawRectangleRec(rect, Color.DarkGray);
    DrawRectangleRec(scrollBar, Color.Gray);
    foreach(Levels level in levels) {
      if(level.rect.Y + level.rect.Height < screenH) {
        if(CheckCollisionPointRec(GetMousePosition(), level.rect)) {
          DrawRectangleRec(level.rect, Color.LightGray);
          DrawText(level.name, (int)level.rect.X + 10, (int)level.rect.Y + 10, 20, Color.Black);
        }
        else {
          DrawRectangleRec(level.rect, Color.Gray);
          DrawText(level.name, (int)level.rect.X + 10, (int)level.rect.Y + 10, 20, Color.White);
        }

      }
    }
    backButton.Draw();
  }
}

