using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // =========== Movement ===========
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    private Vector2 moveInput;

    // =========== Shooting ===========
    [Header("Shooting")]
    // variables for shooting

    // =========== Health ===========
    [Header("Health")]
    // variables for health

    // =========== Animation ===========
    // variables for animation


    private Rigidbody2D rb;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        
    }
}
