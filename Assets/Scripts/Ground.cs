using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    private bool isScored;

	// Use this for initialization
	void Start () {
       // GetComponent<Scroller>().canScroll = true;
	}

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "LeftBorder") {
            GetComponentInParent<GroundPositions>().ResetGround(this);
        }
    }

    // Update is called once per frame
    void Update () {
	}

    public void SetScored(bool b) {
        isScored = b;
    }

    public bool GetScored() {
        return isScored;
    }
}
