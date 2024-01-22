using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
using System.Numerics;
using BuildGame.Types.Enums;
namespace BuildGame;

internal class Game {
  // ------- TEMP -------  
  Dictionary<int, Color> colors = new Dictionary<int, Color> {
      { 0, Color.RAYWHITE }, { 1, Color.LIGHTGRAY }, { 2, Color.GRAY },
      { 3, Color.DARKGRAY }, { 4, Color.YELLOW }, { 5, Color.GOLD },
      { 6, Color.ORANGE }, { 7, Color.PINK }, { 8, Color.RED },
      { 9, Color.MAROON }, { 10, Color.GREEN }, { 11, Color.LIME },
      { 12, Color.DARKGREEN }, { 13, Color.SKYBLUE }, { 14, Color.BLUE },
      { 15, Color.DARKBLUE }, { 16, Color.PURPLE }, { 17, Color.VIOLET },
      { 18, Color.DARKPURPLE }, { 19, Color.BEIGE }, { 20, Color.BROWN }
  };
  // --------------------
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
        temp.Add(new Rectangle(j, i, 60, 60));
      }
      segments.Add(temp);
    }

    for(int col = 0; col < lines.Count; col++) {
      string[] tLine = lines[col].Split(',');
      for(int row = 0; row < tLine.Length; row++) {
        if(tLine[row] == "1") {
          Cel temp = new(segments[col].ElementAt(row), CelType.Floor);
          int aux = 1;
          for(int k = 1; tLine[row+k] == "1"; k++) {
            temp.cords.Width += segments[col].ElementAt(row+k).Width;
            aux++;
          }
          cels.Add(temp);
          row += aux;
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
        DrawRectangle((int)cel.cords.X, (int)cel.cords.Y, (int)cel.cords.Width, 60, GenRandom());
      }
      //DrawRectangleRec(new Rectangle(segment.X+10,segment.Y+10,50,50), Color.GREEN);
    }
    player.Draw();
  }

  private Color GenRandom() {
    int num = new Random().Next(0,21);
    return colors[num];
  }
}