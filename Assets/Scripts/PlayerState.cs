using System;
using System.Collections.Generic;

public class GameState {

	public Dictionary<string, Room> roomMap = null;
	private List<Room> roomList = new List<Room>();
	public Room currentRoom = null;
	public string currentFile = "";

	public int SceneIndex = 0;

	public Random random = new Random(DateTime.Now.Millisecond);

	public void Load(DirectoryStructure directoryStructure) {
		roomMap = new Dictionary<string, Room>();
		foreach (DirEntry entry in directoryStructure.dirEntries) {
			add(entry);
		}

		NewGame();
	}

	public void NewGame() {
		var fileCount = 0;

		do {
			currentRoom = roomMap["pure-core"];
			fileCount = currentRoom.files.Count;
		} while (fileCount == 0);		
	}

	private void add(DirEntry entry) {
		if (entry.type == "file") {
			addFile(entry);
		}
		if (entry.type == "directory") {
			addDirectory(entry);
		}
	}

	private void addFile(DirEntry entry) {
		roomMap[entry.path].files.Add(entry);
	}
	private void addDirectory(DirEntry entry) {
		var directoryPath = entry.path.Length > 0
			? entry.path + "/" + entry.name
			: entry.name;

		createIfNeeded(directoryPath);
		createIfNeeded(entry.path);
		roomMap[entry.path].folders.Add(entry);
	}

	private void createIfNeeded(string path) {
		if (!roomMap.ContainsKey(path)) {
			var room = new Room(path);
			roomMap.Add(path, room);
			roomList.Add(room);
		}
	}
}

[Serializable]
public class DirectoryStructure {
	public DirEntry[] dirEntries;
}

[Serializable]
public class DirEntry {
	public string name;
	public string path;
	public int size;
	public string type;
}

public class Room {
	public Room(string path) {
		Path = path;
	}

	public string Path;
	public List<DirEntry> files = new List<DirEntry>();
	public List<DirEntry> folders = new List<DirEntry>();

	public string GetParentRoomKey() {
		int index = Path.LastIndexOf('/');
		if (index > 0) {
			return Path.Substring(0, index);
		}
		return "";
	}
}