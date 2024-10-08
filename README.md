# hololens_anatomy

# Download project and Unity Editor
1. git clone this project 
2. Open project in Unity Hub
3. Install Unity Editor version __2021.3.18f1__ with __Universal Windows Platform Build Support__

# Switch Build Platform
> [Source](https://learn.microsoft.com/en-us/training/modules/learn-mrtk-tutorials/1-3-exercise-configure-unity-for-windows-mixed-reality)

1. Close __MRTK Project Configurator__ window

2. In the menu bar, select __File__ > __Build Settings....__

3. In the __Build Settings__ window, select __Universal Windows Platform__.

4. Make sure the following settings are active:

    __Architecture__: ARM 64-bit

    __Build Type__: D3D Project

    __Target SDK Version__: Latest Installed
    > Windows SDK should be installed

    __Minimum Platform Version__: 10.0.10240.0

    __Visual Studio Version__: Latest installed

    __Build and Run on__: Local Machine

    __Build configuration__: Release (there are known performance issues with Debug)

5. Click the __Switch Platform__ button. Unity displays a progress bar while it switches platforms.

6. After the switch platform process is finished, close the __Build Settings__ window.