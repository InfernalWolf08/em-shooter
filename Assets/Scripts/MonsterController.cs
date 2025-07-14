using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterController : MonoBehaviour
{
    [Header("Stats")]
    public float health=100;
    [Range(0.1f, 5f)] public float speed;
    private float baseSpeed;
    public int damage;
    [Space]
    [Header("Animation")]
    public Animator animator;
    public bool isMoving;
    private Vector3 lastPos;
    [Space]
    [Header("AI")]
    [Header("Pathing")]
    public Transform player;
    public AIPath pathing;
    public AIDestinationSetter AiDest;
    public bool isChasing;
    [Header("Sight")]
    public Transform eyes;
    public Vector3 eyeSize = new Vector3(1, 1, 1);
    public LayerMask eyeMask;
    [Header("Hearing")]
    public List<AudioSource> audioSources = new List<AudioSource>();
    [Header("Death")]
    public AudioSource deathScream;
    public ScoreController scoreCont;

    void Start()
    {
        // Initialize
        player = GameObject.FindWithTag("Player").transform;
        scoreCont = player.GetComponent<ScoreController>();
        baseSpeed = speed;
        pathing.maxSpeed = speed;
        lastPos = transform.position;
        deathScream = GetComponent<AudioSource>();

        // Coroutines
        StartCoroutine(animate());
        StartCoroutine(death());
    }

    void Update()
    {
        // See the player
        RaycastHit2D hit = Physics2D.BoxCast(eyes.position, eyeSize, 0, eyes.right, eyeSize.z, eyeMask);
        if (hit)
        {
            if (hit.collider.transform.parent.gameObject.tag=="Player")
            {
                isChasing = true;
            }
        }

        // Collect data on nearby audioSources


        // Hear the player
        foreach (AudioSource audioSource in audioSources)
        {
            if (audioSource.isPlaying)
            {
                PlayerController playerCont = player.GetComponent<PlayerController>();
                if (audioSource.gameObject.name!="PlayerSprite")
                {
                    AiDest.target = audioSource.transform;
                } else if (playerCont.speed!=playerCont.baseSpeed && audioSource.gameObject.name=="PlayerSprite") {
                    isChasing = true;
                }
            }
        }

        // Flip towards target
        if (AiDest.target!=null)
        {
            print(AiDest.target.position.x<transform.GetChild(0).position.x);

            if (AiDest.target.position.x<transform.GetChild(0).position.x && AiDest.target.position.x<transform.parent.position.x)
            {
                transform.parent.rotation = Quaternion.Euler(0, 180, 0);
            } else if (AiDest.target.position.x>transform.GetChild(0).position.x && AiDest.target.position.x>transform.parent.position.x) {
                transform.parent.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        // Chase the player
        if (isChasing)
        {
            // Chase
            AiDest.target = player;
            speed = baseSpeed*2f;
        } else if (!isChasing && AiDest.target==player) {
            // Stop Chasing
            AiDest.target = null;
            speed = baseSpeed;
        }
        pathing.maxSpeed = speed;

        // Play death sound
        if (health<=0 && !deathScream.isPlaying)
        {
            speed = 0;
            scoreCont.score += 1;
            deathScream.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        // Collect data on nearby audioSources
        if (info.gameObject.GetComponent<AudioSource>()!=null)
        {
            audioSources.Add(info.gameObject.GetComponent<AudioSource>());
        } else if (info.transform.parent.gameObject.tag == "Player") { // Attack player
            print("Attacked");
            player.GetComponent<PlayerController>().takeDamage(damage);
        }
    }

    void OnTriggerExit2D(Collider2D info)
    {
        // Collect data on nearby audioSources
        if (info.gameObject.GetComponent<AudioSource>()!=null)
        {
            audioSources.Remove(info.gameObject.GetComponent<AudioSource>());
        }
    }

    // Coroutines
    IEnumerator animate()
    {
        while (true)
        {
            // Animate the sprite
            isMoving = lastPos!=transform.position;
            lastPos = transform.position;
            animator.SetBool("isMoving", isMoving);
            
            yield return new WaitForSeconds(0.1f);
            yield return null;
        }
    }

    IEnumerator death()
    {
        while (true)
        {
            // Die
            if (health<=0)
            {
                yield return new WaitForSeconds(2.8f);
                this.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}