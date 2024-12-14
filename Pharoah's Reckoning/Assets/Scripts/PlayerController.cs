using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;

    public AudioClip jump1;
    public AudioClip jump2;
    public AudioClip gunshotSound;

    public KeyCode Spacebar;
    public KeyCode L;
    public KeyCode R;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool grounded;
    
    public KeyCode Return;
    public Transform FirePoint;
    public GameObject bullet;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death")) { return;} // Ignore inputs during death

         transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        if(Input.GetKeyDown(Spacebar) && grounded)
        {
            
            Jump();
        }

        if(Input.GetKey(L))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);

            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = true;
               
            }

            
        }

        if(Input.GetKey(R))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
       
            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            
            }

            

        }
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetBool("Grounded", grounded);

        if(Input.GetKeyDown(Return))
        {
            Shoot();
        }
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
        AudioManager.instance.RandomizeSfx(jump1,jump2);
    }

     void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    public void Shoot()
    {
        Instantiate(bullet,FirePoint.position,FirePoint.rotation);
        AudioManager.instance.PlaySingle(gunshotSound); // Play the gunshot sound every time the player shoots
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Slider"))
        {
            anim.SetTrigger("Slide");
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        // Stop the slide animation when leaving the slider object
        if (other.CompareTag("Slider"))
        {
            anim.ResetTrigger("Slide");
        }
    }

}
