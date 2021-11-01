using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public float health = 30f;

    private CharacterAnimation animationScript;
    private EnemyMovement enemyMovement;

    private bool characterDied;

    public bool is_Player;

    private HealthUI health_UI;
    public AudioClip hurtClip;

    void Awake() {
        animationScript = GetComponentInChildren<CharacterAnimation>();

        //if(is_Player) {
            health_UI = GetComponent<HealthUI>();
        //}
    }

    public void ApplyDamage(float damage, bool knockDown) {

        if (characterDied)
            return;

        health -= damage;

        // display health UI
        if(is_Player) {
           health_UI.DisplayHealth(health);
            animationScript.Hurt();
            GameObject.FindGameObjectWithTag("BGSound").GetComponent<AudioSource>().PlayOneShot(hurtClip);
        }

        if (health <= 0f) {
            animationScript.Death();
            characterDied = true;

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().gameOver = true;
            GameObject.FindGameObjectWithTag("WinText").GetComponent<Text>().text = "Victory";
            GameObject.FindGameObjectWithTag("WinText").GetComponent<Animator>().Play("FadeOut");
           // GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>().SetTrigger("Win");

            //play win sound
            GameObject.FindGameObjectWithTag("BGSound").GetComponent<AudioSource>().Stop();
            GameObject winAudioSource = GameObject.FindGameObjectWithTag("WinSound");

            AudioSource[] winSound = winAudioSource.GetComponents<AudioSource>();

            foreach (AudioSource sound in winSound)
            {

                sound.Play();
            }

            // if is player deactivate enemy script
            if (is_Player) {
                GameObject.FindWithTag(Tags.ENEMY_TAG)
                    .GetComponent<EnemyMovement>().enabled = false;

               
            }

            return;
        }

        if(!is_Player) {
            health_UI.DisplayEnemyHealth(health);

            if (knockDown) {  

               // if(Random.Range(0, 2) > 0) {
                    animationScript.KnockDown();
                //}

            } else {

                //if (Random.Range(0, 3) > 1) {
                    animationScript.Hit();
                GameObject.FindGameObjectWithTag("BGSound").GetComponent<AudioSource>().PlayOneShot(hurtClip);
                // }

            }

        } // if is player



    } // apply damage



} // class




































