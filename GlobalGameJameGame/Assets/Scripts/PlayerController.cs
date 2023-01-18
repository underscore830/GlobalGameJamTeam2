using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //fields
    public InputAction playerControls;
    private Vector2 direction = Vector2.zero;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpHeight;
    public Rigidbody2D rigidbody;
    private bool canJump = true;

    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        //updates player's horizontal velocity
        rigidbody.velocity = new Vector2((direction.x * speed * Time.deltaTime), rigidbody.velocity.y);
        Debug.Log(rigidbody.velocity.y);
    }
    // Update is called once per frame
    void Update()
    {
        
        direction = playerControls.ReadValue<Vector2>();

        
    }

    private void OnJump(InputValue value)
    {

        if (canJump)
        {
            
            rigidbody.AddForce(new Vector2(0, jumpHeight));
            canJump = false;
        }
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
}
