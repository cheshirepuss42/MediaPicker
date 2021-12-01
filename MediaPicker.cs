using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

public class MediaCollection
{
    public List<string> Folders = new List<string>();
    public string Name;
}


public class MediaPicker
{
    public MediaCollection CurrentCollection;
    public MediaPickerSettings Settings;
    public List<string> Items, History;
    public List<string> VideoExtensions = new List<string> { "avi", "mpg", "mpeg", "mp4", "mkv" };
    public MediaPicker()
    {
        Settings = LoadSettings();
        Items = new List<string>();
        History = new List<string>();
        CurrentCollection = Settings.CurrentCollection();
        foreach (var folder in CurrentCollection.Folders)
        {
            Items.AddRange(GetFiles(folder));
        }
        

    }
    public void PlayFile(string path)
    {
        Process.Start(path);
        History.Add(path);
    }
    public MediaCollection CollectionByName(string name)
    {
        return Settings.StoredCollections.FirstOrDefault(x => x.Name == name);
    }
    private MediaPickerSettings LoadSettings()
    {
        var mps = new MediaPickerSettings();
        return mps;
    }
    public List<string> SearchResults(string term)
    {
        return Items.Where(x => x.Contains(term)).ToList();
    }
    public void AddToFoldersList(string folder)
    {
        CurrentCollection.Folders.Add(folder);
    }
    public void RemoveFromFoldersList(string folder)
    {
        CurrentCollection.Folders.Remove(folder);
    }

    private List<string> GetFiles(string folder, bool recurse = true)
    {
        List<string> list = new List<string>();
        if (recurse)
        {
            foreach (string d in Directory.GetDirectories(folder))
            {
                list.AddRange(GetFiles(d, recurse));
            }
        }
        foreach (var filepath in Directory.GetFiles(folder))
        {
            bool filtered = true;
            foreach (var suffix in VideoExtensions)
            {
                if (filepath.EndsWith(suffix))
                {
                    filtered = false;
                }
            }
            if (filtered)
            {
                list.Add(filepath);
            }
        }
        return list;
    }
}
