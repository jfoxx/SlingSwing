using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameState state;

	bool playerDead 	= false;
	bool playerFinished = false;
	bool playerStarted 	= false;

	float respawnTime 	= 2f;
	float playtime 		= 0f;

	int playerScore 	= 0;

	float currentHighScore = 0f;

	Rect windowRect;
	public GUISkin skin;
	public GUISkin hamburgerSkin;

	bool showMenu 		= false;

	PlayerControll playerControll;
	GameObject playerObject;

	void Start () 
	{
		playerObject 		= GameObject.FindGameObjectWithTag("Player");
		playerControll 		= playerObject.GetComponent<PlayerControll>();

		state 				= GameState.Instance;

		playerDead 			= false;
		playerFinished		= false;
		playerStarted 		= false;


		playtime 			= 0f;

		currentHighScore 	= PlayerPrefs.GetFloat("HigScore_" + state.currentLevel);

		unPause();
	}
	
	void Update () {

		if(!playerDead && !playerFinished && playerStarted)
		{
			playtime += Time.deltaTime;
		}

		if(showMenu || playerDead || playerFinished)
		{
			float height 	= Screen.height - 50f;
	     	float width 	= 450;
			float top 		= (Screen.height / 2) - (height/2);
			float left 		= (Screen.width / 2) - (width/2);

			windowRect = new Rect (left, top, width, height);
		}

	}

	void OnPlayerStarted()
	{
		playerStarted = true;
		playerDead = false;
	}

	void OnPlayerDied ()
	{
		playerDead = true;
	}
	
	void OnPlayerFinished ()
	{
		playerFinished = true;
	   	if(currentHighScore > playtime || currentHighScore < 1f){
			PlayerPrefs.SetFloat("HigScore_" + state.currentLevel, playtime);
		}
	}

	void GoToMainMenu()
	{
		Debug.Log("GoToManMenu");
		state.SetLevel(GameState.MAIN_MENU);
	}

	void GoToNextLevel()
	{
		state.GoToNextLevel();
	}

	void Pause(){
		Time.timeScale = 0f;
	}

	void unPause(){
		Time.timeScale = 1f;
	}

	void Respawn()
	{
		state.SetLevel(state.currentLevel);
	}

	void OnGUI(){

		GUI.skin = skin;

		float height = 50f;
		float width = 300f;
		float top = 0f;
		float left = Screen.width - width;

		GUI.Label(new Rect(left , top, width, height), "Time: " + PlayTime() );

//		GUI.Label(new Rect(left/2, top, width, height), "life: " + (int) playerControll.health);


		if(playerFinished)
		{
			windowRect = GUI.Window (2, windowRect, RetryWndow, "");
		}

		if(showMenu) {
			windowRect = GUI.Window (1, windowRect, MenuWindow, "");
			Pause();
		}

		GUI.skin = hamburgerSkin;
		if(GUI.Button(new Rect(0, 0, 80, 80), ""))
		{
			showMenu = !showMenu;
			if(showMenu){
				Pause();
			} else {
				unPause();
			}
		}
	}

	void MenuWindow (int windowID)
	{
		if (GUILayout.Button ("Resume"))
		{
			showMenu = false;
			unPause();
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Options"))
		{
			state.SetLevel(GameState.OPTIONS_MENU);
		}
		
		GUILayout.FlexibleSpace();

		if (GUILayout.Button ("Exit"))
		{
			Debug.Log ("Moving to main menu");
			state.SetLevel(GameState.MAIN_MENU);
		}
	}

	void RetryWndow (int windowID)
	{
		GUILayout.Label("Time:\t" + PlayTime() );
		GUILayout.Space(0);
		GUILayout.Label("Best:\t\t" + HighScore() );


		if (GUILayout.Button ("Retry"))
		{
			Debug.Log ("Reloading level");
			unPause();
			GameState.Instance.SetLevel(state.currentLevel);
		}

		if (GUILayout.Button ("Next"))
		{
			Debug.Log ("Next level");
			GoToNextLevel();
		}

		GUILayout.FlexibleSpace();

		if (GUILayout.Button ("Back to Menu"))
		{
			Debug.Log ("Moving to main menu");
			state.SetLevel(GameState.MAIN_MENU);
		}
	}

	private string PlayTime()
	{
		string minutes = Mathf.Floor(playtime / 60).ToString("00");
		string seconds = (playtime % 60).ToString("00");
		return minutes + ":" + seconds;
	}

	private string HighScore()
	{
		string minutes = Mathf.Floor(currentHighScore / 60).ToString("00");
		string seconds = (currentHighScore % 60).ToString("00");
		return minutes + ":" + seconds;
	}
	
}
