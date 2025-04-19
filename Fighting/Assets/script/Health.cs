using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    int currentHealth;
    public string DieAnim = "Die";
    public string HitAnim = "Hit";
    public float StunTime;
    public Animator Animator;
    public PlayerState State;
    // Start is called before the first frame update

    private void Awake(){
        currentHealth = MaxHealth;
        GetComponent<Rigidbody2D>().gravityScale = 8;
        GetComponent<Collider2D>().enabled = true;
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        StartCoroutine("Hit");

        if(currentHealth <= 0){
            Debug.Log("died");
            currentHealth += MaxHealth;
        }
    }
    IEnumerator Hit(){
        Debug.Log(gameObject.name + currentHealth);
        Animator.Play(HitAnim);
        State.stunned = true;
        State.AnimationPlaying = "Hit";
        yield return new WaitForSeconds(StunTime);
        State.stunned = false;

    }
    void Die(){
        State.Dead = true;
        State.AnimationPlaying = "Die";
        Debug.Log(gameObject.name + " died");
        Animator.Play(DieAnim);
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
    }

}
