using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour {

    public static EnemyManager instance;
    private GameObject playerTarget;

    [SerializeField]
    private GameObject enemyPrefab;

    void Awake() {
        if (instance == null)
            instance = this;
    }

    void Start() {
        playerTarget = GameObject.FindWithTag(Tags.PLAYER_TAG);
        SpawnEnemy();
    }

    public void SpawnEnemy() {
        GameObject.FindGameObjectWithTag("WinText").GetComponent<Text>().text = "New Match";
        GameObject.FindGameObjectWithTag("WinText").GetComponent<Animator>().Play("FadeOut");
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        playerTarget.GetComponent<PlayerMovement>().gameOver = false;
        playerTarget.GetComponentInChildren<Animator>().Play("Idle");
        //play win sound
        GameObject.FindGameObjectWithTag("BGSound").GetComponent<AudioSource>().Play();
        GameObject winAudioSource = GameObject.FindGameObjectWithTag("WinSound");

        AudioSource[] winSound = winAudioSource.GetComponents<AudioSource>();

        foreach (AudioSource sound in winSound)
        {
            sound.Stop();
        }

    }

} // class
