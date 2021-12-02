using System;
using System.Collections.Generic;

using System.Linq;

[Serializable]
public class MediaCollection
{
    public List<string> Folders = new List<string>();
    public string Name
    {
        get
        {
            List<string> result = new List<string>();
            foreach (var f in Folders)
            {
                var lastpart = f.Split(new[] { "\\" }, StringSplitOptions.None).LastOrDefault();
                result.Add(lastpart);
            }
            return string.Join(", ", result);
        }
    }

}


[Serializable]
public class MediaPickerSettings
{
    public List<MediaCollection> StoredCollections;
    public MediaCollection CurrentCollection;
    public string CurrentPath;
    public string CurrentFileName;
    public string CurrentFolderPath;
    public string CurrentSearchTerm;
    public bool AutoPlay;
    public MediaPickerSettings()
    {
        StoredCollections = new List<MediaCollection>();
        StoredCollections.Add(new MediaCollection() { Folders = new List<string>() { "Z:\\Video\\Movies" } });
        CurrentCollection = StoredCollections.First();
    }


    public MediaCollection CollectionByName(string name)
    {
        return StoredCollections.FirstOrDefault(x => x.Name == name);
    }
    public void SetCurrentFile(string path)
    {

        CurrentPath = path;
        var parts = path.Split(new[] { "\\" }, StringSplitOptions.None).ToList();
        CurrentFileName = parts.Last();
        parts.RemoveAt(parts.Count - 1);
        CurrentFolderPath = string.Join("\\", parts);
    }
}
