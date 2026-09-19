using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 120f;
    public float acceleration = 120f;   // how fast you reach top speed
    public float deceleration = 20f;   // how fast you slow down after releasing input

    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = movementInput * speed;

        // pick accel or decel rate depending on whether there's input
        float rate = movementInput.sqrMagnitude > 0.01f ? acceleration : deceleration;

        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, targetVelocity, rate * Time.fixedDeltaTime);
    }
}
