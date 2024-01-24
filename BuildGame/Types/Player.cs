using Raylib_cs;
using System.Numerics;
using static Raylib_cs.Raylib;
namespace BuildGame.Types;

internal class Player {
  Vector2 position = new();
  float speed = 0;
  int horSpeed = 200;
  int jumpSpeed = 100;
  bool canJump = false;
  private bool onFloor = false;

  public Player() { }
  public Player(Vector2 pos) {
    position.X = (int)pos.X;
    position.Y = (int)pos.Y;
    //rect.Width = 32;
    //rect.Height = 32;
  }

  public void Draw() {
    DrawRectangleV(position, new(32, 32), Color.MAGENTA);
  }

  public void Update(ref List<Cel> cels, float deltaTime) {
    // TODO: Collision detection all sides

    bool hitObstacle = false;
    for(int i = 0; i < cels.Count; i++) {
      Cel ei = cels[i];
      if(ei.rect.X <= position.X &&
          ei.rect.X + ei.rect.Width >= position.X &&
          ei.rect.Y + 32 >= position.Y &&
          ei.rect.Y - 32 <= position.Y + speed * deltaTime) {
        hitObstacle = true;
        speed = 0.0f;
        position.Y = ei.rect.Y - 32;
      }
    }

    if(!hitObstacle) {
      position.Y += speed * deltaTime;
      speed += 400 * deltaTime;
      canJump = false;
    }
    else canJump = true;

    if(IsKeyDown(KeyboardKey.KEY_A))
      position.X -= horSpeed * deltaTime;
    if(IsKeyDown(KeyboardKey.KEY_D))
      position.X += horSpeed * deltaTime;
    if(IsKeyDown(KeyboardKey.KEY_SPACE) && canJump) {
      speed = -jumpSpeed;
      canJump = false;
    }
  }

  public void ResetLoop() {
    onFloor = false;
  }
}

