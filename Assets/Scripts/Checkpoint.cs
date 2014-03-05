using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public Color checkedColor;
	public AudioClip checkSound;

	CircleCollider2D circleCollider;
	SpriteRenderer spriteRenderer;


	void Start () 
	{
		circleCollider 	= gameObject.GetComponent<CircleCollider2D>();
		spriteRenderer 	= gameObject.GetComponent<SpriteRenderer>();
	}
	
	void OnTriggerEnter2D()
	{
		GetChecked();
	}

	void OnTriggerExit2D()
	{
		GetChecked();
	}

	void GetChecked()
	{
		circleCollider.enabled = false;
		spriteRenderer.color = checkedColor;
		audio.PlayOneShot(checkSound);

	}
}
