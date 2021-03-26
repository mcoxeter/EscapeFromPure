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

			int numFiles = room.files.Count;

            float x = Player.State.random.Next(-2, 2);
			float z = Player.State.random.Next(-2, 2);
			float y = Player.State.random.Next(4, 10);

			for (int i = 0; i < numFiles; i++)
			{
				var instance = Instantiate(filePrefab, new Vector3(x, y, z), Quaternion.identity);
				float size = room.files[i].size / 1000f;
				FileScript fileScript = instance.GetComponent<FileScript>();
				fileScript.fileName = room.files[i].name;

				if ( size > 6) {
					size = 6;
				}

				instance.transform.localScale = new Vector3(1 * size, 1 * size, 1 * size);
				var textMesh = instance.GetComponentInChildren<TextMesh>();
				textMesh.text = room.files[i].name;
			}
		}
	}
}
