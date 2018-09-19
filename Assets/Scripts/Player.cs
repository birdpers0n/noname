using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class Player : MonoBehaviour {

    private Rigidbody2D myBody;
    public Text scoreText;
    public GameObject pan, restartButton;
    private GroundPositions groundPos;
    private bool start,fall,gameOver;

    private float upPower = 8f, rightPower = 0.2f,factor = 20f;
    public bool isGrounded,isPressed,canJump;
    private int score,i;

    private Vector3 startPos;

    public GameObject camera; // set this via inspector

	// Use this for initialization
	void Start () {

        // perfect panel
        pan.SetActive(false);
        myBody = transform.GetComponent<Rigidbody2D>();
        groundPos = FindObjectOfType<GroundPositions>();
        startPos = transform.position;
        GetComponent<Scroller>().canScroll = false;
    }

    void Update() {
        isPressed = (Input.GetMouseButton(0)) ? true : false;
    }

    private void FixedUpdate() {

        if (gameOver) return;

        if (isGrounded && !fall) {
            // pritisak
            if (isPressed && upPower < 18f) {
                // jump power
                upPower += 15f * Time.fixedDeltaTime;
                rightPower += 15f * Time.fixedDeltaTime;
                canJump = true;
            }

            //odskok
            if (!isPressed && canJump) {
                canJump = false;
                isGrounded = false;
                GetComponent<Scroller>().canScroll = false;
                //skok
                myBody.AddForce(Vector3.up * (upPower * myBody.mass * factor));
                myBody.AddForce(Vector3.right * (rightPower * myBody.mass * factor));
                // vracanje u normalu
                upPower = 8f;
                rightPower = 0.2f;
            }
        } else {
            // vracanje indikatora na nulu
        }

        // brze padanje nakon dosega vrhunca u zraku
        if (myBody.velocity.y < 0) {
            myBody.AddForce(Vector3.down * ((upPower / 10f) * myBody.mass * factor));
        }
    }

       
        public void OnCollisionEnter2D(Collision2D collision) {
        // kontakt sa groundom
        if (collision.collider.tag == "Ground") {
            start = true;
            isGrounded = true;
            GetComponent<Scroller>().canScroll = true;
            if (!collision.collider.GetComponent<Ground>().GetScored()) {
                collision.collider.GetComponent<Ground>().SetScored(true);
                if(!fall) score++;
                scoreText.text = score.ToString();
            }
        }
    }

    // perfect panel disappear
    IEnumerator PanelTween() {
        yield return new WaitForSeconds(1f);
        pan.SetActive(false);
    }

    // kada player dotakne ground sa strane (da se ne povecava score)  i kada player propadne
    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Fall Border") {
            fall = true;
        } else {
            GameOver();
            restartButton.SetActive(true);
        }
    }

    private void GameOver() {
        gameOver = true;
        Scroller.scrollSpeed = 0;
        score = 0;
        scoreText.text = score.ToString();
    }

    public void Restart() {
        restartButton.SetActive(false);
        gameOver = false;
        fall = false;
        Scroller.scrollSpeed = 1.2f;
        transform.position = startPos;
        i = 0;
        groundPos.Restart();
    }

    public int GetScore() {
        return score;
    }
}
