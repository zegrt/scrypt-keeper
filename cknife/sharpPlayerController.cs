using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpPress; 
    private float horizontalInput;
    private Rigidbody rigidBodyComponent;
    private int superJump;
    [SerializeField] private Transform groundCheckTrasform;
    [SerializeField] private LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //check if space is pressed down.
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            jumpPress = true; 
        }

        //assigning horizontal input to variable.
        horizontalInput = Input.GetAxis("Horizontal");
    }
    
    //Fixed update will run once every physics update.
    private void FixedUpdate() 
    {
        //Horizontal Movement.
        rigidBodyComponent.velocity = new Vector3(horizontalInput * 2, rigidBodyComponent.velocity.y, 0);

        //jump limit through layer masks.
        if (Physics.OverlapSphere(groundCheckTrasform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        
        //jump physics action running if jumpPress is true.
        if (jumpPress)
        {
            float jumpPower = 3f;

            if (superJump > 0)
            {
                jumpPower *=2;
                superJump--;
            }

            Debug.Log("Jump");
            rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpPress = false;
        }

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(other.gameObject);
            superJump++;
        }
    }

    private void OnCollisionEnter(Collision collision) 
    {
        return;    
    }
}
