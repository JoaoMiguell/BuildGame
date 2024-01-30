using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.Types;
using System.Numerics;
namespace BuildGame.Screens;

class EditableCel : Cel {
  public Vector2 pos;
  public EditableCel(Rectangle rect, CelType type, Vector2 position)
    : base(rect, type) {
    pos = position;
  }

}

internal class EditLevel {
  List<List<EditableCel>> cels = new();
  List<string[]> lines;

  public EditLevel(string name = "") {
    using StreamReader sr = new($@"{Directory.GetCurrentDirectory()}\Levels\base.txt");
    lines = sr.ReadToEnd().Split("\n").Select(l => l.TrimEnd('\r').Split(",")).ToList();
    sr.Close();

    int y = 0, x = 0;
    for(int i = 0; i < screenH; i += 60) {
      List<EditableCel> temp = new();
      for(int j = 0; j < screenW; j += 60) {
        temp.Add(new EditableCel(new(j, i, 60, 60), CelType.None, new(x,y)));
        x++;
      }
      cels.Add(temp);
      y++;
    }
  }

  public void Update() {
    cels.ForEach(cel => {
      cel.ForEach(c => {
        if(IsMouseButtonPressed(MouseButton.Left) && CheckCollisionPointRec(GetMousePosition(), c.rect)) {
          if(c.CelType == CelType.None) {
            c.CelType = CelType.Floor;
            lines[(int)c.pos.Y][(int)c.pos.Y] = "1";
          }
          else {
            c.CelType = CelType.None;
            lines[(int)c.pos.Y][(int)c.pos.Y] = "0";
          }
        }
      });
    });

    if(IsKeyPressed(KeyboardKey.LeftControl) && IsKeyPressed(KeyboardKey.S)) {
      //File.WriteAllLines()
    } 
  }

  public void Draw() {
    // DRAW
    cels.ForEach(cel => {
      cel.ForEach(c => {
        if(c.CelType == CelType.Floor) {
          DrawRectangle((int)c.rect.X, (int)c.rect.Y, (int)c.rect.Width, (int)c.rect.Height, Color.Gray);
        }
      });
      //DrawRectangleRec(new Rectangle(cel.rect.X,cel.rect.Y,60,60), Color.Green);
    });
  }
}
