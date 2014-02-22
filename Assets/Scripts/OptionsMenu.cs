using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour {

	private Rect optionsWindowRect;

	private bool difficultyMenu = false;

	GameState state;

	public GUISkin skin;

	public float space = 20f;

	void Start () {
		state = GameState.Instance;
	}
	
	void Update () {

		float height = Screen.height;
		float width = 450f;
		float top = 0f;
		float left = (Screen.width / 2) - (width/2);
		
		optionsWindowRect = new Rect (left, top, width, height);
	
	}

	void OnGUI()
	{
		GUI.skin = skin;

		if(!difficultyMenu)
		{
			optionsWindowRect = GUI.Window (1, optionsWindowRect, OptionsWindowFunction, "");
		}

		if(difficultyMenu)
		{
			optionsWindowRect = GUI.Window (2, optionsWindowRect, DifficultyWindowFunction, "");
		}
	}

	void OptionsWindowFunction (int windowID)
	{
		if (GUILayout.Button ("Difficulty"))
		{
			difficultyMenu = true;
		}
		
		GUILayout.Space(30);
		
		if (GUILayout.Button ("Back to Menu"))
		{
			state.setLevel(GameState.MAIN_MENU);
		}
	}

	void DifficultyWindowFunction (int windowID)
	{
		if (GUILayout.Button ("nOOb"))
		{
			state.setDifficulty(Difficulty.nOOb);
			difficultyMenu = false;
		}

		GUILayout.Space(space);

		if (GUILayout.Button ("Normal"))
		{
			state.setDifficulty(Difficulty.Normal);
			difficultyMenu = false;
		}

		GUILayout.Space(space);

		if (GUILayout.Button ("Hard"))
		{
			state.setDifficulty(Difficulty.Hard);
			difficultyMenu = false;
		}

		GUILayout.Space(space);

		if (GUILayout.Button ("Expert"))
		{
			state.setDifficulty(Difficulty.Expert);
			difficultyMenu = false;
		}

		GUILayout.Space(space);
		
		if (GUILayout.Button ("Back to Menu"))
		{
			Debug.Log ("Moving to main menu");
			state.setLevel(GameState.MAIN_MENU);
		}
	}
}
