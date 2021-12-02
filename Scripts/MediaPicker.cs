using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;


public static class ListExtensions
{
    public static T RandomItem<T>(this IList<T> list)
    {
        int n = list.Count;
        int k = ((int)GD.RandRange(0, list.Count));
        return list[k];
    } 
}

public class MediaPicker
{
    public FileHandler Files;
    public MediaPickerSettings Settings;
    public List<string> Items, History;

    public MediaPicker()
    {
        Files = new FileHandler();
        Settings = Files.LoadSettings();
        Items = new List<string>();
        History = new List<string>();
    }

    public void UpdateFiles()
    {
        Items.Clear();
        foreach (var folder in Settings.CurrentCollection.Folders)
        {
            Items.AddRange(Files.GetFiles(folder));
        }
    }

    public void PlayFile(string path)
    {
        Settings.SetCurrentFile(path);
        Process.Start(path);
        History.Add(path);
    }

    public MediaCollection CollectionByName(string name)
    {
        return Settings.StoredCollections.FirstOrDefault(x => x.Name == name);
    }

    public List<string> SearchResults(string term)
    {
        return Items.Where(x => x.Contains(term)).ToList();
    }

    public void AddToFoldersList(string folder)
    {
        Settings.CurrentCollection.Folders.Add(folder);
    }

    public void RemoveFromFoldersList(string folder)
    {
        Settings.CurrentCollection.Folders.Remove(folder);
    }
    public void PlayRandomFromCollection()
    {
        PlayFile(Items.RandomItem());
    }
    public void PlayRandomFromCurrentFolder()
    {
        var files = Items.Where(x=>x.StartsWith(Settings.CurrentFolderPath)).ToList();
        PlayFile(files.RandomItem());
    }
   
}
