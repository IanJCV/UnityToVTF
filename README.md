# UnityToVTF
Automatically converts texture files from Unity to Hammer VTF on import.

When a Texture2D file gets imported into a `vtfexport` folder it gets automatically sent to an `hl2/materials/` folder 
found in your install of Source SDK 2013 Singleplayer. You can also change it to search for Half-Life 2 or any other Source game, 
just change the game name at
```cs
        // name has to match the steamapps/common folder name.
        if (!TryGetGame("Source SDK Base 2013 Singleplayer"))
        {
            Debug.LogError("Could not find Source SDK Base 2013.");
            return;
        }
```


MIT License

Copyright (c) 2023 Ian

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
