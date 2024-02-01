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

  public void Update(ref MainMenuState mainMenuState, ref EditLevel editLevel, ref ScreenState screenState) {
    if(state == SelectEditLevelState.Main) {
      foreach(Levels level in levels) {
        if(CheckCollisionPointRec(GetMousePosition(), level.rect) && IsMouseButtonPressed(MouseButton.Left)) {
          editLevel.Load(level.path);
          screenState = ScreenState.Edit;
        }
      }
    } else {
      if(input == null)
        input = new(new(screenW/2-250,screenH/2 -25,500,50));
      var letter = GetKeyPressed();
      if(letter >= 65 &&  letter <= 90 && MeasureText(input.text,30) + 20 <= input.rect.Width) {
        input.text += Convert.ToChar(letter);
      } else if(letter == 259 && input.text!.Length != 0)
        input.text = input.text!.Remove(input.text.Length - 1);

      if(IsKeyPressed(KeyboardKey.Enter) && input.text!.Length > 0) {
        editLevel.Create(input.text);
        screenState = ScreenState.Edit;
      }
    }

    backButton.Update(ref mainMenuState);
    newLevel.Update(ref state);
  }

  public void Draw() {
    DrawRectangleRec(rect, Color.DarkGray);
    DrawRectangleRec(scrollBar, Color.Gray);
    if(state == SelectEditLevelState.New) {
      input?.Draw();
    } else {
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
    }
    backButton.Draw();
    newLevel.Draw();
  }
}