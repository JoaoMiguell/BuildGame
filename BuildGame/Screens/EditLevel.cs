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
  Pencil pencil = new();
  bool isMenuPencil = false;
  bool hasPlayer = false;

  public EditLevel() {
    isEmpty = true;
  }

  public void Load(string path) {
    //using StreamReader sr = new($@"{Directory.GetCurrentDirectory()}\Levels\base.txt");
    using StreamReader sr = new(path);
    lines = sr.ReadToEnd().Split("\n").Select(l => l.TrimEnd('\r').Split(",")).Where(s => s[0] != "").ToList();
    sr.Close();
    this.path = path;

    GenerateCels();

    for(int row = 0; row < lines.Count; row++) {
      for(int col = 0; col < lines[row].Length; col++) {
        if(lines[row][col] == "1") {
          cels[row][col].CelType = CelType.Floor;
        }
        else if(lines[row][col] == "P") {
          cels[row][col].CelType = CelType.Player;
          hasPlayer = true;
        }
      }
    }

    isEmpty = false;
  }

  public void Create(string name) {
    path = $@"{Directory.GetCurrentDirectory()}\Levels\{name}.txt".ToLower();
    File.Copy($@"{Directory.GetCurrentDirectory()}\BaseLevel\base.txt", path);
    GenerateCels();
    using StreamReader sr = new (path);
    lines = sr.ReadToEnd().Split("\n").Select(l => l.TrimEnd('\r').Split(",")).Where(s => s[0] != "").ToList();
  }

  public void GenerateCels() {
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
  }

  public bool VerifyFileName(string name) {
    return File.Exists($@"{Directory.GetCurrentDirectory()}\Levels\{name}.txt");
  }

  public void Update(ref ScreenState state) {
    cels.ForEach(cel => {
      cel.ForEach(c => {
        if(IsMouseButtonPressed(MouseButton.Left) && CheckCollisionPointRec(GetMousePosition(), c.rect)) {
          if(pencil.type == CelType.Floor) {
            if(c.CelType == CelType.None) {
              c.CelType = CelType.Floor;
              if(lines[(int)c.pos.Y][(int)c.pos.X] == "P")
                hasPlayer = false;
              lines[(int)c.pos.Y][(int)c.pos.X] = "1";
            }
            else {
              c.CelType = CelType.None;
              if(lines[(int)c.pos.Y][(int)c.pos.X] == "P")
                hasPlayer = false;
              lines[(int)c.pos.Y][(int)c.pos.X] = "0";
            }
          }
          else if(pencil.type == CelType.Player && !hasPlayer) {
            c.CelType = CelType.Player;
            lines[(int)c.pos.Y][(int)c.pos.X] = "P";
            hasPlayer = true;
          }
        }
      });
    });

    if(isMenuPencil) {
      pencil.Update();
    }

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
    if(IsKeyPressed(KeyboardKey.Tab)) {
      isMenuPencil = !isMenuPencil;
    }
  }

  public void Draw() {
    if(isMenuPencil) {
      pencil.Draw();
    }
    else {
      cels.ForEach(cel => {
        cel.ForEach(c => {
          if(c.CelType == CelType.Floor) {
            DrawRectangle((int)c.rect.X, (int)c.rect.Y, (int)c.rect.Width, (int)c.rect.Height, Color.Gray);
          }
          else if(c.CelType == CelType.Player) {
            DrawRectangle((int)c.rect.X, (int)c.rect.Y, (int)c.rect.Width, (int)c.rect.Height, Color.Red);
          }
        });
        //DrawRectangleRec(new Rectangle(cel.rect.X,cel.rect.Y,60,60), Color.Green);
      });
    }
  }

  public void Reset() {
    //cels = [];
    //lines = [];
    //isEmpty = true;
  }
}
