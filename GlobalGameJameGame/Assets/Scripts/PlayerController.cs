using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public InputAction playerControls;
    private Vector2 direction = Vector2.zero;
    [SerializeField]
    private float speed;
    public Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + (direction * speed * Time.deltaTime));
    }
    // Update is called once per frame
    void Update()
    {
        direction = playerControls.ReadValue<Vector2>();

        
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
