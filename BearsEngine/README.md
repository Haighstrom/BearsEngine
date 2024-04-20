# BearsEngine
(.NET 6.0) A C# library for doing low level windows stuff

<details><summary>WINDOW</summary>

#### Create and loop a window using the IWindow interface:
```
WindowSettings WindowSettings = new()
{
    Width = 800,
    Height = 600,
    Centre = true,
    Title = "BearsEngine Tester",
};

using IWindow window = new HaighWindow(windowSettings);

while (window.IsOpen)
{
    window.ProcessEvents();
    
    //Game Loop Here
}
```

</details>
