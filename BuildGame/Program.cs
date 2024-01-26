using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.Screens;
namespace BuildGame;

internal class Program {
  static void Main(string[] args) {
    SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
    InitWindow(screenW, screenH, "Build Game");

    ScreenState state = ScreenState.MainMenu;

    Game game = new Game($@"{Directory.GetCurrentDirectory()}\Levels\base.txt");
    MainMenu mainMenu = new MainMenu();
    bool pause = false;
    while(!WindowShouldClose()) {
      switch(state) {
        case ScreenState.Game: {
          float deltaTime = GetFrameTime();
          if(IsKeyPressed(KeyboardKey.KEY_F1)) pause = !pause;

          if(!pause) {
            game.Update(deltaTime);

            BeginDrawing();
            ClearBackground(Color.BLACK);
            game.Draw();
            DrawFPS(10, 10);
            EndDrawing();
          }
          else {
            BeginDrawing();
            EndDrawing();
          }
        }
        break;
        case ScreenState.MainMenu: {
          mainMenu.Update();
          if(IsKeyPressed(KeyboardKey.KEY_F1))
            state = ScreenState.Game;

          BeginDrawing();
          ClearBackground(Color.BLACK);
          mainMenu.Draw();
          DrawFPS(10, 10);
          EndDrawing();
        }
        break;
      }
    }
    CloseWindow();
    return;
  }
}

