using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Rigidbody2D rb;
    public float moveSpeed;
    private bool up = true;
    private float timer = 0;
    private float timer2 = 0;
    public float timerMax;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (timer >= timerMax){
            timer = 0;
            up = false;
        }
        if (timer2 >= timerMax){
            timer2 = 0;
            up = true;
        }

        if (up){
            timer++;
            rb.velocity = Vector2.up * moveSpeed;
            GetComponent<Animator>().SetBool("goingUp", true);
        }else{
            timer2++;
            rb.velocity = Vector2.down * moveSpeed;
            GetComponent<Animator>().SetBool("goingUp", false);
        }
	}
}
