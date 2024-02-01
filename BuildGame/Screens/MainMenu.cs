using Raylib_cs;
using static Raylib_cs.Raylib;
using static BuildGame.Globals;
using BuildGame.Types.Enums;
using BuildGame.UI;
namespace BuildGame.Screens;

internal class MainMenu {
  StartButton StartButton;
  CloseButton CloseButton;
  EditButton EditButton;
  public SelectLevel? SelectLevel;
  SelectEditLevel? SelectEditLevel;
  MainMenuState State = MainMenuState.Main;
  public bool isClean = false;

  public MainMenu() {
    StartButton = new StartButton(new(screenW / 2 - 95, screenH / 2 - 25, 190, 50),
                                      Color.Beige, "Start", Color.White, 30);

    CloseButton = new CloseButton(new(screenW / 2 - 95, screenH / 2 + 25, 190, 50),
                                      Color.Red, "Close", Color.White, 30);

    EditButton = new EditButton(new(screenW / 2 - 95, screenH / 2 + 75, 190, 50),
                                      Color.Green, "Edit", Color.White, 30);

    //InputEdit = new Input(new(screenW / 2 - 95, screenH / 2 - 25, 300, 200));
  }

  public void Update(ref Game game, ref bool exit, ref EditLevel editLevel, ref ScreenState screenState) {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Update(ref State);
        CloseButton.Update(ref exit);
        EditButton.Update(ref State);
        if(SelectLevel != null)
          SelectLevel = null;
        if(SelectEditLevel != null)
          SelectEditLevel = null;
      }
      break;
      case MainMenuState.Start: {
        if(SelectLevel == null)
          SelectLevel = new();
        SelectLevel.Update(ref game, ref State);
      }
      break;
      case MainMenuState.EditSelect: {
        if(SelectEditLevel == null) SelectEditLevel = new();
        SelectEditLevel.Update(ref State, ref editLevel, ref screenState);
      }
      break;
    }
  }

  public void Draw() {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Draw();
        CloseButton.Draw();
        EditButton.Draw();
      }
      break;
      case MainMenuState.Start: {
        SelectLevel?.Draw();
      }
      break;
      case MainMenuState.EditSelect: {
        SelectEditLevel?.Draw();
      }
      break;
    }
  }

  public void Reset() {
    SelectLevel = null;
    SelectEditLevel = null;
    State = MainMenuState.Main;
    isClean = true;
  }
}