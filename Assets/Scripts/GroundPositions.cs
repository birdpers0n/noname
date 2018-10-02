using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPositions : MonoBehaviour {

    private Vector3 leftBorder;
    private float startWidth;
    private Vector3 startPos;
    private Vector3[] jumpScaleStartPos = new Vector3[4];
    private Player player;
    public GameObject ground;
    public GameObject coin;
    private List<GameObject> coins = new List<GameObject>();
    private static float groundWidth;

    private int blockBreak = 0, blockCounter = 0;
    private int lastObjectIndex = 10;

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<Player>();
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        // pocetni ground uvijek isti nakon restarta (malo siri)
        startPos = transform.GetChild(0).position;
        // pocetni ground se ne gleda za score
        transform.GetChild(0).GetComponent<Ground>().SetScored(true);

        groundWidth = transform.GetChild(0).GetComponent<BoxCollider2D>().bounds.size.x;

        for (int i = 0; i < 10; i++) {
            Vector3 pos = transform.GetChild(i).transform.position;
            pos.x += groundWidth;
 
            GameObject platform = Instantiate(ground, pos, Quaternion.identity);
            platform.transform.parent = this.transform;
            platform.name = (i+1).ToString();
        }

    }

    private void Update() {
        if(player.transform.position.x > player.startPos.x) {
            GetComponent<Scroller>().canScroll = true;
            if(player.isGrounded) player.GetComponent<Scroller>().canScroll = true;
        } else {
            GetComponent<Scroller>().canScroll = false;
            player.GetComponent<Scroller>().canScroll = false;

        }
    }

    public void ResetGround(Ground ground) {
        blockCounter++;
        if (blockCounter == 1) {
            ground.transform.position = new Vector3(transform.GetChild(lastObjectIndex).position.x + transform.GetChild(lastObjectIndex).GetComponent<BoxCollider2D>().bounds.size.x / 2f + Random.Range(2, 3.7f), ground.transform.position.y, ground.transform.position.z);
        } else {
            ground.transform.position = new Vector3(transform.GetChild(lastObjectIndex).position.x + transform.GetChild(lastObjectIndex).GetComponent<BoxCollider2D>().bounds.size.x, ground.transform.position.y, ground.transform.position.z);
        }

        if (blockBreak == 0 || blockCounter == blockBreak) {
            blockBreak = GetBlockBreak();
            blockCounter = 0;
        }

        lastObjectIndex = int.Parse(ground.name);
    }

    public int GetBlockBreak() {
        if (player.GetScore() <= 5) {
            return Random.Range(3, 8);
        } else if (player.GetScore() <= 15) {
            return Random.Range(2, 7);
        } else if (player.GetScore() <= 40) {
            return Random.Range(2, 5);
        } else if (player.GetScore() <= 75) {
            return Random.Range(1, 3);
        } else if (player.GetScore() <= 100) {
            return Random.Range(1, 2);
        }
        return 0;
    }

    public void Restart() {

        for (int i = 0; i <= 10; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }

        blockBreak = 0;
        blockCounter = 0;
        lastObjectIndex = 10;

        Vector3[] poss = new Vector3[11];

        poss[0] = startPos;
        GameObject platform0 = Instantiate(ground, poss[0], Quaternion.identity);
        platform0.transform.parent = this.transform;
        platform0.name = "0";


        for(int i = 0; i < 10; i++) {
            poss[i+1] = poss[i];
            poss[i+1].x += groundWidth;
            GameObject platform = Instantiate(ground,poss[i+1],Quaternion.identity);
            platform.transform.parent = this.transform;
            platform.name = (i + 1).ToString();
        }
    }
}
