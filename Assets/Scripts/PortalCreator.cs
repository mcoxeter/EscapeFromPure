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

			Room parentRoom = Player.State.roomMap[parentRoomKey];

			int numPortals = room.folders.Count + 1;

			//float dtheta = (float)(2 * Math.PI / numPortals);
			//float radius = 5;
			//float theta = 0;
			for (int i = 0; i < numPortals; i++) {
				//float x1 = (float)(0 + radius * Math.Cos(theta));
				//float z1 = (float)(0 + radius * Math.Sin(theta));
				//theta += dtheta;
				var instance = Instantiate(portalPrefab, new Vector3(0, 0, 0), Quaternion.identity);
				Portal portalScript = instance.GetComponent<Portal>();
				var textMesh = instance.GetComponentInChildren<TextMesh>();
				if (i == numPortals - 1) {
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
