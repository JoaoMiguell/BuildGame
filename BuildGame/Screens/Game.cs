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
      { 0, Color.RayWhite }, { 1, Color.LightGray }, { 2, Color.Gray },
      { 3, Color.DarkGray }, { 4, Color.Yellow }, { 5, Color.Gold },
      { 6, Color.Orange }, { 7, Color.Pink }, { 8, Color.Red },
      { 9, Color.Maroon }, { 10, Color.Green }, { 11, Color.Lime },
      { 12, Color.DarkGreen }, { 13, Color.SkyBlue }, { 14, Color.Blue },
      { 15, Color.DarkBlue }, { 16, Color.Purple }, { 17, Color.Violet },
      { 18, Color.DarkPurple }, { 19, Color.Beige }, { 20, Color.Brown }
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
    List<string[]> lines = sr.ReadToEnd().Split("\n").Select(l => l.TrimEnd('\r').Split(",")).ToList();
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
          for(int k = 1; row + k < lines[col].Length; k++) {
            if(lines[col][row + k] == "1") {
              temp.rect.Width += segments[col].ElementAt(row + k).Width;
              aux++;
            }
            else
              break;
          }
          if(aux == 1) {
            for(int k = 1; lines[col + k][row] == "1"; k++) {
              string right = row + 1 <= lines[col + k].Length ? "0" : lines[col + k][row+1];
              string left = row - 1 < 0 ? "0" : lines[col + k][row - 1];
              if(right == "0" && left == "0") {
                temp.rect.Height += segments[col + k].ElementAt(row).Height;
                lines[col + k][row] = "0";
              }
              else
                break;
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
    if(IsKeyPressed(KeyboardKey.R)) {
      player.Reset(initialPlayerPosition);
    }
    player.Update(ref cels, deltaTime);
  }

  public void Draw() {
    // DRAW
    foreach(Cel cel in cels) {
      if(cel.CelType == CelType.Floor) {
        DrawRectangle((int)cel.rect.X, (int)cel.rect.Y, (int)cel.rect.Width, (int)cel.rect.Height, Color.Gray);
      }
      //DrawRectangleRec(new Rectangle(cel.rect.X,cel.rect.Y,60,60), Color.Green);
    }
    player.Draw();
  }

  private Color GenRandom() {
    int num = new Random().Next(0, 21);
    return colors[num];
  }
}