using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    // You can adjust this speed directly inside the Unity Inspector
    public float speed = 5f; 

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        // This automatically grabs the Rigidbody 2D component from your player
        rb = GetComponent<Rigidbody2D>();

        // IMPORTANT FIX: Stops the player from spinning out of control when hitting the wall
        rb.freezeRotation = true;

        // If you are making a top-down game, uncomment the line below to turn off gravity:
        // rb.gravityScale = 0f;
    }

    void Update()
    {
        // 1. Gather keyboard input every frame (W/A/S/D or Arrow Keys)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        // 2. Store the movement direction and normalize it so moving diagonally isn't faster
        movementInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        // 3. Move via velocity inside FixedUpdate. 
        // This lets the physics engine handle the movement so you hit the wall and stop!
        rb.linearVelocity = movementInput * speed;
    }
}