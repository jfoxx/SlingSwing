using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour
{

	public Transform target;
	public GameObject finish;
	Vector3 targetPosition;

	
	Vector3 startPosition = new Vector3(0,0,-10);
	
	void Start ()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		targetPosition = startPosition;
	}

	void FixedUpdate ()
	{
		if (target != null) {

			if(target.gameObject == null){
				targetPosition = startPosition;
			}else{
				targetPosition = new Vector3 (target.transform.position.x, target.transform.position.y, -10);
			}

		}

		if (targetPosition != transform.position) {
			transform.position = Vector3.Lerp (transform.position, targetPosition, 0.2f);
		}

	}
}
