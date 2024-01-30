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
  public EditLevel? Edit;
  MainMenuState State = MainMenuState.Main;
  Input InputEdit;

  public MainMenu() {
    StartButton = new StartButton(new(screenW / 2 - 95, screenH / 2 - 25, 190, 50),
                                      Color.Beige, "Start", Color.White, 30);

    CloseButton = new CloseButton(new(screenW / 2 - 95, screenH / 2 + 25, 190, 50),
                                      Color.Red, "Close", Color.White, 30);

    EditButton = new EditButton(new(screenW / 2 - 95, screenH / 2 + 75, 190, 50),
                                      Color.Green, "Edit", Color.White, 30);

    InputEdit = new Input(new(screenW / 2 - 95, screenH / 2 - 25, 300, 200));
  }

  public void Update(ref Game game, ref bool exit) {
    switch(State) {
      case MainMenuState.Main: {
        StartButton.Update(ref State);
        CloseButton.Update(ref exit);
        EditButton.Update(ref State);
      }
      break;
      case MainMenuState.Start: {
        if(SelectLevel == null)
          SelectLevel = new();
        SelectLevel.Update(ref game);
      }
      break;
      case MainMenuState.EditSelect: {
        InputEdit.Update(ref Edit);
      }
      break;
      case MainMenuState.Edit: {
        if(Edit == null)
          Edit = new EditLevel();
        Edit.Update();
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
      case MainMenuState.Edit: {
        Edit?.Draw();
      }
      break;
    }
  }

  public void Reset() {
    SelectLevel = null;
    State = MainMenuState.Main;
  }
}