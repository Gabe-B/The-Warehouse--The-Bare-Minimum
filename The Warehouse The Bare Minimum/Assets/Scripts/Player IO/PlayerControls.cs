using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour

// If Something is wrong feel free to change but tell me so I know how to do it correctly
{
    public InputActionAsset moveControlsMap;
    private InputActionMap mc;
    private InputAction moveControls;

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

    public float jumpDelay;

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

    private Vector2 _moveDirection;

    private Quaternion _lastRotation;

    private bool _jumpCRStarted = false;
    private bool _onShelf = false;

    private void Awake()
    {
        moveControlsMap = GetComponent<PlayerInput>().actions;
        mc = moveControlsMap.FindActionMap("Player");
    }

    private void OnEnable()
	{
        mc.FindAction("Move").started += OnMove;
        moveControls = mc.FindAction("Move");
        mc.Enable();
    }

	private void OnDisable()
	{
        mc.FindAction("Move").started -= OnMove;
        mc.Disable();
    }

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
        grounded = Physics.BoxCast(transform.position, playerModel.localScale/2, Vector3.down, Quaternion.Euler(orientation.rotation.eulerAngles), (transform.localScale.y * 0.5f) + groundCheckDistance);
        //Debug.Log(grounded);

        //Sets the gravity based on if player is grounded or not
        if(grounded)
		{
            Physics.gravity = new Vector3(0, -groundedGravity, 0);
        }
        else if (_onShelf)
		{
            Physics.gravity = new Vector3(0, -groundedGravity, 0);
        }
        else
		{
            Physics.gravity = new Vector3(0, -airGravity, 0);
        }
    }

	//private void OnDrawGizmos()
	//{
 //       Gizmos.DrawCube(transform.position, new Vector3(playerModel.localScale.x, playerModel.localScale.y + (transform.localScale.y * 0.5f) + groundCheckDistance, playerModel.localScale.z));
	//}

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

	private void OnCollisionStay(Collision collision)
	{
        if (collision.gameObject.tag == "Shelf")
        {
            _onShelf = true;
            Debug.Log("on shelf");
        }
    }

	private void OnCollisionExit(Collision collision)
	{
        if (collision.gameObject.tag == "Shelf")
        {
            _onShelf = false;
            Debug.Log("off shelf");
        }
    }

	//Gets the inputs from the player
	private void GetInput()
	{
        //_horizontalInput = Input.GetAxisRaw("Horizontal");
        //_verticalInput = Input.GetAxisRaw("Vertical");

        //If the spacebar is pressed, jump
        if(Input.GetButton("Jump") && grounded && !_jumpCRStarted)
		{
            StartCoroutine(Jump());
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
        //moveDirection = (pc.cameraXOrientation.forward * _verticalInput) + (pc.cameraXOrientation.right * _horizontalInput);
        //moveDirection.y = 0;
        //Debug.Log(moveDirection);
        //Vector2 dir = (pc.cameraXOrientation.forward * _moveDirection.x) + (pc.cameraXOrientation.right * _moveDirection.y);
        //Debug.Log(dir);

        Vector3 dir = (pc.cameraXOrientation.forward * _moveDirection.y) + (pc.cameraXOrientation.right * _moveDirection.x);
        //Debug.Log(dir);
        dir.y = 0;

        orientation.rotation = Quaternion.LookRotation(dir);

        rb.AddForce(dir.normalized * moveSpeed, ForceMode.Force);
    }

    public void OnMove(InputAction.CallbackContext ctx)
	{
        _moveDirection = moveControls.ReadValue<Vector2>();
        //Debug.Log(_moveDirection);

        Vector3 dir = (pc.cameraXOrientation.forward * _moveDirection.y) + (pc.cameraXOrientation.right * _moveDirection.x);
        //Debug.Log(dir);
        dir.y = 0;

        orientation.rotation = Quaternion.LookRotation(dir);

        rb.AddForce(dir.normalized * moveSpeed, ForceMode.Force);
    }

    //Makes the player jump
    private IEnumerator Jump()
	{
        _jumpCRStarted = true;
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(jumpDelay);
        _jumpCRStarted = false;
    }

    public void Sprint()
	{
        moveSpeed = sprintSpeed;
	}
}
