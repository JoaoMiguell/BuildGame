using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
namespace BuildGame;

internal class Program {
  static void Main(string[] args) {
    SetConfigFlags(ConfigFlags.FLAG_VSYNC_HINT);
    InitWindow(screenW, screenH, "Build Game");

    Game game = new Game($@"{Directory.GetCurrentDirectory()}\TestScenarios\base.txt");

    while(!WindowShouldClose()) {
      BeginDrawing();
      ClearBackground(Color.BLACK);
      game.Draw();
      DrawFPS(10, 10);
      EndDrawing();
    }
    CloseWindow();
    return;
  }
}

