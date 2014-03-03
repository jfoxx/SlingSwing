using UnityEngine;
using System.Collections;

public class EmitParticles : MonoBehaviour {

	public Transform particlePrefab;
	int frames = 5;
	int counter = 0;
	void FixedUpdate () 
	{
		if(counter > frames)
		{
			Emit();
			counter = 0;
		} else {
			counter ++;
		}
	}

	void Emit()
	{

		Vector2 spawn = new Vector2(
			Random.Range(transform.position.x-0.2f, transform.position.x + 0.2f),
			Random.Range(transform.position.y-0.2f, transform.position.y + 0.2f));
		Instantiate(particlePrefab, spawn, Quaternion.identity );
	}
}
