using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
using System.Numerics;
using BuildGame.Types.Enums;
namespace BuildGame;

internal class Game {
  List<Cel> cels = new();
  Player player = new();

  public Game(string path) {
    using StreamReader sr = new(path);
    List<string> lines = sr.ReadToEnd().Split("\n").ToList();
    sr.Close();

    List<List<Rectangle>> segments = new();
    for(int i = 0; i < screenH; i += 60) {
      List<Rectangle> temp = new();
      for(int j = 0; j < screenW; j += 60) {
        temp.Add(new Rectangle(j, i, 60,60));
      }
      segments.Add(temp);
    }

    for(int col = 0; col < lines.Count; col++) {
      string[] tLine = lines[col].Split(',');
      for(int row = 0; row < tLine.Length; row++) {
        if(tLine[row] == "1") {
          cels.Add(new Cel(segments[col].ElementAt(row), CelType.Floor));
        } else if(tLine[row] == "P") {
          player = new(segments[col].ElementAt(row));
        }
      }
    }
  }

  public void Draw() {
    // UPDATE
    player.Update(ref cels);

    // DRAW
    foreach(Cel cel in cels) {
      if(cel.CelType == CelType.Floor) {
        DrawRectangle((int)cel.cords.X, (int)cel.cords.Y, 60, 60, Color.GRAY);
      }
      //DrawRectangleRec(new Rectangle(segment.X+10,segment.Y+10,50,50), Color.GREEN);
    }
    player.Draw();
  }
}