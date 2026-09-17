 using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    //sets the speed and makes it a variable within the unity editor
    public float speed = 5f; 

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        //finds the rigidbody within the player
        rb = GetComponent<Rigidbody2D>();

        //stops the player from spinning
        rb.freezeRotation = true;


    }

    void Update()
    {
        //gets userinput and converts it to a float
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        //stores movement direction and normalises to make sure diagonal movement isn't faster
        movementInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        //moves in velocity so you stop when hitting an object
        rb.linearVelocity = movementInput * speed;
    }
}