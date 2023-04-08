using UnityEditor;
using UnityEngine;
using System.IO;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class VTFConvert : AssetPostprocessor
{
    private string sourceSDKPath;

    // the folder from which textures will be auto exported. in lowercase
    private const string SPECIAL_FOLDER = "vtfexport";

    // Relative to the Assets directory. Starts with a slash.
    // Requires VTFCmd.exe, along with VTFLib.dll and DevIL.dll.
    // VTFCmd can be compiled from source, requires MFC libraries.
    private const string VTFCMD_PATH = "/Scripts/Editor/Plugins/VTFCmd.exe";


    void OnPostprocessTexture(Texture2D texture)
    {
        var dirName = new DirectoryInfo(Path.GetDirectoryName(assetPath)).Name;

        if (dirName.ToLower() != SPECIAL_FOLDER) return;

        if (!TryGetGame("Source SDK Base 2013 Singleplayer"))
        {
            Debug.LogError("Could not find Source SDK Base 2013.");
            return;
        }

        // looking for hl2/materials. here you can change it to episodic or ep2, or add an extra subfolder to materials.
        string matFolder = Path.Combine(sourceSDKPath, "hl2", "materials");

        // generate vtf next to the texture, because vtfcmd can't automatically output it into a specified folder
        RunVTF($"-file {GetTexturePath()}").WaitForExit();

        // get vtf file path
        var vtfPath = Path.Combine(Path.GetDirectoryName(GetTexturePath()), $"{texture.name}.vtf");

        // cut to destination
        File.Copy(vtfPath, Path.Combine(matFolder, $"{texture.name}.vtf"), true);
        File.Delete(vtfPath);

        // generate a simple vmt. here you should add a subfolder for the texture search before {texture.name}
        string vmt = $"\"LightmappedGeneric\"\n{{\n\t\"$basetexture\" \"{texture.name}\"\n}}";
        File.WriteAllText(Path.Combine(matFolder, $"{texture.name}.vmt"), vmt);
    }

    public Process RunVTF(string Command)
    {
        ProcessStartInfo ProcessInfo;

        ProcessInfo = new ProcessStartInfo(Application.dataPath + VTFCMD_PATH, Command);
        ProcessInfo.CreateNoWindow = true;
        ProcessInfo.UseShellExecute = false; // silently execute vtfcmd

        return Process.Start(ProcessInfo);
    }

    private string GetTexturePath() => Application.dataPath + assetPath.Remove(0, "Assets".Length);

    private bool TryGetGame(string gameName)
    {

        string steamPath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Valve\\Steam", "InstallPath", null);
        var libraryFolders = Path.Combine(steamPath, "steamapps", "libraryfolders.vdf");
        List<string> steamLibraries = new List<string>();
        foreach (var line in File.ReadAllLines(libraryFolders))
        {
            if (line.StartsWith("\t\t\"path\""))
            {
                steamLibraries.Add(line.Remove(0, "\t\t\"path\"\t\t\"".Length).TrimEnd('\"'));
            }
        }

        foreach (var lib in steamLibraries)
        {
            string l = lib;
            try
            {
                l += "\\steamapps\\common";
            }
            catch (ArgumentException e)
            {
                break;
            }
            l += $"\\{gameName}";
            if (Directory.Exists(l))
            {
                sourceSDKPath = l;
                break;
            }
        }

        return sourceSDKPath != null;
    }
}
