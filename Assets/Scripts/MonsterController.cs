using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Transform ears;
    public float earRad;
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
        Collider2D[] hearing = Physics2D.OverlapCircleAll(ears.position, earRad);
        audioSources.Clear();
        
        foreach (Collider2D m_collider in hearing)
        {
            if (m_collider.gameObject.GetComponent<AudioSource>()!=null && !audioSources.Contains(m_collider.gameObject.GetComponent<AudioSource>()))
            {
                audioSources.Add(m_collider.gameObject.GetComponent<AudioSource>());
            }
        }

        // Hear the player
        foreach (AudioSource audioSource in audioSources)
        {
            // Initialize
            bool canHear = true;

            /*// Remove all audiosources out of range
            if (!hearing.Contains(audioSource.gameObject.GetComponent<Collider2D>()))
            {
                audioSources.Remove(audioSource);
                canHear = false;
            }*/

            // Check if an audioSource in range is playing
            if (audioSource.isPlaying && canHear)
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
            speed = baseSpeed*1.5f;
        } else if (!isChasing && AiDest.target==player) {
            // Stop Chasing
            AiDest.target = null;
            speed = baseSpeed;
        }
        pathing.maxSpeed = speed;

        // Reset target on reached destination
        if (pathing.reachedDestination)
        {
            AiDest.target = null;
            isChasing = false;
        }

        // Play death sound
        if (health<=0 && !deathScream.isPlaying)
        {
            animator.enabled = false;
            speed = 0;
            scoreCont.score += 1;
            deathScream.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D info)
    {
        // Attack player
        if (LayerMask.LayerToName(info.gameObject.layer) == "Player") {
            player.GetComponent<PlayerController>().takeDamage(damage);
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
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.black;
                yield return new WaitForSeconds(2.8f);
                this.gameObject.SetActive(false);
            }

            yield return null;
        }
    }
}