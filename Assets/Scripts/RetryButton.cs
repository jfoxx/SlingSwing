using UnityEngine;
using System.Collections;

public class RetryButton : MonoBehaviour {

	GameState state;

	void Start () {
		state = GameState.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		state.setLevel(state.currentLevel);
	}
}
