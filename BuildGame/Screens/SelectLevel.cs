using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
namespace BuildGame.Screens;

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
      if(CheckCollisionPointRec(GetMousePosition(), level.rect) && IsMouseButtonPressed(MouseButton.Left)) {
        game = new(level.path);
      }
    }
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
  }
}

