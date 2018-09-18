using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour {

    public static float scrollSpeed = 1.2f;
    public bool canScroll;

    void FixedUpdate () {
        if (canScroll) transform.position -= Vector3.right * scrollSpeed * Time.deltaTime;
    }

}
