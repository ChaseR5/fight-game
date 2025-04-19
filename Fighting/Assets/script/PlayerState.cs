using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public Movement Movement;
    public BaseCombat baseCombat;
    public bool Grounded;
    public bool Dashing;
    public bool Crouching;
    public bool Idle;
    public bool Attacking;
    public bool stunned;
    public bool Dead;
    public bool Blocking;
    public string AnimationPlaying;
    public Animator Animator;


    void Update() {
        if(Movement.IsGrounded()){
            Grounded = true;
            if (Input.GetKey(Movement.downInput)){
                Crouching = true;
            }
            else{
                Crouching = false;
            }
        }
        else{
            Grounded = false;
            Crouching = false;
        }
        if(Grounded == true){

        }
        if(Movement.isDashing == true){
            Dashing = true;
        }
        else{
            Dashing = false;
        }
        if (!Input.GetKey(Movement.downInput) && !Input.GetKey(Movement.dashInput) && !Input.GetKey(Movement.jumpInput) && Movement.horizontal == 0 && !Input.GetKey(baseCombat.lightInput) && !Input.GetKey(baseCombat.heavyInput) && !Input.GetKey(baseCombat.blockInput)){
            if(Dashing != true && Grounded == true && Attacking == false && stunned == false && Dead == false){
                Idle = true;
                Animator.Play("Idle");
            }
            else{
                Idle = false;
            }
        }
        else{
            Idle = false;
        }

    }
}
