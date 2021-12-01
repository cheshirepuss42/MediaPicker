using System.Collections.Generic;
using System.Linq;

public class MediaPickerSettings
{
    public List<MediaCollection> StoredCollections; 
    public string CurrentCollectionName;   
    public string CurrentFileName;
    public string CurrentFolderPath;
    public string CurrentSearchTerm;
    public bool AutoPlay;
    public MediaPickerSettings()
    {
        StoredCollections=new List<MediaCollection>();
    }
    public MediaCollection CurrentCollection()
    {
        var cbn = CollectionByName(CurrentCollectionName);
        return cbn ?? new MediaCollection();
    }
    public MediaCollection CollectionByName(string name)
    {
        return StoredCollections.FirstOrDefault(x => x.Name == name);
    }    
}
