using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
namespace BuildGame;

internal class Program {
  static void Main(string[] args) {
    SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
    InitWindow(screenW, screenH, "Build Game");

    Game game = new Game($@"{Directory.GetCurrentDirectory()}\TestScenarios\base.txt");
    bool pause = false;
    while(!WindowShouldClose()) {
      float deltaTime = GetFrameTime();

      if(IsKeyPressed(KeyboardKey.KEY_F1))
        pause = !pause;

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
    CloseWindow();
    return;
  }
}

