using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombat : MonoBehaviour
{
    public PlayerState State;
    public Movement Movement;
    public SoundManager Sound;
    public Animator Animator;
    public LayerMask enemyLayers;
    public Transform AttackPoint;
    public float AttackRange;
    public int Damage;
    public string LightPunch1Anim = "LightPunch1";
    public AudioClip LightPunch13Sound;
    public AudioClip LightPunch2Sound;
    public AudioClip LightPunch4Sound;
    public AudioClip MissSound;
    public int hitnum;
    public float timeSinceLastHit;
    public string lightInput;
    public string heavyInput;
    public string blockInput;
    // Start is called before the first frame update

    private void Awake(){
        if(Movement.Right == true){
            lightInput = "o";
            heavyInput = "p";
            blockInput = ";";
        }
        else{
            lightInput = "e";
            heavyInput = "r";
            blockInput = "f";
        }
    }
    // Update is called once per frame
    void Update(){

        

       if(State.Attacking != true){
            if(Input.GetKeyDown(lightInput) && Input.GetKeyDown(heavyInput)){
                Debug.Log("Light+Heavy");
            }
            else if(Input.GetKeyDown(lightInput)){
                StartCoroutine("LightPunchATK");
            }
       }
       if(timeSinceLastHit <= Time.time){
            hitnum = 0;
       }
       if(hitnum > 3){
            hitnum = 0;
       }
    }

    IEnumerator LightPunchATK(){
        // if(State.AnimationPlaying == "LightPunch1" || State.AnimationPlaying == "LightPunch2" || State.AnimationPlaying =="LightPunch3" || State.AnimationPlaying =="LightPunch4"){
        //     hitnum += 1;
        // }

        //STARTUP
        State.Attacking = true;
        timeSinceLastHit = Time.time + 5;
        //Movement.rb.velocity = new Vector2(0f, 0f);
        if(hitnum == 0){
            Animator.Play("LightPunch1");
            State.AnimationPlaying = "LightPunch1";
        }
        else if(hitnum == 1){
            Animator.Play("LightPunch2");
            State.AnimationPlaying = "LightPunch2";
        }

        else if(hitnum == 2){
            Animator.Play("LightPunch3");
            State.AnimationPlaying = "LightPunch3";
        }
        else{
            Animator.Play("LightPunch4");
            State.AnimationPlaying = "LightPunch4";
        }
        hitnum += 1;
        //Animator.Play(LightPunch1Anim);
        Sound.PlaySound(MissSound);
        //yield return new WaitForSeconds(0.35f);
            if(State.AnimationPlaying == "LightPunch4"){
               yield return new WaitForSeconds(0.24f);
            }
            else if(State.AnimationPlaying == "LightPunch2"){
                yield return new WaitForSeconds(0.06f);
            }
            else{
                yield return new WaitForSeconds(0.12f);
            }
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies){
            enemy.GetComponent<Health>().StunTime = 1f;
            //Debug.Log("hit" + enemy.name);
            //Debug.Log(hitEnemies.Length);
            if(State.AnimationPlaying == "LightPunch4"){
                Sound.PlaySound(LightPunch4Sound);
            }
            else if(State.AnimationPlaying == "LightPunch2"){
                Sound.PlaySound(LightPunch2Sound);
            }
            else{
                Sound.PlaySound(LightPunch13Sound);
            }
            enemy.GetComponent<Health>().TakeDamage(Damage);
        }
        if(hitEnemies.Length <= 0){
            State.stunned = true;
            yield return new WaitForSeconds(0.5f); //ENDLAG
            State.Attacking = false;
        }
        else{
            if(State.AnimationPlaying == "LightPunch4"){
                yield return new WaitForSeconds(0.36f); //ENDLAG
            }
            else if(State.AnimationPlaying == "LightPunch2"){
                yield return new WaitForSeconds(0.12f); //ENDLAG
            }
            else{
                yield return new WaitForSeconds(0.06f); //ENDLAG
            }
        }
        State.stunned = false;
        State.Attacking = false;
    }


    void OnDrawGizmosSelected(){
        if(AttackPoint == null)
        return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
