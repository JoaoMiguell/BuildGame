using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
using BuildGame.Types.Enums;
using System.Numerics;
namespace BuildGame.Screens;

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
  Vector2 initialPlayerPosition;
  public bool isEmpty = false;

  public Game() {
    isEmpty = true;
  }

  public Game(string path) {
    using StreamReader sr = new(path);
    List<string[]> lines = sr.ReadToEnd().Split("\n").Select(l => l.Split(",")).ToList();
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
      for(int row = 0; row < lines[col].Length; row++) {
        if(lines[col][row] == "1") {
          Cel temp = new(segments[col].ElementAt(row), CelType.Floor);
          int aux = 1;
          for(int k = 1; lines[col][row + k] == "1"; k++) {
            temp.rect.Width += segments[col].ElementAt(row + k).Width;
            aux++;
          }
          if(aux == 1) {
            for(int k = 1; lines[col + k][row] == "1"; k++) {
              if(lines[col + k][row + 1] == "0" && lines[col + k][row - 1 <= 0 ? 1 : row - 1] == "0") {
                temp.rect.Height += segments[col + k].ElementAt(row).Height;
                lines[col + k][row] = "0";
              }
            }
          }
          cels.Add(temp);
          row += aux;
        }
        else if(lines[col][row] == "P") {
          player = new(new(segments[col].ElementAt(row).X, segments[col].ElementAt(row).Y));
          initialPlayerPosition = new(segments[col].ElementAt(row).X, segments[col].ElementAt(row).Y);
        }
      }
    }
  }

  public void Update(float deltaTime) {
    if(IsKeyPressed(KeyboardKey.KEY_R)) {
      player.Reset(initialPlayerPosition);
    }
    player.Update(ref cels, deltaTime);
  }

  public void Draw() {
    // DRAW
    foreach(Cel cel in cels) {
      if(cel.CelType == CelType.Floor) {
        DrawRectangle((int)cel.rect.X, (int)cel.rect.Y, (int)cel.rect.Width, (int)cel.rect.Height, Color.GRAY);
      }
      //DrawRectangleRec(new Rectangle(segment.X+10,segment.Y+10,50,50), Color.GREEN);
    }
    player.Draw();
  }

  private Color GenRandom() {
    int num = new Random().Next(0, 21);
    return colors[num];
  }
}