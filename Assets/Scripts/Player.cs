using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public static GameState State = new GameState();
	// Start is called before the first frame update
	public void Start() {
		if (State.roomMap == null) {
			var jsonTextFile = Resources.Load<TextAsset>("dir");
			var directoryStructure = JsonUtility.FromJson<DirectoryStructure>(jsonTextFile.text);
			State.Load(directoryStructure);
		}
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void OnTriggerEnter(Collider other) {
		Portal destination = other.gameObject.GetComponent<Portal>();
		if (destination != null) {
			if (destination.roomPath == "") {
				WinRoom();
			} else {
				if (!State.roomMap.ContainsKey(destination.roomPath)) {
					Debug.LogError("destination.roomPath not found in roomMap: " + destination.roomPath, destination);
				}
				State.currentRoom = State.roomMap[destination.roomPath];

				NextRoom();
			}
		}

		if(other.gameObject.name == "FallStop") {
			SceneManager.LoadScene(State.SceneIndex);
		}

		if (other.gameObject.name.StartsWith("Sphere")) {
			FileScript fileScript = other.gameObject.GetComponent<FileScript>();
			Player.State.currentFile = fileScript.fileName;
		}
	}

	private void NextRoom() {
		var roomMaxIndex = SceneManager.sceneCountInBuildSettings - 2;
		var roomMinIndex = 1;

		State.SceneIndex = State.SceneIndex >= roomMaxIndex
			? roomMinIndex
			: State.SceneIndex + 1;
		SceneManager.LoadScene(State.SceneIndex);
	}

	public void RePlay() {
		Start();
		State.NewGame();
		NextRoom();
	}

	public void Quit() {
		Application.Quit();
	}

	private void WinRoom() {
		State.SceneIndex = 4;
		SceneManager.LoadScene("Win");
	}
}
