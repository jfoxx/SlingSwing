using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{

	public static readonly string MAIN_MENU = "main";
	public static readonly string OPTIONS_MENU = "options";

	public string currentLevel;
	public bool showMenu;
	public Difficulty currentDifficulty = Difficulty.nOOb;

	private static GameState instance;

	public static GameState Instance {
		get {
			if (instance == null) {
				instance = new GameObject ("GameState").AddComponent ("GameState") as GameState;
			}
			
			return instance;
		}
	}

	void Start(){
		DontDestroyOnLoad(instance);

		int savedDifficulty = PlayerPrefs.GetInt("PlayerDifficulty");

		SetDifficulty(Difficulty.Expert);
	}

	void Update ()
	{
		if(currentLevel == "" || currentLevel == null){
			currentLevel = Application.loadedLevelName;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			showMenu = !showMenu;
		}
		if (currentLevel == MAIN_MENU) {
			Screen.lockCursor = false;
		} else {
			Screen.lockCursor = false;
		}

	}

	void OnLevelWasLoaded(){
		showMenu = false;
		currentLevel = Application.loadedLevelName;
	}
	// Sets the instance to null when the application quits
	public void OnApplicationQuit ()
	{
		instance = null;
	}

	public void startState ()
	{
		SetLevel(MAIN_MENU);
	}

	public string GetLevel ()
	{
		return currentLevel;
	}

	public void SetLevel (string newLevel)
	{
		currentLevel = newLevel;
		Application.LoadLevel(newLevel);
	}
	
	public void GoToNextLevel()
	{
		if(Application.levelCount -1 >= Application.loadedLevel +1)
		{
			Application.LoadLevel(Application.loadedLevel + 1); 
		} else {
			SetLevel(GameState.MAIN_MENU);
		}
	}

	public void SetDifficulty(Difficulty difficulty)
	{
		Debug.Log("setDifficulty " + difficulty);
		currentDifficulty = difficulty;
		PlayerPrefs.SetInt("PlayerDifficulty", (int) difficulty);
	}

	public Difficulty getDifficulty()
	{
		return currentDifficulty;
		Debug.Log("getDifficulty");
	}

	void OnGUI()
	{
		//GUI.Label(new Rect (Screen.width/2 - 15, 30, 300, 30), currentLevel);
	}
}