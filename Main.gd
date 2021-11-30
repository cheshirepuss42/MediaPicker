extends Control

# Declare member variables here. Examples:
# var a = 2
# var b = "text"
var ResultItemsNode
var SearchResultPath = "Tabs/Picker/Sections/Search/Result"
var FileDialogPath = "FileDialog"

# Called when the node enters the scene tree for the first time.
func _ready():	
	ResultItemsNode = get_node(SearchResultPath)
	ResultItemsNode.add_item(Global.bla)


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass

func _on_ItemList_item_activated(index):
	pass#ResultItemsNode.add_item("Branch")


func _on_Button_pressed():
	var a = get_node(FileDialogPath)
	a.popup()


func _on_FileDialog_dir_selected(dir):
	#ResultItemsNode.add_item(dir)
	var dc = Global.dir_contents(dir)
	for f in dc:
		ResultItemsNode.add_item(f)


func _on_OpenFolderButton_pressed():
	#OS.shell_open(str("file://", absoluteDirPath))
	var a = get_node(FileDialogPath)
	a.popup()
