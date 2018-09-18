using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    private Vector3 leftBorder;
    private GroundPositions groundPos;

    private void Start() {
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        groundPos = FindObjectOfType<GroundPositions>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (transform.position.x + transform.GetComponent<BoxCollider2D>().bounds.size.x / 2 < leftBorder.x) {
            groundPos.DestroyCoins(this.gameObject);
        }

    }
}
