using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCreator: MonoBehaviour {
	public GameObject portalPrefab;

	// Update is called once per frame
	void Update() {
		GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

		if (portals.Length == 0) {
			// We need to create portals
			Room room = Player.State.currentRoom;

			string parentRoomKey = room.GetParentRoomKey();			

			int numPortals = room.folders.Count + parentRoomKey == "" ? 0 : 1;

			for (int i = 0; i < numPortals; i++) {
				var instance = Instantiate(portalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
				Portal portalScript = instance.GetComponent<Portal>();
				var textMesh = instance.GetComponentInChildren<TextMesh>();
				if (parentRoomKey != "" && i == numPortals - 1) {
					Room parentRoom = Player.State.roomMap[parentRoomKey];
					portalScript.roomPath = parentRoom.Path;
					textMesh.text = parentRoom.Path;
				} else {
					textMesh.text = room.folders[i].name;
					portalScript.roomPath = room.Path + "/" + room.folders[i].name;
				}
			}
		}
	}
}
