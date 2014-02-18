using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour
{

	public static readonly string MAIN_MENU = "main";

	public string currentLevel;
	public bool showMenu;

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
	}
	// Sets the instance to null when the application quits
	public void OnApplicationQuit ()
	{
		instance = null;
	}

	public void startState ()
	{
		setLevel(MAIN_MENU);
	}

	public string getLevel ()
	{
		return currentLevel;
	}

	public void setLevel (string newLevel)
	{
		currentLevel = newLevel;
		Application.LoadLevel(newLevel);
	}

	void OnGUI()
	{
		//GUI.Label(new Rect (Screen.width/2 - 15, 30, 300, 30), currentLevel);
	}
}