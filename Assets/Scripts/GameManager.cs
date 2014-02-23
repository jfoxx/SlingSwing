using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public GameState state;


	bool gameStarted;
	bool playerDead 	= false;
	bool playerFinished = false;
	bool playerStarted 	= false;

	float respawnTime 	= 2f;

	int playerScore 	= 0;

	float playtime 		= 0f;

	float currentHighScore = 0f;

	Rect retryWindowRect;
	public GUISkin skin;

	PlayerControll playerControll;
	GameObject playerObject;

	void Start () 
	{
		playerObject 		= GameObject.FindGameObjectWithTag("Player");
		playerControll 		= playerObject.GetComponent<PlayerControll>();

		state 				= GameState.Instance;

		playerDead 			= false;
		playerFinished		= false;

		playtime 			= 0f;

		currentHighScore 	= PlayerPrefs.GetFloat("HigScore_" + state.currentLevel);

		unPause();
	}
	
	void Update () {

		if(!playerDead && !playerFinished && playerStarted){
			playtime += Time.deltaTime;
		}

		float height 	= Screen.height - 50f;
     	float width 	= 450;
		float top 		= (Screen.height / 2) - (height/2);
		float left 		= (Screen.width / 2) - (width/2);

		retryWindowRect = new Rect (left, top, width, height);

	}

	void OnPlayerStarted()
	{
		playerStarted = true;
	}

	void OnPlayerDied ()
	{
		playerDead = true;
	}

	void OnPlayerFinished ()
	{
		Invoke("GoToMainMenu", 3);
		playerFinished = true;
	   	if(currentHighScore > playtime || currentHighScore < 1f){
			PlayerPrefs.SetFloat("HigScore_" + state.currentLevel, playtime);
		}
	}

	void GoToMainMenu()
	{
		Debug.Log("GoToManMenu");
		state.setLevel(GameState.MAIN_MENU);

	}

	void Pause(){
		Time.timeScale = 0f;
	}

	void unPause(){
		Time.timeScale = 1f;
	}

	void Respawn()
	{
		state.setLevel(state.currentLevel);
	}

	void OnGUI(){

		GUI.skin = skin;

		float height = 50f;
		float width = 160f;
		float top = 15f;
		float left = Screen.width - width;

		string minutes = Mathf.Floor(playtime / 60).ToString("00");
		string seconds = (playtime % 60).ToString("00");

		GUI.Label(new Rect(left /2 , top, width, height), "Time: " + minutes + ":" + seconds );

//		string minutesHS = Mathf.Floor(currentHighScore / 60).ToString("00");
//		string secondsHS = (currentHighScore % 60).ToString("00");
//
//		GUI.Label(new Rect(left, top -15, width, height), "High Score: " + minutesHS + ":" + secondsHS );

		GUI.Label(new Rect(0, top, width, height), "life: " + (int) playerControll.health);



		if(playerDead)
		{
			retryWindowRect = GUI.Window (2, retryWindowRect, windowFunction, "");
		}

	}
	
	void windowFunction (int windowID)
	{
		if (GUILayout.Button ("Respawn"))
		{
			Debug.Log ("Reloading level");
			unPause();
			GameState.Instance.setLevel(state.currentLevel);
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Back to Menu"))
		{
			Debug.Log ("Moving to main menu");
			state.setLevel(GameState.MAIN_MENU);
		}
	}

}
