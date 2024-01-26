using Raylib_cs;
using static Raylib_cs.Raylib;
namespace BuildGame.UI;

internal class Button {
  protected Rectangle rect;
  protected Color rectColor;
  protected string text;
  protected int textSize;
  protected Color textColor;

  public Button(Rectangle rect, Color rectColor, string text, Color textColor, int textSize) {
    this.rect = rect;
    this.rectColor = rectColor;
    this.text = text;
    this.textColor = textColor;
    this.textSize = textSize;
  }

  public void Draw() {
    DrawRectangleRec(rect, rectColor);
    DrawText(text, (int)rect.X + 10 + (MeasureText(text, textSize) / 2),
            (int)rect.Y + textSize / 3, textSize, textColor);
  }
}