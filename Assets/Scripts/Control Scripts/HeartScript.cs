using UnityEngine;
using UnityEngine.InputSystem;

public class HeartScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed, tempSpeed;
    private Vector2 direction;
    public InputActionReference movement;
    public bool slip = false, stick = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tempSpeed = speed;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = movement.action.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if(slip && !stick)
        {
            tempSpeed = speed*5;
        }
        else if(stick && !slip)
        {
            tempSpeed = speed/5;
        }
        else
        {
            tempSpeed = speed;
        }
        
        rb.velocity = new Vector2(direction.x * tempSpeed, direction.y * tempSpeed);
    }
}
