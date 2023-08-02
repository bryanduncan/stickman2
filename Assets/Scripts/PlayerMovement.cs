using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Animator anim;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    bool shoot = false;
    public GameObject waterbullet;
    float bulletCooldown;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("spawnIn"))
            return;

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(horizontalMove != 0)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shoot = true;
            anim.SetTrigger("Shoot");
            anim.SetBool("ShotOver", false);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            shoot = false;
            anim.SetTrigger("Shoot");
            anim.SetBool("ShotOver", true);
        }

        // Set animations
        anim.SetFloat("Velocity X", horizontalMove);
        if(jump)
            anim.SetFloat("Velocity Y", 1f);
        else
            anim.SetFloat("Velocity Y", 0f);
        anim.SetBool("isGrounded", controller.m_Grounded);

        // shoot anim
        bulletCooldown += Time.deltaTime;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fired") && bulletCooldown >= .5f)
        {
            if (controller.m_FacingRight)
            {
                Vector3 rightSide = new Vector3(transform.position.x + 1, transform.position.y + .5f, 0);
                GameObject obj = Instantiate(waterbullet, rightSide, transform.rotation);
            }
            else
            {
                Vector3 leftSide = new Vector3(transform.position.x - 1, transform.position.y + .5f, 0);
                GameObject obj = Instantiate(waterbullet, leftSide, transform.rotation);
                obj.GetComponent<SpriteRenderer>().flipX = !controller.m_FacingRight;
                obj.GetComponent<waterbulletScript>().speed = -obj.GetComponent<waterbulletScript>().speed;
            }
            bulletCooldown = 0;
        }
    }

    private void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime,crouch,jump);
        jump = false;
    }
}
