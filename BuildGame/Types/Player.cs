using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace BuildGame.Types;

internal class Player {
  Vector2 position = new();
  float speed = 0;
  int horSpeed = 200;
  int jumpSpeed = 200;
  bool canJump = false;

  public Player() { }
  public Player(Vector2 pos) {
    position.X = (int)pos.X;
    position.Y = (int)pos.Y;
    //rect.Width = 32;
    //rect.Height = 32;
  }

  public void Draw() {
    DrawRectangleV(position, new(32, 32), Color.Magenta);
  }

  public void Update(ref List<Cel> cels, float deltaTime) {
    // TODO: Collision detection all sides

    bool onFloor = false;
    for(int i = 0; i < cels.Count; i++) {
      Cel cel = cels[i];
      if(CheckCollisionRecs(new(position.X, position.Y, 32, 32), cel.rect)) {
        float overlapX = Math.Min(position.X + 32, cel.rect.X + cel.rect.Width) - Math.Max(position.X, cel.rect.X);
        float overlapY = Math.Min(position.Y + 32, cel.rect.Y + cel.rect.Height) - Math.Max(position.Y, cel.rect.Y);

        if(overlapX > overlapY) {
          // top
          if(position.Y < cel.rect.Y) {
            onFloor = true;
            speed = 0.0f;
            position.Y = cel.rect.Y - 32;
          }
          // bottom
          else position.Y = cel.rect.Y + cel.rect.Height;
        }
        else {
          // left
          if(position.X < cel.rect.X) position.X = cel.rect.X - 32;
          // right
          else position.X = cel.rect.X + cel.rect.Width;
        }
      }
    }

    if(!onFloor) {
      position.Y += speed * deltaTime;
      speed += 300 * deltaTime;
      canJump = false;
    }
    else
      canJump = true;

    if(IsKeyDown(KeyboardKey.A) || IsKeyDown(KeyboardKey.Left))
      position.X -= horSpeed * deltaTime;
    if(IsKeyDown(KeyboardKey.D) || IsKeyDown(KeyboardKey.Right))
      position.X += horSpeed * deltaTime;
    if(IsKeyDown(KeyboardKey.S) || IsKeyDown(KeyboardKey.Down))
      position.Y += horSpeed * deltaTime;
    if((IsKeyDown(KeyboardKey.Space) || IsKeyDown(KeyboardKey.W) || IsKeyDown(KeyboardKey.Up))
      && canJump) {
      speed = -jumpSpeed;
      canJump = false;
    }
  }

  public void Reset(Vector2 initPos) {
    position.X = initPos.X;
    position.Y = initPos.Y;
    speed = 0;
    canJump = false;
  }
}