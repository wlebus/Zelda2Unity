using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {
	public Transform Top;
	public Transform Bottom;
	public float Speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
		float vert = Input.GetAxis ("Vertical");

		Vector3 newPos = transform.position;
		newPos.y += vert * Speed;
		newPos.y = Mathf.Clamp (newPos.y, Bottom.position.y, Top.position.y);
		transform.position = newPos;

	}
}
