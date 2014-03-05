using UnityEngine;
using System.Collections;

public class MainMenuGui : MonoBehaviour
{

	GameState state;

	private float top;
	private float left;
	private float height;
	private float width;
	private Rect menuWindowRect;

	public GUISkin skin;

	float space = 0;

	void Start ()
	{
		state = GameState.Instance;
	}

	void Update ()
	{
		height = Screen.height;
		width = 450;
		top = 1;
		left = (Screen.width / 2) - (width/2);
		
		menuWindowRect = new Rect (0, top, width, height);
	}

	void OnGUI ()
	{
		GUI.skin = skin;
		menuWindowRect = GUI.Window (1, menuWindowRect, windowFunction, "");
	}

	void windowFunction (int windowID)
	{
		if (GUILayout.Button ("Level 1"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.SetLevel("level1");
		}

		GUILayout.Space(space);

		if (GUILayout.Button ("Level 2"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.SetLevel("level2");
		}

		GUILayout.Space(space);

		if (GUILayout.Button ("Level 3"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.SetLevel("level3");
		}

		GUILayout.Space(space);
		
		if (GUILayout.Button ("Level 4"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.SetLevel("level4");
		}

		GUILayout.FlexibleSpace();

		if (GUILayout.Button ("Options"))
		{
			Debug.Log ("Moving to Options");
			GameState.Instance.SetLevel(GameState.OPTIONS_MENU);
		}
	}
}
