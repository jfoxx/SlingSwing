using UnityEngine;
using System.Collections;

public class RingParticle : MonoBehaviour {

	float killTime = 4f;

	void Start () 
	{
		float rand = Random.Range( 0.1f, 1f );

		transform.localScale = new Vector3(rand,rand,rand);

		Invoke("Kill", Random.Range(1f , killTime));
	}

	void Kill()
	{
		Destroy(gameObject);
	}
}
