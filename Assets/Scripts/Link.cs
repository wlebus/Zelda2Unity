using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	protected enum Facing {
		NONE = -1,
		RIGHT = 0,
		LEFT
	};

	public float MaxSpeed = 1.0f;
	public float JumpForce = 140.0f;
	public AudioClip AttackSFX = null;

	protected Animator m_Animator;
	protected LevelObject m_LevelObject;

	protected Facing m_Facing;
	protected bool m_Crouching;


	// Use this for initialization
	void Start () {
		m_Facing = Facing.RIGHT;
		m_Crouching = false;
		m_Animator = GetComponent<Animator> ();
		m_LevelObject = GameObject.Find ("Level").GetComponent<LevelObject> ();
		Transform startObj = m_LevelObject.StartLocation; 

		rigidbody2D.position = startObj.position;

	}

	void FixedUpdate () {
		float move = Input.GetAxis ("Horizontal");
		float vert = Input.GetAxis ("Vertical");

		m_Crouching = vert < -0.5f;
		
		if(m_Crouching){
			m_Animator.SetBool ("Crouching", true);
		}
		else{
			m_Animator.SetBool ("Crouching", false);

			m_Animator.SetFloat ("Speed", Mathf.Abs (move));
			rigidbody2D.velocity = new Vector2 (move * MaxSpeed, rigidbody2D.velocity.y);
		}

		
		if (move > 0.0f && m_Facing != Facing.RIGHT) {
			ChangeFacing (Facing.RIGHT);
		}
		else if (move < 0.0f && m_Facing != Facing.LEFT) {
			ChangeFacing (Facing.LEFT);
		}

	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Jump")) {
			rigidbody2D.AddForce (new Vector2(0.0f, JumpForce));
		}
		if (Input.GetButtonDown ("Fire1")) {
			m_Animator.SetTrigger ("Attack");
			audio.PlayOneShot (AttackSFX);
		}
	}

	void ChangeFacing(Facing newFacing)
	{
		if (m_Facing != newFacing) {
			m_Facing = newFacing;
			Vector3 newScale = transform.localScale;
			newScale.x *= -1.0f;
			transform.localScale = newScale;
		}
	}


}
