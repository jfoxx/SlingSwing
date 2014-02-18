using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


	public GameState state;

	bool gameStarted;
	bool playerDead = false;

	float respawnTime = 2f;

	int playerScore;
	float playtime = 0f;


	float currentHighScore = 0f;

	Rect retryWindowRect;
	public GUISkin skin;

	void Start () 
	{
		state 				= GameState.Instance;

		playerDead 			= false;
		playtime 			= 0f;

		currentHighScore 	= PlayerPrefs.GetFloat("HigScore_" + state.currentLevel);
		string minutesHS 	= Mathf.Floor(currentHighScore / 60).ToString("00");
		string secondsHS 	= (currentHighScore % 60).ToString("00");
	}
	
	void Update () {

		if(!playerDead){
			playtime += Time.deltaTime;
		}

		float height = Screen.height / 2;
     	float width = 400;
		float top = (Screen.height / 2) - (height/2);
		float left = (Screen.width / 2) - (width/2);

		retryWindowRect = new Rect ((Screen.width / 2) - (width/2), 
		                            (Screen.height / 2) - (height/2), 
		                            width, 
		                            (Screen.height / 2));

	}

	void OnPlayerDied ()
	{
		playerDead = true;
		Debug.Log("Invoking respawn in " + respawnTime + " seconds");
	}

	void OnPlayerFinished ()
	{
	   if(currentHighScore > playtime || currentHighScore < 1f){
			PlayerPrefs.SetFloat("HigScore_" + state.currentLevel, playtime);
		}
	}

	void Respawn()
	{
		state.setLevel(state.currentLevel);
	}

	void OnGUI(){

		float height = 40f;
		float width = 150f;
		float top = 30f;
		float left = (Screen.width / 2) - (width/2);

		string minutes = Mathf.Floor(playtime / 60).ToString("00");
		string seconds = (playtime % 60).ToString("00");

		GUI.Label(new Rect((Screen.width / 2) - (width/2) , top, width, height), "Time: " + minutes + ":" + seconds );

		string minutesHS = Mathf.Floor(currentHighScore / 60).ToString("00");
		string secondsHS = (currentHighScore % 60).ToString("00");

		GUI.Label(new Rect(left, top -15, width, height), "High Score: " + minutesHS + ":" + secondsHS );

		if(playerDead)
		{
			GUI.skin = skin;
			retryWindowRect = GUI.Window (2, retryWindowRect, windowFunction, "");
		}

	}
	
	void windowFunction (int windowID)
	{
		if (GUILayout.Button ("Respawn"))
		{
			Debug.Log ("Reloading level");
			GameState.Instance.setLevel(state.currentLevel);
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Back to Menu"))
		{
			Debug.Log ("Moving to main menu");
			state.setLevel(GameState.MAIN_MENU);
		}

		//		GUILayout.Space(5);
		//		
		//		if (GUILayout.Button ("Level 3"))
		//		{
		//			Debug.Log ("Moving to level 3");
		//			GameState.Instance.setLevel("level3");
		//		}
	}

}
