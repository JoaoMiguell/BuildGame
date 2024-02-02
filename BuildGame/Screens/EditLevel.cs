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
  public bool isEmpty = false;
  string path;

  public EditLevel() {
    isEmpty = true;
  }

  public void Load(string path) {
    //using StreamReader sr = new($@"{Directory.GetCurrentDirectory()}\Levels\base.txt");
    using StreamReader sr = new(path);
    lines = sr.ReadToEnd().Split("\n").Select(l => l.TrimEnd('\r').Split(",")).Where(s => s[0] != "").ToList();
    sr.Close();
    this.path = path;

    int y = 0, x = 0;
    for(int i = 0; i < screenH; i += 60) {
      List<EditableCel> temp = new();
      for(int j = 0; j < screenW; j += 60) {
        temp.Add(new EditableCel(new(j, i, 60, 60), CelType.None, new(x, y)));
        x++;
      }
      cels.Add(temp);
      y++;
      x = 0;
    }

    for(int row = 0; row < lines.Count; row++) {
      for(int col = 0; col < lines[row].Length; col++) {
        if(lines[row][col] == "1") {
          cels[row][col].CelType = CelType.Floor;
        }
      }
    }

    isEmpty = false;
  }

  public void Create(string name) {

  }

  public void Update(ref ScreenState state) {
    cels.ForEach(cel => {
      cel.ForEach(c => {
        if(IsMouseButtonPressed(MouseButton.Left) && CheckCollisionPointRec(GetMousePosition(), c.rect)) {
          if(c.CelType == CelType.None) {
            c.CelType = CelType.Floor;
            lines[(int)c.pos.Y][(int)c.pos.X] = "1";
          }
          else {
            c.CelType = CelType.None;
            lines[(int)c.pos.Y][(int)c.pos.X] = "0";
          }
        }
      });
    });

    if(IsKeyDown(KeyboardKey.LeftControl) && IsKeyPressed(KeyboardKey.S)) {
      List<string> tempList = lines.Select(l => string.Join(",", l)).ToList();
      string temp = "";
      tempList.ForEach(l => {
        temp += new string(l[..39] + "\n");
      });
      using StreamWriter sw = new(path, false);
      sw.Write(temp);
      sw.Flush();
      sw.Close();
    }

    if(IsKeyPressed(KeyboardKey.Escape)) {
      state = ScreenState.MainMenu;
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

  public void Reset() {
    //cels = [];
    //lines = [];
    //isEmpty = true;
  }
}
