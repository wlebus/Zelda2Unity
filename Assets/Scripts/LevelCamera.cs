using UnityEngine;
using System.Collections;

public class LevelCamera : MonoBehaviour {

	public bool YFollow = true;

	protected GameObject m_Player;
	protected LevelObject m_Level;
	protected Rect m_MaxPan;

	// Use this for initialization
	void Start () {
		m_Player = GameObject.Find ("Link");
		m_Level = GameObject.Find ("Level").GetComponent<LevelObject>();
		m_Level.OnSubLevelChange += SubLevelChange;

		CalcPanExtents (m_Level.GetCurrentSubLevel ());
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate() {
		var v3 = transform.position;
		v3.x = Mathf.Clamp(m_Player.transform.position.x, m_MaxPan.xMin, m_MaxPan.xMax);
		v3.y = m_MaxPan.yMin; // make sure we hug the buttom
		transform.position = v3;
	}

#region Delagates
	void SubLevelChange(SubLevelObject newSubLevel)
	{
		CalcPanExtents (newSubLevel);
	}
#endregion

	void CalcPanExtents(SubLevelObject SubLevel){
		float vertExtent = Camera.main.camera.orthographicSize;   
		float horzExtent = vertExtent * Screen.width / Screen.height;
		Bounds mapExtents = SubLevel.GetBounds();
		print (mapExtents);

		// Calculations assume map is position at the origin
		m_MaxPan.xMin = mapExtents.min.x + horzExtent;
		m_MaxPan.xMax = mapExtents.max.x - horzExtent;
		m_MaxPan.yMin = mapExtents.min.y + vertExtent;
		m_MaxPan.yMax = mapExtents.max.y - vertExtent;
	}
}
