using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    Vector3 forward;
    public float forwardSpeed;
    Rigidbody rb;
    public float moveVert;
    public float moveHori;
    public float jumpspeed;
    bool up, down, left, right = false;
    bool canJump = true;

	// Use this for initialization
	void Start () {
        
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
        
	}

    private void FixedUpdate()
    {
        forward = new Vector3(0, 0, forwardSpeed);
        jump();
        rb.AddForce(forward);
    }


    void jump()
    {
        moveHori = Input.GetAxisRaw("Horizontal");
        moveVert = Input.GetAxisRaw("Vertical");
        if(moveHori != 0)
        {
            moveVert = 0;
        }

        if(canJump && (moveHori != 0 || moveVert != 0))
        {
            Vector3 jump = new Vector3(moveHori, moveVert, 0);

            if(left)
            {
                jump = (jump + Vector3.right).normalized;
            }
            if(right)
            {
                jump = (jump + Vector3.left).normalized;
            }
            

            rb.velocity = jump * jumpspeed + forward;
            if(rb.velocity.x != 0 && rb.velocity.y != 0)
            {
                rb.velocity = rb.velocity / 2;
            }
            if(moveHori != 0 || moveVert != 0)
            {
                canJump = false;
            }
            
        }



        //rb.AddForce(jump * jumpspeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "platform")
        {
            canJump = true;
            rb.velocity = forward;

            if (collision.collider.transform.position.y == 5)
            {
                up = true;
                down = left = right = false;
            }
            if (collision.collider.transform.position.y == -5)
            {
                down = true;
                up = right = left = false;
            }
            if (collision.collider.transform.position.x == 5)
            {
                right = true;
                up = left = down = false;
            }
            if (collision.collider.transform.position.x == -5)
            {
                left = true;
                up = right = down = false;
            }
        }
    }
}
