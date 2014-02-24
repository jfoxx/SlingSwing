using UnityEngine;
using System.Collections;

public class Grapple : MonoBehaviour {

	Vector2 targetPosition;
	GameObject player;
	Transform energyField;
	public bool hitSet;
	public float raycastDistance = 1000;
	string myguitext = "text";

	public virtual bool touchExit {
		get {
			if(Input.touchCount > 0)
			{
				return Input.GetTouch(0).phase == TouchPhase.Ended;
			} else {
				return false;
			}
		} 
	}

	void Start () {
		targetPosition 	= transform.position;
		energyField 	= transform.FindChild("EnergyField");
		player 			= GameObject.FindGameObjectWithTag("Player");

	}


	void Update () {

		if(player == null){return;}

		ShootGrappleTouch();

		ShootGrappleMouse();


		if(touchExit || Input.GetMouseButtonUp(0))
		{
			hitSet = false;
		}
		
		energyField.gameObject.SetActive( hitSet );

		transform.position = targetPosition;

	}

	void ShootGrappleTouch()
	{
		if(Input.touchCount > 0)
		{
			
			if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
				
				Vector3 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, 
				                               Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 
				                               0 );
				
				RaycastHit2D hit = Physics2D.Raycast (player.transform.position, (touchPos - player.transform.position).normalized, raycastDistance );
				
				if ( hit.transform != null ) 
				{
					Debug.Log ("grabbed " + hit.transform.name);
					targetPosition = hit.point;
					
					transform.parent = hit.transform;
					hitSet = true;
					player.transform.SendMessage("OnHitSet");
					
				} else {
					player.transform.SendMessage("OnHitExit");
					hitSet = false;
				}
			}
		}
	}

	void ShootGrappleMouse()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			
			Vector3 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, 
			                               Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
			                               0 );
			
			RaycastHit2D hit = Physics2D.Raycast (player.transform.position, (touchPos - player.transform.position).normalized, raycastDistance);
			
			if (hit != null && hit.transform != null) 
			{
				Debug.Log ("grabbed " + hit.transform.name);
				targetPosition = hit.point;
				transform.parent = hit.transform;
				hitSet = true;
				player.transform.SendMessage("OnHitSet");
				
			} else {
				hitSet = false;
				player.transform.SendMessage("OnHitExit");
				Debug.Log ("its a miss ");
			}
		}
	}
}
