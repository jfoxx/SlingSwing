using UnityEngine;
using System.Collections;

public class EmitParticles : MonoBehaviour {

	public Transform particlePrefab;

	void FixedUpdate () 
	{
		Emit();
	}

	void Emit()
	{
		Vector2 spawn = new Vector2(transform.position.x, transform.position.y);
		Instantiate(particlePrefab, spawn, Quaternion.identity );
	}
}
