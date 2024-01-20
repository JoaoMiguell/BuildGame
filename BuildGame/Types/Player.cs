using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.Types;

internal class Player {
  Rectangle rectangle = new();

  public Player() { }
  public Player(Rectangle pos) {
    rectangle.X = (int)pos.X;
    rectangle.Y = (int)pos.Y;
    rectangle.Width = 32;
    rectangle.Height = 32;
  }

  public void Draw() {
    DrawRectangleRec(rectangle, Color.MAGENTA);
  }

  public void Update(ref List<Cel> cels) {
    // TODO: Collision detection
    // TODO: Gravity

    if(IsKeyDown(KeyboardKey.KEY_A))
      rectangle.X -= 100 * GetFrameTime();
    if(IsKeyDown(KeyboardKey.KEY_D))
      rectangle.X += 100 * GetFrameTime();
  }
}

