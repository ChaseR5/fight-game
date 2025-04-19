using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerState State;
    public BaseCombat baseCombat;
    private float horizontal;
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;

    private bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.2f;
    public float dashingCooldown = 1f;
    public bool Right;

    public string dashInput;
    public string jumpInput;
    public string downInput;

    public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    public Animator Animator;

    private void Awake(){
        if(Right == true){
            dashInput = "u";
            jumpInput = "i";
            downInput = "k";

            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
        else{
            dashInput = "q";
            jumpInput = "w";
            downInput = "s";
        }
    }
    private void Update(){
        if (isDashing)
        {
            return;
        }
        if(State.Attacking == false && State.stunned == false){
            if(Right == true){
                horizontal = Input.GetAxisRaw("Horizontal2");
            }
            else{
                horizontal = Input.GetAxisRaw("Horizontal");
            }
            
        }
        else{
            horizontal = 0;
        }
        if (Input.GetKeyDown(dashInput)){
            Debug.Log(dashInput);
        }
        if (Input.GetKeyDown(jumpInput)){
            Debug.Log(jumpInput);
        }
        if (Input.GetKeyDown(downInput)){
            Debug.Log(downInput);
        }

        if (Input.GetKeyDown(jumpInput) && IsGrounded() && State.Attacking != true && State.stunned != true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetKeyDown(jumpInput) && rb.velocity.y > 0f && State.Attacking != true && State.stunned != true)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        if (canDash && State.Attacking != true && State.stunned != true && State.Grounded == true){
            if(Input.GetKeyDown(downInput) && Input.GetKeyDown(dashInput)){
                Debug.Log("Dodge"); //DODGE
            }
            else if(Input.GetKeyDown(dashInput)){
                StartCoroutine(Dash()); //DASH
            }    
        }
        if(State.Idle == true){
            State.AnimationPlaying = "Idle";
        }
        //Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private IEnumerator Jump(){
        yield return new WaitForSeconds(0.2f);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        if(Right == true && horizontal != 0){
            rb.velocity = new Vector2(transform.localScale.x * dashingPower * horizontal, 0f);
        }
        else if (Right == false && horizontal != 0){
            rb.velocity = new Vector2(transform.localScale.x * dashingPower * horizontal * -1, 0f);
        }
        else{
            rb.velocity = new Vector2(transform.localScale.x * dashingPower * -1, 0f);
        }
        //float originalGravity = rb.gravityScale;
        //rb.gravityScale = 0f;
        if(tr != null){
            tr.emitting = true;
        }
        yield return new WaitForSeconds(dashingTime);
        if(tr != null){
            tr.emitting = false;
        }
        //rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}