using BearsEngine.SystemTests.Source.BearSpinner;
using BearsEngine.SystemTests.Source.ConsoleDemo;
using BearsEngine.SystemTests.Source.Globals;
using BearsEngine.SystemTests.Source.InputDemo;
using BearsEngine.SystemTests.Source.IODemo;
using BearsEngine.SystemTests.Source.PathfindingDemo;
using BearsEngine.SystemTests.Source.ShaderDemo;
using BearsEngine.SystemTests.Source.SoundTest;
using BearsEngine.SystemTests.Source.TestSquare;
using BearsEngine.SystemTests.Source.TextDemo;
using BearsEngine.SystemTests.Source.UIDemo;

namespace BearsEngine.SystemTests.Source;

public class MenuScreen : Screen
{
    private static Rect GetImageRect(int row, int column) => new(10 + 170 * column, 10 + 70 * row, 60, 60);
    private static Rect GetButtonRect(int row, int column) => new(70 + 170 * column, 10 + 70 * row, 100, 60);

    public MenuScreen()
    {
        AddDemoWorldButton(0, 0, GA.GFX.Icon_TestSquare, "Test Square", new TestSquareScreen());

        AddDemoWorldButton(1, 0, GA.GFX.ICON_BEAR_SPINNER, "Bear Spinner", new BearSpinnerScreen());

        AddDemoWorldButton(2, 0, GA.GFX.ICON_UI_DEMO, "UI Demo", new UIDemoScreen());

        AddDemoWorldButton(3, 0, GA.GFX.ICON_TEXT_DEMO, "Text Demo", new TextDemoScreen());

        AddDemoWorldButton(4, 0, GA.GFX.Pathfinding.Icon_Pathfinding_Demo, "Pathfinding Demo", new PathfindingDemoScreen());

        AddDemoWorldButton(5, 0, GA.GFX.IO.Icon, "IO Demo", new IODemoScreen());

        AddDemoWorldButton(6, 0, GA.GFX.ShaderDemo.Icon_ShaderDemo, "Shader Demo", new ShaderDemoScreen());

        AddDemoWorldButton(7, 0, GA.GFX.SoundTest.Icon, "Sound Test", new SoundTestScreen());

        AddDemoWorldButton(0, 1, GA.GFX.InputDemo.Icon, "Input Demo", new InputDemoScreen());

        AddDemoWorldButton(1, 1, GA.GFX.ConsoleDemo.Icon, "Console Demo", new ConsoleDemoScreen());
    }

    private void AddDemoWorldButton(int row, int column, string iconPath, string description, IScene scene)
    {
        Add(new Image(iconPath, GetImageRect(row, column)));
        Add(new Button(1, GetButtonRect(row, column), Colour.White, GV.Theme, description, () => Engine.Scene = scene));
    }
}
