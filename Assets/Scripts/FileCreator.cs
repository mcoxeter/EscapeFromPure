using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileCreator : MonoBehaviour
{
	public GameObject filePrefab;

	// Update is called once per frame
	void Update()
	{
		GameObject[] files = GameObject.FindGameObjectsWithTag("File");

		if (files.Length == 0)
		{
			// We need to create portals
			Room room = Player.State.currentRoom;

			string parentRoomKey = room.GetParentRoomKey();

			Room parentRoom = Player.State.roomMap[parentRoomKey];

			int numFiles = room.files.Count;

            float x = Player.State.random.Next(-2, 2);
			float z = Player.State.random.Next(-2, 2);

			for (int i = 0; i < numFiles; i++)
			{
				var instance = Instantiate(filePrefab, new Vector3(x, 4, z), Quaternion.identity);
				var textMesh = instance.GetComponentInChildren<TextMesh>();
				textMesh.text = room.files[i].name;
			}
		}
	}
}
