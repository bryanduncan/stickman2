using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guyScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private float multiplier = .01f;
    public Vector3 velocity = Vector3.zero;
    public float gravity = 20.0f;
    public float speed = 5f;
    public float acceleration = .1f;
    private float startSpeed;
    public float cap = 12f;
    private Vector3 startPos;
    private bool facingRight = true;
    public GameObject waterbullet;
    float bulletCooldown;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startSpeed = speed;
        startPos = transform.position;
        facingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("spawnIn"))
            return;

        //CharacterController controller = GetComponent<CharacterController>();
        GetComponent<SpriteRenderer>().flipX = !facingRight;

        GameObject.Find("Main Camera").GetComponent<CameraScript>().UpdateX();
        GameObject.Find("Main Camera").GetComponent<CameraScript>().UpdateY();

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("Shoot");
            anim.SetBool("ShotOver", false);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetTrigger("Shoot");
            anim.SetBool("ShotOver", true);
        }

        bulletCooldown += Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fired") && bulletCooldown >= .5f)
        {
            if (facingRight)
            {
                Vector3 rightSide = new Vector3(transform.position.x + 1, transform.position.y + .5f, 0);
                GameObject obj = Instantiate(waterbullet, rightSide, transform.rotation);
            }
            else
            {
                Vector3 leftSide = new Vector3(transform.position.x - 1, transform.position.y + .5f, 0);
                GameObject obj = Instantiate(waterbullet, leftSide, transform.rotation);
                obj.GetComponent<SpriteRenderer>().flipX = !facingRight;
                obj.GetComponent<waterbulletScript>().speed = -obj.GetComponent<waterbulletScript>().speed;
            }
            bulletCooldown = 0;
        }

        velocity = new Vector3(Input.GetAxis("Horizontal") * speed, velocity.y, 0);

        if (velocity.y == 0)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 0, 0);

            if (Input.GetAxis("Horizontal") != 0 && speed <= cap)
            { speed += acceleration; }

            else
            { speed = startSpeed; }

            if (speed > cap)
            { speed = cap; }
        }
        else
        {
            transform.parent = null;
        }

        if (Input.GetButtonDown("Jump"))
        {
            velocity = new Vector3(Input.GetAxis("Horizontal") * speed, 15, 0);
        }

        //velocity.y -= gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        //transform.Translate(velocity.x * multiplier, 0,0);
        rb.velocity = new Vector2(
            velocity.x,
            velocity.y
        );

        anim.SetFloat("Velocity X", velocity.x);
        anim.SetFloat("Velocity Y", velocity.y);
        //anim.SetBool("isGrounded", controller.isGrounded);

        if (velocity.x > 0)
            facingRight = true;
        else if (velocity.x < 0)
            facingRight = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
    }
}
