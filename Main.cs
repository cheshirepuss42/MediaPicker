using Godot;
using System;

/*
TODO:
-link the signals from the ui to the app:
    -signalhandler?
-serialization
    -settings-model
-search and random functionality

*/

public class Main : Control
{
    public ItemList FoldersList, CollectionsList, SearchResults,History;
    public LineEdit SearchTerm;
    private MediaPicker Picker;

    public override void _Ready()
    {

        FoldersList = GetNode<ItemList>("Tabs/Collections/Sections/CurrentCollection/FoldersList");
        CollectionsList = GetNode<ItemList>("Tabs/Collections/Sections/Collections/CollectionsList");
        SearchResults = GetNode<ItemList>("Tabs/Picker/Sections/Search/SearchResult");
        SearchTerm = GetNode<LineEdit>("Tabs/Picker/Sections/Search/SearchBar");
        History = GetNode<ItemList>("Tabs/History/HistoryItems");
        Picker = new MediaPicker();
    }

    // Picker
    public void _on_RandomFromFolder_pressed()
    {
    }
    public void _on_RandomFromCollectionButton_pressed()
    {
    }
    public void _on_NextButton_pressed()
    {
    }
    public void _on_PrevButton_pressed()
    {
    }
    public void _on_AutoPlayCheckBox_pressed()
    {
    }
    public void _on_PlayButton_pressed()
    {
    }
    public void _on_SearchResult_item_activated(int index)
    {
        string item = SearchResults.GetItemText(index);
        Picker.PlayFile(item);

    }
    public void _on_SearchBar_text_changed(string newText)
    {
        SearchResults.Clear();
        foreach(var result in Picker.SearchResults(SearchTerm.Text))
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
        Picker.PlayFile(History.GetItemText(index));
    }
}


