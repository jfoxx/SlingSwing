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
	public SpringJoint2D spring;

	bool hitSet = false;
	bool iMustDie = false;
	bool isFinished = false;
	bool isDead = false;
	bool isHurt = false;
	bool isHurting = false;
	float maxSpeed = 10;

	public bool playerStarted = false;

	SpriteRenderer spriteRenderer;
	Color playerDefaultColor;

	public Health health;

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

		if(state.currentDifficulty == Difficulty.Expert) {
			health = Health.Low;
		}
		else {
			health = Health.Full;
		}

		maxSpeed = (int) state.currentDifficulty;
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
	}

	void FixedUpdate ()
	{
		rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
	}

	void StopBeingHurt(){
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

	void OnCollisionStay2D (Collision2D coll)
	{

		if( !coll.transform.CompareTag("SafeZone") ){
			if( !isHurt ){
				isHurting 	= true;
				isHurt 		= true;
				audio.PlayOneShot(hurt);
				ReduceHealth();
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){

		Debug.Log("in da trigger " + other.transform.name);

		if(other.transform.CompareTag("Finish")){
			Finish();
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
		isDead = true;
		Instantiate(exposionPrefab, transform.position, Quaternion.identity);
		Debug.Log("died!");
		manager.SendMessage("OnPlayerDied", SendMessageOptions.DontRequireReceiver);
		Destroy(gameObject);
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
		GUI.Label(new Rect(5, 300, 200, 50), "Velocity: " + rigidbody2D.velocity.magnitude);
//		GUILayout.Space(50);
//		GUILayout.Label("hurt: " + isHurt);
//		GUILayout.Label("hurting: " + isHurting);
	}

}
