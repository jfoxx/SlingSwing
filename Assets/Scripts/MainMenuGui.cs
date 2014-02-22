﻿using UnityEngine;
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


	void Start ()
	{
		state = GameState.Instance;
	}

	void Update ()
	{
		height = Screen.height;
		width = 450;
		top = 20;
		left = (Screen.width / 2) - (width/2);
		
		menuWindowRect = new Rect (left, top, width, height);
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
			GameState.Instance.setLevel("level1");
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Level 2"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.setLevel("level2");
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Level 3"))
		{
			Debug.Log ("Moving to level 2");
			GameState.Instance.setLevel("level3");
		}

		GUILayout.Space(20);

		if (GUILayout.Button ("Options"))
		{
			Debug.Log ("Moving to Options");
			GameState.Instance.setLevel(GameState.OPTIONS_MENU);
		}
	}
}
