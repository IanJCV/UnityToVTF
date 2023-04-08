# UnityToVTF
Automatically converts texture files from Unity to Hammer VTF on import.

When a Texture2D file gets imported into a `vtfexport` folder it gets automatically sent to an `hl2/materials/` folder 
found in your install of Source SDK 2013 Singleplayer. You can also change it to search for Half-Life 2 or any other Source game, 
just change the game name at
```cs
        if (!TryGetGame("Source SDK Base 2013 Singleplayer")) // name has to match the steamapps/common folder name.
        {
            Debug.LogError("Could not find Source SDK Base 2013.");
            return;
        }
```
