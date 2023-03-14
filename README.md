# Rive Noesis Unity FPS Demo

Unity project using [NoesisGUI](https://www.noesisengine.com/) and [Rive](https://rive.app/) with the [Unity FPS](https://www.noesisengine.com/forums/viewtopic.php?t=2772) game template.

The [original](https://learn.unity.com/project/fps-template) Unity package template comes from Unity Learn.

## Getting Started

Clone the above project onto your local machine and open the project through the Unity Hub.

You may need to configure a [Noesis Trial Key](https://www.noesisengine.com/trial/) in the Noesis Settings in Unity to play the game demo. See [here](https://www.noesisengine.com/docs/Gui.Core.Licensing.html#unity) for instructions on how to set the key in the Unity Noesis settings.

### Seting the Scene

You'll have to set the Main Scene from the FPS game template yourself. Follow the below steps to load it in:

1. In the Project tab in Unity, open the `Assets/FPS/Scenes` folder
2. Locate the `MainScene` Unity Scene Asset
3. Drag that onto the Hierarchy
4. Remove the default scene that displayed when you first opened the project

### Playing the Game

In the main stage area, switch to the `Game` tab, and set the Display from _Free Aspect_ to _Full HD (1920 x 1080)_. Click the Play head button above to play the game.

### Editing the Noesis UI (XAML)

Note: Microsoft Blend is supported only on Windows

Most of the HUD camera UI comes from a XAML file parsed and rendered with NoesisGUI. To inspect this file:

1. Open the `Assets` dropdown in the top toolbar and select `Open Blend Project`
2. Once Microsoft Blend opens, in the Solution Explorer, toggle the "Show All Files" icon button to show all files in the project
3. In the `Assets/NoesisGUI` folder, locate the `RiveNoesisExampleMainView.xaml` file
4. Right-click and select _Include in Project_
5. Open the file to load it in the main stage and see the UI editor/XAML code

### Editing the View Model

In the Unity editor, in the Project tab, locate the folder `Assets/NoesisGUI`. This folder includes the assets for our Noesis integration, which include the Rive files used in the UI, as well as the ViewModels to control the Noesis View.

Open `MainHudViewModel.cs` to inspect the logic that drives the Rive state machine input values for the Rive inputs defined in the XAML file from above.

## Rives Included

The following bits on the Player -> Main Camera are replaced with Rive UI:
- Health bar
- Ammo bar
- (New) Camera frame border
- Objective for enemies left

**Note**: As of Noesis 3.2, you may only load the default artboard/state machine in a Rive file. To load multiple Rive state machines in your scene, ensure you have broken them out into individual Rive files.