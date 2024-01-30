using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.Screens;
namespace BuildGame;

internal class Program {
  static void Main(string[] args) {
    SetConfigFlags(ConfigFlags.VSyncHint);
    SetExitKey(KeyboardKey.F12);
    InitWindow(screenW, screenH, "Build Game");

    ScreenState state = ScreenState.MainMenu;

    Game game = new();
    MainMenu mainMenu = new MainMenu();
    //EditLevel? Edit;
    bool pause = false;
    bool exit = false;
    while(!exit) {
      if(WindowShouldClose() && !IsKeyPressed(KeyboardKey.Escape)) exit = true;

      switch(state) {
        case ScreenState.MainMenu: {
          mainMenu.Update(ref game, ref exit);
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
          if(mainMenu.SelectLevel != null)
            mainMenu.Reset();
          if(IsKeyPressed(KeyboardKey.Escape)) {
            state = ScreenState.MainMenu;
            game = new();
            TraceLog(TraceLogLevel.Info, "TESTE");
            break;
          }

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