using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(Player.State != null && Player.State.currentRoom != null) {
			var text = GetComponent<Text>();
			text.text = Player.State.currentRoom.Path + (Player.State.currentFile != "" ? "/" + Player.State.currentFile : "");
		}
	}
}
