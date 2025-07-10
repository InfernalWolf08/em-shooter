using static System.Convert;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public Rigidbody2D rb2d;
    [Range(0.1f, 50f)] public float speed=11f;
    private float baseSpeed;
    [Range(0.1f, 2f)] public float sprintMultiplier=1.75f;
    public float maxStamina=3f;
    private float stamina;
    private bool canSprint=true;

    [Header("Animation")]
    public Animator animator;
    private bool isMoving;
    public SpriteRenderer sr;

    void Start()
    {
        // Initialize
        baseSpeed = speed;
        stamina = maxStamina;
    }

    void FixedUpdate()
    {
        // Move
        move();
    }

    // Custom functions
    void move()
    {
        // Sprint
        if (Input.GetKey("left shift") && canSprint)
        {
            // Increase speed
            speed = baseSpeed*sprintMultiplier;
            animator.speed = sprintMultiplier;

            // Lower stamina
            if (stamina>0)
            {
                stamina -= Time.deltaTime;
            }
        } else if (!Input.GetKey("left shift") || !canSprint) {
            // Reset speed
            speed = baseSpeed;
            animator.speed = 1;

            // Raise stamina
            if (stamina<maxStamina)
            {
                stamina += Time.deltaTime;
            }
        }

        // Set canSprint
        if (stamina<=0)
        {
            canSprint = false;
            speed = baseSpeed;
        } else if (stamina>=maxStamina) {
            canSprint = true;
        }

        // Get velocity
        Vector2 velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical")).normalized;
        velocity *= speed;

        // Move the player
        rb2d.linearVelocity = velocity;
        isMoving = velocity.x+velocity.y!=0;

        // Animate the player
        animator.SetBool("Walking", isMoving);

        if (!isMoving)
        {
            gunOffset(0);
        }
        
        if (velocity.x!=0)
        {
            transform.GetChild(1).rotation = Quaternion.Euler(0, 180*movingLeft(), 0);
            transform.GetChild(2).rotation = Quaternion.Euler(0, 180*movingLeft(), 0);
        }
    }

    int movingLeft()
    {
        if (Input.GetAxisRaw("Horizontal")==-1)
        {
            return 1;
        } else {
            return 0;
        }
    }

    void gunOffset(int frame)
    {
        // Initialize
        Transform gun = transform.GetChild(2);
        float offset = 1;

        // Offset
        if (frame==1)
        {
            gun.localPosition = new Vector3(-0.0144f*offset, 0, gun.localPosition.z);
        } else if (frame==2) {
            gun.localPosition = new Vector3(-0.0095f*offset, -0.0049f, gun.localPosition.z);
        } else if (frame==3) {
            gun.localPosition = new Vector3(-0.0046f*offset, -0.0049f, gun.localPosition.z);
        } else if (frame==4) {
            gun.localPosition = new Vector3(-0.0298f*offset, -0.0049f, gun.localPosition.z);
        } else if (frame==5) {
            gun.localPosition = new Vector3(-0.02f*offset, 0, gun.localPosition.z);
        } else if (frame==6) {
            gun.localPosition = new Vector3(-0.0147f*offset, 0, gun.localPosition.z);
        } else if (frame==7) {
            gun.localPosition = new Vector3(-0.0045f*offset, -0.0049f, gun.localPosition.z);
        } else if (frame==8) {
            gun.localPosition = new Vector3(-0.0293f*offset, -0.0049f, gun.localPosition.z);
        } else if (frame==9) {
            gun.localPosition = new Vector3(-0.0146f*offset, 0, gun.localPosition.z);
        } else if (frame==0) {
            gun.localPosition = new Vector3(-0.0244f, 0, gun.localPosition.z);
        }
    }
}