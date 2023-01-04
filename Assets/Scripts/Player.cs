using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontalMovement=0f;
    float playerSpeed=15f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody2D;
    Animator animator;
    bool inAir=false;
    bool IsDead=false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement=Input.GetAxisRaw("Horizontal")*playerSpeed*Time.deltaTime;
        print(horizontalMovement);
        transform.position +=new Vector3(horizontalMovement,0,0);

        animator.SetFloat("Speed",Mathf.Abs(horizontalMovement));

        if (horizontalMovement>0){
            spriteRenderer.flipX=false;
        }
        else if(horizontalMovement<0){
            spriteRenderer.flipX=true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {

            inAir = true;
            rigidBody2D.AddForce(Vector3.up * 450);
            animator.SetBool("isJump", true);

        }
        // jump and attack
        if (Input.GetKeyDown(KeyCode.Space) && !inAir && Input.GetKeyDown(KeyCode.Return))
        {
            inAir = true;
            rigidBody2D.AddForce(Vector3.up * 450);
            animator.SetBool("isJumpAttack", true);
        }
        // jump and throw
        if (Input.GetKeyDown(KeyCode.Space) && !inAir && Input.GetKeyDown(KeyCode.T))
        {
            inAir = true;
            rigidBody2D.AddForce(Vector3.up * 450);
            animator.SetBool("isJumpThrow", true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animator.SetBool("isSlide", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.SetBool("isSlide", false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("isGlide", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            animator.SetBool("isGlide", false);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            animator.SetBool("isAttack", true);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            animator.SetBool("isAttack", false);
        }


        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.SetBool("isThrow", true);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            animator.SetBool("isThrow", false);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.SetBool("isClimb", true);
            rigidBody2D.AddForce(Vector3.up * 100);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            animator.SetBool("isClimb", false);
        }

        if (IsDead==true){
            animator.enabled=false;
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "floor")
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isJumpAttack", false);
            animator.SetBool("isJumpThrow", false);
            animator.SetBool("isDead", false);
            inAir=false;
        }

        if (collision.gameObject.tag == "cactus")
        {
            animator.SetBool("isDead", true);
            IsDead=true;
        }
    }
}