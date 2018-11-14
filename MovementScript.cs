using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    public GameObject dashEffect;

    private Rigidbody2D rb;
    public float moveSpeed;
    public float jumpHeight;
    public bool isGrounded;
    public float dashSpeed;
    public float startDashTime;
    public float dashTime;
    private bool dashActive;
    private bool dashRestored;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update () {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashRestored) {
            //transform.position += move * dashSpeed * Time.deltaTime;
            if (move.x > 0 && dashActive == false){
                GetComponent<Animator>().SetBool("isDashing", true);
                rb.velocity = Vector2.right * dashSpeed;
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                dashActive = true;
                dashRestored = false;
            }
            if(move.x < 0 && dashActive == false){
                GetComponent<Animator>().SetBool("isDashing", true);
                rb.velocity = Vector2.left * dashSpeed;
                Instantiate(dashEffect, transform.position, Quaternion.identity);
                dashActive = true;
                dashRestored = false;
            }
        } else {
            transform.position += move * moveSpeed * Time.deltaTime;
        }
        if(dashTime <= 0 && dashActive){
            dashActive = false;
            GetComponent<Animator>().SetBool("isDashing", false);
            rb.velocity = Vector2.zero;
            dashTime = startDashTime;
        }else if(dashActive && dashTime > 0){
            dashTime -= Time.deltaTime;
        }
        if(dashTime > 0 && isGrounded && dashActive == false){
            dashRestored = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded){
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
            GetComponent<Animator>().SetBool("isJumping", true);
            StartCoroutine(JumpAnim());
        }
        if(move.x > 0 && isGrounded){
            GetComponent<Animator>().SetBool("isWalking", true);
            GetComponent<Animator>().SetBool("facingRight", true);
        }
        if (move.x < 0 && isGrounded){
            GetComponent<Animator>().SetBool("isWalking", true);
            GetComponent<Animator>().SetBool("facingRight", false);
        }
        if(move.x == 0 || isGrounded == false && GetComponent<Animator>().GetBool("isJumping") == false){
            GetComponent<Animator>().SetBool("isWalking", false);
        }
    }

    IEnumerator JumpAnim(){
        yield return new WaitForSeconds(0.333f);
        GetComponent<Animator>().SetBool("isJumping", false);

    }

    void OnTriggerEnter2D(){
        isGrounded = true;
    }
    void OnTriggerStay2D(){
        isGrounded = true;
    }
    void OnTriggerExit2D(){
        isGrounded = false;
    }

}

