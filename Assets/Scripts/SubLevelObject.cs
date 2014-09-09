using UnityEngine;
using System.Collections;

public class SubLevelObject : MonoBehaviour {

	protected Bounds m_Bounds;

	public Bounds GetBounds() {
		return m_Bounds;
	}

	// Use this for initialization
	void Start () {
		m_Bounds = GetComponent<SpriteRenderer>().sprite.bounds;
		m_Bounds.center += transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
