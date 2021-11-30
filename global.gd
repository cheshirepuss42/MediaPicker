extends Node

var current_scene = null
var bla="poep"
func _ready():
	pass
	#var root = get_tree().get_root()
	#current_scene = root.get_child(root.get_child_count() -1)
func dir_contents(path):
	var files = []
	var dir = Directory.new()
	if dir.open(path) == OK:
		dir.list_dir_begin()
		var file_name = dir.get_next()
		while file_name != "":
			if !dir.current_is_dir():
				files.append(file_name)
			file_name = dir.get_next()
	else:
		print("An error occurred when trying to access the path.")
	return files
