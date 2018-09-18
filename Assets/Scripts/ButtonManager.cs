using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public Player player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnClick(string s) {
        SceneManager.LoadScene(s);
    }

    public void Restart() {
        FindObjectOfType<Player>().Restart();
    }
}
