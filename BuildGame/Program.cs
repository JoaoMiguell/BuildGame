using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.Screens;
namespace BuildGame;

internal class Program {
  static void Main(string[] args) {
    SetConfigFlags(ConfigFlags.VSyncHint);
    InitWindow(screenW, screenH, "Build Game");

    ScreenState state = ScreenState.MainMenu;

    Game game = new();
    MainMenu mainMenu = new MainMenu();
    bool pause = false;
    while(!WindowShouldClose()) {
      switch(state) {
        case ScreenState.MainMenu: {
          mainMenu.Update(ref game);
          if(!game.isEmpty) {
            state = ScreenState.Game;
          }
          BeginDrawing();
          ClearBackground(Color.Black);
          mainMenu.Draw();
          DrawFPS(10, 10);
          EndDrawing();
        }
        break;

        case ScreenState.Game: {
          float deltaTime = GetFrameTime();
          if(IsKeyPressed(KeyboardKey.F1))
            pause = !pause;

          if(!pause) {
            game.Update(deltaTime);

            BeginDrawing();
            ClearBackground(Color.Black);
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
      }
    }
    CloseWindow();
    return;
  }
}

