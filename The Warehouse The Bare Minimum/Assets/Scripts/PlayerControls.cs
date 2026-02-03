using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour

// If Something is wrong feel free to change but tell me so I know how to do it correctly
{
    public PlayerCam pc;

    [HideInInspector]
    //This is the transform of the players parent object
    public Transform orientation;

    public Transform playerModel;

    [HideInInspector]
    //The rigidbody reference of the player
    public Rigidbody rb;

    [Header("Movement Variables")]
    //The speed you want the player to move
    public float moveSpeed;
    public float sprintSpeed;
    private float startMoveSpeed;

    //How hard the player jumps
    public float jumpForce;

    //The starting gravity
    public float groundedGravity;

    //The airborne gravity
    public float airGravity;

    public float groundCheckDistance;

    //The direction the player will move in. [HideInInspector] just hides it from the unity window
    [HideInInspector]
    public Vector3 moveDirection;

    //Used to check if the player is grounded
    private bool grounded;

    //These variables get the value of WASD as a flaot value is if it were reading a joystick
    private float _horizontalInput, _verticalInput;

    private Quaternion _lastRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Automatically gets the components
        rb = gameObject.GetComponent<Rigidbody>();
        orientation = gameObject.GetComponent<Transform>();

        //Sets the gravity
        rb.useGravity = true;
        Physics.gravity = new Vector3(0, -groundedGravity, 0);

        startMoveSpeed = moveSpeed;
    }

	private void Update()
	{
        GetInput();

        //Sends an invisible ray down from the players center. If it hits something then it returns true. If it hits nothing, then it returns false
        grounded = Physics.BoxCast(transform.position, playerModel.localScale/2 ,Vector3.down, Quaternion.Euler(orientation.rotation.eulerAngles), (transform.localScale.y * 0.5f) + groundCheckDistance);

        //Sets the gravity based on if player is grounded or not
        if(grounded)
		{
            Physics.gravity = new Vector3(0, -groundedGravity, 0);
        }
        else
		{
            Physics.gravity = new Vector3(0, -airGravity, 0);
        }
    }

	#region old move functions
	void forward()
    {
        Debug.Log("Forward!");
    }

    void left()
    {
        Debug.Log("Left!");
    }

    void reverse()
    {
        Debug.Log("Reverse!");
    }

    void right()
    {
        Debug.Log("Right!");
    }
	#endregion

	// Getting input and calling the control functions
	void FixedUpdate()
    {
        #region old movement
        //if (Input.GetKey("w"))
        //{
        //    forward();
        //}

        //if (Input.GetKey("a"))
        //{
        //    left();
        //}

        //if (Input.GetKey("s"))
        //{
        //    reverse();
        //}

        //if (Input.GetKey("d"))
        //{
        //    right();
        //}
        #endregion

        MovePlayer();
    }

    //Gets the inputs from the player
	private void GetInput()
	{
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        //If the spacebar is pressed, jump
        if(Input.GetButton("Jump") && grounded)
		{
            Jump();
		}

        if(Input.GetButton("Fire3"))
		{
            Sprint();
		}
        else
		{
            moveSpeed = startMoveSpeed;
		}
	}

    //Moves the player
    private void MovePlayer()
	{
        moveDirection = (pc.cameraXOrientation.forward * _verticalInput) + (pc.cameraXOrientation.right * _horizontalInput);
        moveDirection.y = 0;

        if (Quaternion.LookRotation(moveDirection) != Quaternion.identity)
		{
            orientation.rotation = Quaternion.LookRotation(moveDirection);
        }

        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    //Makes the player jump
    private void Jump()
	{
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    public void Sprint()
	{
        moveSpeed = sprintSpeed;
	}
}
