using Godot;

public class Main : Control
{
    public ItemList FoldersList, CollectionsList, SearchResults, HistoryList;
    public LineEdit SearchTerm, CurrentPath;
    public Button PlayButton;
    public CheckBox AutoPlayCheck;
    public Label CurrentCollectionLabel;
    private MediaPicker Picker;

    public override void _Ready()
    {
        CurrentCollectionLabel = GetNode<Label>("Tabs/Picker/Sections/Current/CurrentCollection");
        SearchResults = GetNode<ItemList>("Tabs/Picker/Sections/Search/SearchResult");
        SearchTerm = GetNode<LineEdit>("Tabs/Picker/Sections/Search/SearchBar");
        CurrentPath = GetNode<LineEdit>("Tabs/Picker/Sections/Current/Path/CurrentPath");
        PlayButton = GetNode<Button>("Tabs/Picker/Sections/Current/PlayControls/PlayButton");
        AutoPlayCheck = GetNode<CheckBox>("Tabs/Picker/Sections/Current/PlayControls/AutoPlayCheckBox");

        FoldersList = GetNode<ItemList>("Tabs/Collections/Sections/CurrentCollection/FoldersList");
        CollectionsList = GetNode<ItemList>("Tabs/Collections/Sections/Collections/CollectionsList");  
              
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
        CurrentPath.Text = Picker.Settings.CurrentPath;
        PlayButton.Text = Picker.Settings.CurrentFileName;
        HistoryList.Clear();
        foreach (var file in Picker.History)
        {
            HistoryList.AddItem(file);
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
    }
    public void _on_PrevButton_pressed()
    {
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
    }
    public void _on_RemoveFolderButton_pressed()
    {
        foreach (var selected in FoldersList.GetSelectedItems())
        {
            Picker.RemoveFromFoldersList(FoldersList.GetItemText(selected));
            FoldersList.RemoveItem(selected);
        }
    }
    public void _on_SaveCurrentCollectionButton_pressed()
    {
    }
    public void _on_LoadCollectionButton_pressed()
    {
    }
    public void _on_RemoveCollectionButton_pressed()
    {
    }

    // History
    public void _on_HistoryItems_item_activated(int index)
    {
        Picker.PlayFile(HistoryList.GetItemText(index), true);
    }
}


