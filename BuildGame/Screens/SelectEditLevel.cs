using BuildGame.Types;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.UI;
using BuildGame.Types.Enums;
namespace BuildGame.Screens;

internal enum SelectEditLevelState {
  Main = 0,
  New = 1
}

internal class NewLevelButton : Button {
  public NewLevelButton(Rectangle rect, Color rectColor, string text, Color textColor, int textSize)
    : base(rect, rectColor, text, textColor, textSize) {}

  public void Update(ref SelectEditLevelState state) {
    if(IsMouseButtonPressed(MouseButton.Left)
      && CheckCollisionPointRec(GetMousePosition(), rect)) {
      state = state == SelectEditLevelState.Main ? SelectEditLevelState.New : SelectEditLevelState.Main;
    }
  }
}

internal class SelectEditLevel {
  protected List<Levels> levels = [];
  protected Rectangle rect;
  protected Rectangle scrollBar;
  protected BackButton backButton;
  NewLevelButton newLevel;
  Input input;

  SelectEditLevelState state = SelectEditLevelState.Main;
  public SelectEditLevel() {
    List<string> temp = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Levels").ToList();
    int auxHeight = 10;
    for(int i = 0; i < temp.Count; i++) {
      levels.Add(new(new(10, auxHeight, screenW - 40, 40), temp[i], temp[i].Split('\\').Last()));
      auxHeight += 40;
    }
    rect = new(10, 10, screenW - 30, screenH - 80);
    scrollBar = new(screenW - 30, 10, 20, screenH - 80);
    backButton = new(new(screenW / 2 - 285, screenH - 60, 190, 50),
                     Color.Blue, "Back", Color.White, 30);
    newLevel = new(new(screenW / 2 + 100, screenH - 60, 190, 50),
                     Color.Green, "New", Color.White, 30);
  }

  public void Update(ref MainMenuState mainMenuState) {
    if(state == SelectEditLevelState.Main) {
      foreach(Levels level in levels) {
        if(CheckCollisionPointRec(GetMousePosition(), level.rect) && IsMouseButtonPressed(MouseButton.Left)) {
          Console.WriteLine("Entrou");
        }
      }
    } else {
      Console.WriteLine("teste");
    }

    backButton.Update(ref mainMenuState);
    newLevel.Update(ref state);
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
    newLevel.Draw();
  }
}