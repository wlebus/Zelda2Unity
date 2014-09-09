using UnityEngine;
using System.Collections;

public class LevelObject : MonoBehaviour {

	public delegate void SubLevelChange(SubLevelObject newSubLevel);
	public SubLevelChange OnSubLevelChange;

	public SubLevelObject DefaultSubLevel;
	public Transform StartLocation;

	protected Link m_Player;
	protected SubLevelObject m_CurrentSubLevel;

	public SubLevelObject GetCurrentSubLevel() {
		return m_CurrentSubLevel;
	}

	SubLevelObject FindSubLevel (Vector3 Position)
	{
		SubLevelObject ret = null;
		foreach(var obj in GetComponentsInChildren<SubLevelObject>()){
			if(obj.GetBounds().Contains (Position)){
				ret = obj;
				break;
			}
		}
		return ret;
	}

	// Use this for initialization
	void Start () {
		m_Player = GameObject.Find ("Link").GetComponent<Link>();
		m_CurrentSubLevel = DefaultSubLevel;
	}
	
	// Update is called once per frame
	void Update () {
		Bounds curSubBounds = m_CurrentSubLevel.GetBounds();
		if (!curSubBounds.Contains (m_Player.transform.position)) {
			SubLevelObject newSubLevel = FindSubLevel(m_Player.transform.position);
			m_CurrentSubLevel = newSubLevel;
			if(OnSubLevelChange != null){
				OnSubLevelChange(m_CurrentSubLevel);
			}
			
		}

	}
}
