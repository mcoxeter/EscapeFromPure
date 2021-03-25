using System;
using System.Collections.Generic;
using UnityEngine;

public class CircularLayout: MonoBehaviour {
	public string TargetTagName;
	public float Radius;
	void Update() {
		// Layout 
		GameObject[] targets = GameObject.FindGameObjectsWithTag(TargetTagName);

		float dtheta = (float)(2 * Math.PI / targets.Length);
		float theta = 0;
		for (int i = 0; i < targets.Length; i++) {
			float x1 = (float)(0 + Radius * Math.Cos(theta));
			float z1 = (float)(0 + Radius * Math.Sin(theta));
			theta += dtheta;
			targets[i].transform.position = new Vector3(x1, 0, z1);
		}
	}
}
