//using UnityEngine;
//using System.Collections;
//
//public class InGameMenu : MonoBehaviour
//{
//
//	GameState state;
//
//	private float top;
//	private float left;
//	private float height;
//	private float width;
//	private Rect menuWindowRect;
//
//	public GUISkin skin;
//
//
//	void Start ()
//	{
//		state = GameState.Instance;
//	}
//
//	void Update ()
//	{
//		float height = Screen.height / 2;
//		float width = 300;
//		float top = (Screen.height / 2) - (height/2);
//		float left = (Screen.width / 2) - (width/2);
//		
//		menuWindowRect = new Rect (left, top, width, height);
//	}
//
//	void OnGUI ()
//	{
//		GUI.skin = skin;
//		menuWindowRect = GUI.Window (1, menuWindowRect, windowFunction, "Levels");
//	}
//
//	void windowFunction (int windowID)
//	{
////		if (GUILayout.Button ("Level 1"))
////		{
////			Debug.Log ("Moving to level 1");
////			GameState.Instance.setLevel("level1");
////			
////		}
//		
////		GUILayout.Space(5);
//		
//		if (GUILayout.Button ("Level 2"))
//		{
//			Debug.Log ("Moving to level 2");
//			GameState.Instance.setLevel("level2");
//		}
//		
////		GUILayout.Space(5);
////		
////		if (GUILayout.Button ("Level 3"))
////		{
////			Debug.Log ("Moving to level 3");
////			GameState.Instance.setLevel("level3");
////		}
//	}
//}
