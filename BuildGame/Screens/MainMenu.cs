using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types;
using BuildGame.Types.Enums;
using System.Numerics;
using BuildGame.UI;
namespace BuildGame.Screens;

internal class MainMenu {
  StartButton StartButton;
  SelectLevel SelectLevel = new();
  MainMenuState State = MainMenuState.Main;

  public MainMenu() {
    StartButton = new StartButton(new(screenW / 2 - 95, screenH / 2 - 25, 190, 50),
                                      Color.BEIGE, "Start", Color.WHITE, 30);
  }

  public void Update() {

    switch(State) {
      case MainMenuState.Main: {
        StartButton.Update(ref State);

      }
      break;
      case MainMenuState.Start:
      break;
    }
  }

  public void Draw() {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Draw();
      }
      break;
      case MainMenuState.Start: {
        Console.WriteLine("DEU");
      }
      break;
    }
  }
}

internal class SelectLevel {
  List<string> levels = [];

  public SelectLevel() {
    levels = Directory.GetFiles($@"{Directory.GetCurrentDirectory()}\Levels").ToList();

  }
}