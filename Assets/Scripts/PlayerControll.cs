using UnityEngine;
using System.Collections;

public class PlayerControll : MonoBehaviour
{

	public Transform manager;
	public Transform exposionPrefab;

	private AudioSource audioSource;
	public SpringJoint2D spring;

	bool hitSet = false;
	bool iMustDie = false;
	bool isDead = false;

	public float maxSpeed = 10;

	void Start ()
	{
		audioSource = GetComponent<AudioSource> ();

		spring = GetComponent<SpringJoint2D> ();
		spring.enabled = false;
		iMustDie = false;
	}

	void Update ()
	{
		if (TouchExit|| Input.GetMouseButtonUp(0)) {
			hitSet = false;
		}
		spring.enabled = hitSet;

		if(iMustDie && !isDead){
			Die();
		}
	}

	void FixedUpdate ()
	{
		if(rigidbody2D.velocity.magnitude > maxSpeed)
		{
			rigidbody2D.velocity = rigidbody2D.velocity.normalized * maxSpeed;
		}
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
			if(Input.touchCount > 0)
			{
				return Input.GetTouch(0).phase == TouchPhase.Ended;
			} else {
				return false;
			}
		} 
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if(coll.transform.CompareTag("Finish")){
			Finish();
			iMustDie = true;
		} else {
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

	void Finish(){
		manager.SendMessage("OnPlayerFinished", SendMessageOptions.DontRequireReceiver);
	}

	void OnGUI()
	{
		GUI.Label(new Rect(5, 300, 200, 50), "Velocity: " + rigidbody2D.velocity.magnitude);
	}

}
