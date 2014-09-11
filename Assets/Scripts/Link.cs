using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

	protected enum Facing {
		NONE = -1,
		RIGHT = 0,
		LEFT
	};

	public LevelObject LevelObject;

	public float MaxSpeed = 1.0f;
	public float JumpForce = 140.0f;
	public AudioClip AttackSFX = null;

	protected Animator m_Animator;
	protected Facing m_Facing;
	protected bool m_Crouching;
	protected bool m_Attacking;

	protected Transform m_CurrElevator;

	void Awake()
	{
		m_Animator = GetComponent<Animator> ();
		m_Facing = Facing.RIGHT;
		m_Crouching = false;
		m_Attacking = false;
	}

	// Use this for initialization
	void Start () {
		m_Facing = Facing.RIGHT;
		m_Crouching = false;
		m_Animator = GetComponent<Animator> ();
		Transform startObj = LevelObject.StartLocation; 

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

		if(m_CurrElevator != null){
			Vector2 newPos = transform.position;
			newPos.y = m_CurrElevator.transform.position.y;
			rigidbody2D.position = newPos;
		}

	}
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonDown ("Jump")) {
			rigidbody2D.AddForce (new Vector2(0.0f, JumpForce));
		}
		if (!m_Attacking && Input.GetButtonDown ("Fire1")) {
			m_Attacking = true;
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

	void OnAttackDone(){
		m_Attacking = false;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<Elevator>()){
			m_CurrElevator = other.GetComponent<Transform>();
			//transform.parent = other.GetComponent<Transform>();
			//rigidbody2D.isKinematic = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.GetComponent<Elevator>()){
			m_CurrElevator = null;
			//transform.parent = null;
			//rigidbody2D.isKinematic = false;
		}
	}
}
