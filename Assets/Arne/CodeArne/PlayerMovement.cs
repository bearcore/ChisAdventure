using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);

        if (collision.collider.tag == "Trunk")
        {
            Debug.Log("Wow you made it to a Trunk!");
            //this.enabled = false;
        }
        onGround = true;
    }

    public SpawnManager spawnManager;
    public Boolean onGround = false;
    public Rigidbody mybody;
    public float upwardForce = 1000f;
    public float forwardForce = 1000f;
    public float sidewaysForce = 100f;

    // Start is called before the first frame update
    // We marked this as "FixedUpdate" because we are using it to mess with physics

    void Start()
    {
        
    }
    void FixedUpdate()
    {

        if(Input.GetKey("space") && onGround)
        {
            mybody.AddForce(0, upwardForce * Time.deltaTime, 0);    // Add a force to the y axis
            onGround=false;
        }

        if (Input.GetKey("a"))
        {
            mybody.AddForce(-sidewaysForce, 0, 0);    // Add a force to the y axis
        }

        if (Input.GetKey("d"))
        {
            mybody.AddForce(sidewaysForce, 0, 0);    // Add a force to the y axis
        }

        if (Input.GetKey("s"))
        {
            mybody.AddForce(0, 0, -forwardForce * Time.deltaTime);    // Add a force to the y axis
        }

        if (Input.GetKey("w"))
        {
            mybody.AddForce(0, 0, forwardForce * Time.deltaTime);    // Add a force to the y axis
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered(); 
    }
}
