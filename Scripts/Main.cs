using Godot;
using System.Collections.Generic;
using System.Linq;

public class Main : Control
{
    public ItemList FoldersList, CollectionsList, SearchResults, HistoryList, RandomHistoryList;
    public LineEdit SearchTerm, CurrentPath, CollectionName;
    public Button PlayButton;
    public CheckBox AutoPlayCheck;
    public Label CurrentCollectionLabel;
    private MediaPicker Picker;

    public override void _Ready()
    {
        string picker = "Tabs/Picker/Sections/";
        CurrentCollectionLabel = GetNode<Label>(picker + "Current/CurrentCollection");
        SearchResults = GetNode<ItemList>(picker + "Find/Search/SearchResult");
        RandomHistoryList = GetNode<ItemList>(picker + "Find/Random/RandomHistory");
        SearchTerm = GetNode<LineEdit>(picker + "Find/Search/SearchBar");
        CurrentPath = GetNode<LineEdit>(picker + "Current/Path/CurrentPath");
        PlayButton = GetNode<Button>(picker + "Current/PlayControls/PlayButton");
        AutoPlayCheck = GetNode<CheckBox>(picker + "Current/PlayControls/AutoPlayCheckBox");

        string collections = "Tabs/Collections/Sections/";
        FoldersList = GetNode<ItemList>(collections + "CurrentCollection/FoldersList");
        CollectionsList = GetNode<ItemList>(collections + "Collections/CollectionsList");
        CollectionName = GetNode<LineEdit>(collections + "CollectionName");

        HistoryList = GetNode<ItemList>("Tabs/History/HistoryItems");
        Picker = new MediaPicker();
        Picker.UpdateFiles();
        RefreshUI();
    }
    public void RefreshUI()
    {
        FoldersList.Clear();
        foreach (var folder in Picker.Settings.CurrentCollection.Folders)
        {
            FoldersList.AddItem(folder);
        }
        CollectionsList.Clear();
        foreach (var collection in Picker.Settings.StoredCollections)
        {
            CollectionsList.AddItem(collection.Name);
        }        
        CurrentCollectionLabel.Text = Picker.Settings.CurrentCollection.Name;
        CollectionName.Text = Picker.Settings.CurrentCollection.Name;
        CurrentPath.Text = Picker.Settings.CurrentPath;
        PlayButton.Text = Picker.Settings.CurrentFileName;
        HistoryList.Clear();
        foreach (var file in Picker.History)
        {
            HistoryList.AddItem(file);
        }
        RandomHistoryList.Clear();
        foreach (var file in Picker.History)
        {
            RandomHistoryList.AddItem(file);
        }
    }

    // Picker
    public void _on_RandomFromFolder_pressed()
    {
        Picker.PlayRandomFromCurrentFolder();
        RefreshUI();
    }
    public void _on_RandomFromCollectionButton_pressed()
    {
        Picker.PlayRandomFromCollection();
        RefreshUI();
    }
    public void _on_NextButton_pressed()
    {
        Picker.SetNextFile();
        RefreshUI();
    }
    public void _on_PrevButton_pressed()
    {
        Picker.SetPrevFile();
        RefreshUI();
    }
    public void _on_AutoPlayCheckBox_pressed()
    {
        Picker.Settings.AutoPlay = AutoPlayCheck.Pressed;
    }
    public void _on_PlayButton_pressed()
    {
        Picker.PlayCurrentFile();
        RefreshUI();
    }
    public void _on_SearchResult_item_activated(int index)
    {
        string item = SearchResults.GetItemText(index);
        Picker.PlayFile(item);
        RefreshUI();

    }
    public void _on_SearchBar_text_changed(string newText)
    {
        SearchResults.Clear();
        foreach (var result in Picker.SearchResults(SearchTerm.Text))
        {
            SearchResults.AddItem(result);
        }
    }
    public void _on_OpenFolderButton_pressed()
    {
        Picker.OpenCurrentFolder();
    }

    // Collections

    public void _on_AddFolderButton_pressed()
    {
        GetNode<FileDialog>("FolderFindDialog").Popup_();
    }
    public void _on_FolderFindDialog_dir_selected(string dir)
    {
        Picker.AddToFoldersList(dir);
        FoldersList.AddItem(dir);
        Picker.UpdateFiles();
        RefreshUI();        
    }
    public void _on_RemoveFolderButton_pressed()
    {
        foreach (var selected in FoldersList.GetSelectedItems())
        {
            Picker.RemoveFromFoldersList(FoldersList.GetItemText(selected));
            FoldersList.RemoveItem(selected);
        }
        Picker.UpdateFiles();
        RefreshUI();
    }
    public List<string> ItemsFromList(ItemList list)
    {
        var nl = new List<string>();
        for (int i = 0; i < list.Items.Count; i++)
        {
            nl.Add(list.GetItemText(i));
        }
        return nl;
    }
    public void _on_SaveCurrentCollectionButton_pressed()
    {
        Picker.SaveCollection(CollectionName.Text, ItemsFromList(FoldersList));
        RefreshUI();
    }
    public void _on_LoadCollectionButton_pressed()
    {
        var name = CollectionsList.GetItemText(CollectionsList.GetSelectedItems()[0]);
        var cl = Picker.Settings.StoredCollections.Find(x => x.Name == name);
        if (cl != null)
        {
            Picker.Settings.CurrentCollection = cl;
        }
        Picker.UpdateFiles();
        RefreshUI();

    }
    public void _on_RemoveCollectionButton_pressed()
    {
        var name = CollectionsList.GetItemText(CollectionsList.GetSelectedItems()[0]);
        var cl = Picker.Settings.StoredCollections.Find(x => x.Name == name);
        if (cl != null)
        {
            Picker.Settings.StoredCollections.Remove(cl);
        }
        RefreshUI();        
    }
    public void _on_RandomHistory_item_activated(int index)
    {
        Picker.PlayFile(RandomHistoryList.GetItemText(index), true);
    }
    // History
    public void _on_HistoryItems_item_activated(int index)
    {
        Picker.PlayFile(HistoryList.GetItemText(index), true);
    }
}


