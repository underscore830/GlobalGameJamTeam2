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
    public new Rigidbody2D rigidbody;
    private bool canJump = true;


    //Animation move
    private bool isRunning = false;
    [SerializeField]
    private Animator animator;
    private float playerPosPre, horizontal;

    public bool CanJump
    {
        get { return canJump; }
        set { canJump = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Animation move
        playerPosPre = 0f;

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


        //Animation move
        horizontal = direction.x;
        isRunning = horizontal != 0f;
        if (isRunning)
        {
            Vector3 scale = gameObject.transform.localScale;
            if (horizontal < 0 && scale.x > 0 || horizontal > 0 && scale.x < 0)
            {
                scale.x *= -1;
            }
            gameObject.transform.localScale = scale;
        }
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumpping", !canJump);
        playerPosPre = transform.position.x;
    }

    private void OnJump(InputValue value)
    {

        if (canJump)
        {
            
            rigidbody.AddForce(new Vector2(0, jumpHeight));
            //canJump = false;
        }

    }
    //Jump adjustment
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
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
