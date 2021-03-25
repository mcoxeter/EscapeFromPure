using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControlledCamera : MonoBehaviour {

	public GameObject Controller;
	public float speedH = 2.0f;
	public float speedV = 2.0f;
	private float yaw = 0.0f;
	private float pitch = 0.0f;

	private Vector3 cameraOffset = Vector3.zero;
	public float turnSpeed = 3;

	void Start() {
		//Get camera-player Transform Offset that will be used to move the camera 
		cameraOffset = transform.position - Controller.transform.position;
	}

	void LateUpdate() {
		//Move the camera to the position of the playerTransform with the offset that was saved in the begining
		// GameObject avatar = GameObject.FindGameObjectWithTag("Avatar");

		//transform.position = Controller.transform.position + cameraOffset;

		yaw += speedH * Input.GetAxis("Mouse X");
		pitch -= speedV * Input.GetAxis("Mouse Y");
		transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
