using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	bool gameStarted;
	bool playerDead = false;

	float respawnTime = 2f;
	float respawnTimer;

	int playerScore;
	float playtime = 0f;

	float height = 40f;
	float width = 150f;
	float top = 30f;

	float currentHighScore = 0f;

	void Start () {
		respawnTimer = respawnTime;
		playerDead = false;
		playtime = 0f;
		currentHighScore = PlayerPrefs.GetFloat("HigScore");
		string minutesHS = Mathf.Floor(currentHighScore / 60).ToString("00");
		string secondsHS = (currentHighScore % 60).ToString("00");
	}
	
	// Update is called once per frame
	void Update () {
		if(respawnTimer > 0 && playerDead){
			respawnTimer -= Time.deltaTime;
		}

		if(respawnTimer <= 0 && playerDead){
			respawn();
		}

		if(!playerDead){
			playtime += Time.deltaTime;
		}

	}

	void OnPlayerDied ()
	{
		respawnTimer = respawnTime;
		playerDead = true;
	}

	void OnPlayerFinished ()
	{
	   if(currentHighScore > playtime || currentHighScore < 1f){
			PlayerPrefs.SetFloat("HigScore", playtime);
		}
	}

	void respawn()
	{
		Application.LoadLevel(0);
	}

	void OnGUI(){

		float left = ((Screen.width / 2) -width);

		string minutes = Mathf.Floor(playtime / 60).ToString("00");
		string seconds = (playtime % 60).ToString("00");

		GUI.Label(new Rect(left, top, width, height), "Time: " + minutes + ":" + seconds );

		string minutesHS = Mathf.Floor(currentHighScore / 60).ToString("00");
		string secondsHS = (currentHighScore % 60).ToString("00");

		GUI.Label(new Rect(left, top -15, width, height), "High Score: " + minutesHS + ":" + secondsHS );

	}

}
