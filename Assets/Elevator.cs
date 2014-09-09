using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	public float Speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float vert = Input.GetAxis ("Vertical");

		Vector3 newPos = transform.position;
		newPos.y += vert * Speed;
		transform.position = newPos;
	}
}
