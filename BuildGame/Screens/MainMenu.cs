using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.UI;
namespace BuildGame.Screens;

internal class MainMenu {
  StartButton StartButton;
  SelectLevel? SelectLevel = new();
  MainMenuState State = MainMenuState.Main;

  public MainMenu() {
    StartButton = new StartButton(new(screenW / 2 - 95, screenH / 2 - 25, 190, 50),
                                      Color.BEIGE, "Start", Color.WHITE, 30);
  }

  public void Update(ref Game game) {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Update(ref State);

      }
      break;
      case MainMenuState.Start: {
        if(SelectLevel == null)
          SelectLevel = new();
        SelectLevel.Update(ref game);
      }
      break;
    }
  }

  public void Draw() {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Draw();
      }
      break;
      case MainMenuState.Start: {
        SelectLevel!.Draw();
      }
      break;
    }
  }
}

struct Levels {
  public Rectangle rect;
  public string path;
  public string name;

  public Levels(Rectangle rect, string path, string name) {
    this.rect = rect;
    this.path = path;
    this.name = name;
  }
}

internal class SelectLevel {
  List<Levels> levels = [];
  Rectangle rect;
  Rectangle scrollBar;

  public SelectLevel() {
    List<string> temp = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Levels").ToList();
    int auxHeight = 10;
    for(int i = 0; i < temp.Count; i++) {
      levels.Add(new(new(10, auxHeight, screenW - 40, 40), temp[i], temp[i].Split('\\').Last()));
      auxHeight += 40;
    }
    rect = new(10, 10, screenW - 30, screenH - 20);
    scrollBar = new(screenW - 30, 10, 20, screenH - 20);
  }

  public void Update(ref Game game) {
    foreach(Levels level in levels) {
      if(CheckCollisionPointRec(GetMousePosition(), level.rect) && IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) {
        game = new(level.path);
      }
    }
  }

  public void Draw() {
    DrawRectangleRec(rect, Color.DARKGRAY);
    DrawRectangleRec(scrollBar, Color.GRAY);
    foreach(Levels level in levels) {
      if(level.rect.Y + level.rect.Height < screenH) {
        if(CheckCollisionPointRec(GetMousePosition(), level.rect)) {
          DrawRectangleRec(level.rect, Color.LIGHTGRAY);
          DrawText(level.name, (int)level.rect.X + 10, (int)level.rect.Y + 10, 20, Color.BLACK);
        }
        else {
          DrawRectangleRec(level.rect, Color.GRAY);
          DrawText(level.name, (int)level.rect.X + 10, (int)level.rect.Y + 10, 20, Color.WHITE);
        }

      }
    }
  }
}