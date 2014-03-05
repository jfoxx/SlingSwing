using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour
{

	GameState state;

	public Transform manager;
	public Transform exposionPrefab;
	public GUISkin skin;

	private AudioSource audioSource;
	public AudioClip hurt;
	public AudioClip bump;
	public SpringJoint2D spring;

	bool hitSet = false;
	bool iMustDie = false;
	bool isFinished = false;
	bool isDead = false;
	bool isHurt = false;
	bool isHurting = false;
	float maxSpeed = 50;

	public bool playerStarted = false;

	SpriteRenderer spriteRenderer;
	Color playerDefaultColor;

	Vector2 respawnPosition;

	public Health health;

	
	public virtual bool TouchExit 
	{
		get {
			if( Input.touchCount > 0 )
			{
				return Input.GetTouch(0).phase == TouchPhase.Ended;
			} else {
				return false;
			}
		} 
	}

	
	void Start ()
	{
		state 	= GameState.Instance;
		manager = GameObject.FindGameObjectWithTag("GameManager").transform;


		spriteRenderer 	= gameObject.GetComponentInChildren<SpriteRenderer>();
		audioSource 	= GetComponent<AudioSource> ();
		playerDefaultColor 	= spriteRenderer.color;

		spring 					= GetComponent<SpringJoint2D> ();
		spring.enabled 			= false;
		spring.connectedBody 	= GameObject.FindGameObjectWithTag("GrapplePoint").rigidbody2D;

		iMustDie 				= false;
		playerStarted 			= false;
		rigidbody2D.isKinematic = true;

		respawnPosition = GameObject.FindGameObjectWithTag("Start").transform.position;

		health = Health.Low;

	}

	void Update ()
	{
		if (TouchExit|| Input.GetMouseButtonUp(0)) {
			hitSet = false;
		}

		if(Input.GetMouseButtonDown(0)){
			playerStarted = true;
			rigidbody2D.isKinematic = false;
			manager.SendMessage("OnPlayerStarted");
		}

		spring.enabled = hitSet;

		if(iMustDie && !isDead){
			Die();
		}

		if( isHurt && isHurting){
			isHurting = false;
			Invoke( "StopBeingHurt", 2 );
		}

		if(isHurt){
			float lerp = Mathf.PingPong(Time.time, 0.2f) / 0.2f;
			spriteRenderer.color = Color.Lerp(Color.magenta, Color.yellow, lerp);
		}

		rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
	}
	
	void StopBeingHurt()
	{
		isHurt = false;
		spriteRenderer.color = playerDefaultColor;
	}

	void OnHitSet()
	{
		Debug.Log("OnHitSet()");
		hitSet = true;
	}

	void OnHitExit()
	{
		Debug.Log("OnHitExit()");
		hitSet = false;
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if(coll.transform.CompareTag("Self")){return;}
		if(coll.transform.CompareTag("KillZone")  )
		{
			if( !isHurt)
			{
				isHurting 	= true;
				isHurt 		= true;
				audio.PlayOneShot(hurt);
				ReduceHealth();
			}
		} else {
			audio.PlayOneShot(bump);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		Debug.Log("in da trigger " + other.transform.name);
		
		if(other.transform.CompareTag("Finish")){
			Finish();
		}

		if(other.transform.CompareTag("Respawn")){
			respawnPosition = other.transform.position;
		}
	}

	void ReduceHealth()
	{
		Debug.Log(health);

		health = (Health)((int) health -1);

		if ((int) health < 1) {
			iMustDie = true;
		}

	}

	void Die()
	{
		iMustDie 				= false;
		playerStarted 			= false;
		rigidbody2D.isKinematic = true;

		Instantiate(exposionPrefab, transform.position, Quaternion.identity);

		manager.SendMessage("OnPlayerDied", SendMessageOptions.DontRequireReceiver);

		gameObject.SetActive(false);
		Invoke("Respawn", 1);
	}

	void Respawn()
	{
		transform.position = respawnPosition;
		health = Health.Low;
		isDead = false;
		gameObject.SetActive(true);
		//manager.SendMessage("OnPlayerStarted", SendMessageOptions.DontRequireReceiver);


	}

	void Finish()
	{
		rigidbody2D.velocity = Vector3.zero;
		manager.SendMessage("OnPlayerFinished", SendMessageOptions.DontRequireReceiver);
		FreezePlayer();
	}

	void FreezePlayer()
	{
		rigidbody2D.isKinematic = true;
		enabled = false;
	}

	void OnGUI()
	{
		GUI.skin = skin;
		float vel = rigidbody2D.velocity.magnitude;
		if(vel < 0.5f){
			vel = 0;
		}
		float center = (Screen.width / 2) - 160;
		GUI.Label(new Rect(center, 0, 350, 50), "Velocity: " + vel.ToString("F0"));
//		GUILayout.Space(50);
//		GUILayout.Label("hurt: " + isHurt);
//		GUILayout.Label("hurting: " + isHurting);
	}

}
