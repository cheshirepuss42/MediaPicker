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
    private MediaPicker Picker;

    public override void _Ready()
    {
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
    }
    public void _on_SearchBar_text_changed(string newText)
    {
    }
    public void _on_OpenFolderButton_pressed()
    {

    }

    // Collections
    public ItemList FoldersList() { return GetNode<ItemList>("Tabs/Collections/Sections/CurrentCollection/FoldersList"); }
    public ItemList CollectionsList() { return GetNode<ItemList>("Tabs/Collections/Sections/Collections/CollectionsList"); }
    public void _on_AddFolderButton_pressed()
    {
        GetNode<FileDialog>("FolderFindDialog").Popup_();
    }
    public void _on_FolderFindDialog_dir_selected(string dir)
    {
        FoldersList().AddItem(dir);
    }
    public void _on_RemoveFolderButton_pressed()
    {
        foreach(var selected in FoldersList().GetSelectedItems())
        {
            FoldersList().RemoveItem(selected);
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
    }





}


