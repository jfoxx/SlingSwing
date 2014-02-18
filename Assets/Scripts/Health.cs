using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

	public Transform explosionPrefab;
	public float health;
	public float max_health = 100;
	
	GameObject manager;

	void Start ()
	{
		health = max_health;

		manager = GameObject.Find ("Manager");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (health <= 0) {
			health = 0;
			die ();
		}
	}
	

	void onKillZone ()
	{
		health = 0;
	}
	
	void die ()
	{
		if(explosionPrefab != null)
		{
			Network.Instantiate(explosionPrefab, transform.position, Quaternion.identity,0);
		}
	}
	
	void OnDestroy ()
	{
		Debug.Log ("i was destroyed");        
	}

	void OnGUI(){
		GUI.Label(new Rect(Screen.width-100, Screen.height - 30, 100, 30), "health: " + health);
	}
}
