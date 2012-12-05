using UnityEngine;
using System;
using System.Collections;

public class Controller : MonoBehaviour {
	void Start() {
		InvokeRepeating("testSend", 1, 1);	
	}
	int ctr = 0;
	public void testSend() {
		if (ctr > 255) ctr = 0;
		Byte[] bytes = { 0x0A, 0x00, (Byte)(ctr++), 0xFF };
		Server.Send(bytes);
	}
	
	void Update () {
		int touchCount = Input.touchCount;
		
		if (touchCount > 0) {
			Touch[] touches = Input.touches; 
			
			Debug.Log(touches[0].deltaPosition);
		}	
	}
}
