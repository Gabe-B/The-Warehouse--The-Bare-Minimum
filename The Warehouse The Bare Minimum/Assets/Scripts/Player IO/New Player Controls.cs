 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

public class NewPlayerControls : MonoBehaviour
{
	//All of the action references for the player IO
	public PlayerInput pi;
	public InputActionAsset InputActions;
	private InputAction m_moveAction;
	private InputAction m_lookAction;
	private InputAction m_jumpAction;
	private InputAction m_sprintAction;
	private InputAction m_interactAction;
	private InputAction m_pauseAction;

	//Used to move the player
	private Vector2 m_moveAmt;
	private Vector2 m_lookAmt;
	private Rigidbody m_rigidbody;

	//Used for camera controls
	[Header("Movement Variables")]
	public GameObject playerModel;
	public GameObject cameraPivot;
	public float upperCamRotationClamp = 30;
	public float lowerCamRotatioClamp = -30;

	//Rates at which to move the players IO
	public float WalkSpeed = 5;
	public float SprintSpeed = 10;
	public float RotateSpeed = 5;
	public float JumpSpeed = 5;

	private float rotationAmtX = 0;
	private float rotationAmtY = 0;
	public float m_currentMoveSpeed;
	private Vector3 m_tempFaceDir;
	private bool isSprinting = false;

	//Ground Check
	public float groundCheckDistance;
	public Vector3 groundCheckBoxSize;
	private bool m_grounded = true;

	//Pause menu
	public PauseMenu pm;

	public float gravity;

	public bool canMove = true;
	public bool canLook = true;

	private void OnEnable()
	{
		//Enables/sets the controls to the 'Player' control scheme
		InputActions.FindActionMap("Player").Enable();

		//Adds the script for spawning the player into the scene to the 'Spawned' event
		PlayerSpawn.Spawned += SpawnPlayer;

		//Adds the script for handling the player on round start to the 'StartGame' event
		PlayerSpawn.StartGame += StartPlayer;
	}

	private void OnDisable()
	{
		//See OnEnable for details. Removes the necessary bindings/scripts in case of player object deletion
		InputActions.FindActionMap("Player").Disable();
		PlayerSpawn.Spawned -= SpawnPlayer;
		PlayerSpawn.StartGame -= StartPlayer;
	}

	private void Awake()
	{
		//Sets the requisite actions from the control scheme so they can be referenced later
		m_moveAction = InputSystem.actions.FindAction("Move");
		m_lookAction = InputSystem.actions.FindAction("Look");
		m_jumpAction = InputSystem.actions.FindAction("Jump");
		m_sprintAction = InputSystem.actions.FindAction("Sprint");
		m_interactAction = InputSystem.actions.FindAction("Interact");
		m_pauseAction = InputSystem.actions.FindAction("Pause");

		//Gets the rigidbody component from the gameobject
		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		//Locks the cursor to the middle of the screen and makes in invisible
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		//Sets the starting move speed to the default walk speed
		m_currentMoveSpeed = WalkSpeed;

		//Automatically sets the size of the boxcast to be the colliders size
		groundCheckBoxSize = gameObject.GetComponent<BoxCollider>().size;
		groundCheckBoxSize.y = 1;

		pm = FindObjectOfType<PauseMenu>();
	}

	private void Update()
	{
		if(pm ==null)
		{
			pm = FindObjectOfType<PauseMenu>();
		}

		//Checks for grounded state of player
		m_grounded = Grounded();

		//Handles the camera rotation
		Rotating();

		//Handles the direction the players model is facing
		AdjustDirectionFacing();

		//Changes gravity based on grounded state, allowing for movement while in the air
		if (m_grounded)
		{
			m_rigidbody.AddForce(Vector3.zero);
		}
		else
		{
			m_rigidbody.AddForce(0, gravity, 0);
		}

		//Checks if the player is pressing the sprint key and adjusts a bool depending on if they are or not
		if (pi.actions.FindAction("Sprint").WasPressedThisFrame())
		{
			isSprinting = true;
		}
		else if (pi.actions.FindAction("Sprint").WasReleasedThisFrame())
		{
			isSprinting = false;
		}

		if(pi.actions.FindAction("Pause").WasPressedThisFrame() && !pm.pausePanel.activeSelf)
		{
			pm.pausePanel.SetActive(true);
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Time.timeScale = 0;
		}
		else if (pi.actions.FindAction("Pause").WasPressedThisFrame() && pm.pausePanel.activeSelf)
		{
			pm.pausePanel.SetActive(false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;
		}
	}

	private void FixedUpdate()
	{
		//Handles the actual movement of the player through space
		Moving();
	}

	#region PlayerIO
	public void OnMove(InputValue value)
	{
		//Updates the variable based on the input from the player to be used in the 'Moving' function
		m_moveAmt = value.Get<Vector2>();
	}

	public void OnLook(InputValue value)
	{
		//Updates the variable based on player input to be used in the 'Rotating' function
		m_lookAmt = value.Get<Vector2>();
	}

	public void OnJump(InputValue value)
	{
		//Checks in the 'Jump' action is pressed ('value' is automatically handled via the player input component) and makes the player jump
		if (value.isPressed && m_grounded)
		{
			m_rigidbody.AddForceAtPosition(new Vector3(0, JumpSpeed, 0), Vector3.up, ForceMode.Impulse);
		}
	}

	private void Moving()
	{
		if (canMove)
		{
			//Adjusts the players rotation to face the direction the player is moving based on player movement input (via WASD or joystick) and where the camera is facing
			Vector3 moveDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
			moveDir.y = 0;

			//Adjusts move speed based on sprinting state
			if (isSprinting)
			{
				Sprinting();
			}
			else
			{
				Walking();
			}

			//Moves the player based on movement variables
			m_rigidbody.AddForce(moveDir.normalized * m_currentMoveSpeed, ForceMode.Force);
		}
	}

	private void Walking()
	{
		//Sets the move speed to the defined walk speed
		m_currentMoveSpeed = WalkSpeed;
	}

	private void Sprinting()
	{
		//Sets the move speed to the defined sprint speed
		m_currentMoveSpeed = SprintSpeed;
	}

	private void Rotating()
	{
		if (canLook)
		{
			//Gets the rotation on the X-axis based on the X-value of the players look value. Look value is derived from the players input (mouse movement or joystick input)
			rotationAmtX += m_lookAmt.x * RotateSpeed * Time.deltaTime;

			//Gets the rotation on the Y-axis based on the Y-value of the players look value. Look value is derived from the players input (mouse movement or joystick input)
			rotationAmtY -= m_lookAmt.y * RotateSpeed * Time.deltaTime;

			//Keeps the Y-rotation within a defined range to keep the camera from being able rotate completely around the player vertically 
			rotationAmtY = Mathf.Clamp(rotationAmtY, lowerCamRotatioClamp, upperCamRotationClamp);

			//Sets the rotation of the pivot point of the camera to the defined rotation. Camera object is a child of this pivot point, so it rotates accordingly
			cameraPivot.transform.rotation = Quaternion.Euler(rotationAmtY, rotationAmtX, 0);
		}
	}

	private void AdjustDirectionFacing()
	{
		if(canLook)
		{
			//Adjusts the players model rotation to face the direction the player is moving based on player movement input (via WASD or joystick) and where the camera is facing
			Vector3 faceDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
			faceDir.y = 0;

			//Checks if the player is providing an input
			if (faceDir != Vector3.zero)
			{
				//Sets the player models rotation
				playerModel.transform.rotation = Quaternion.LookRotation(faceDir);

				//Sets a temp variable to keep track of for future use
				m_tempFaceDir = faceDir;
			}
			//Checks if the player isn't providing new inputs and the temp variable has been set previously
			else if (faceDir == Vector3.zero && m_tempFaceDir != null)
			{
				//Sets the player models rotation based on the last known rotation to prevent the rotation from snapping back to the 'zero' position
				playerModel.transform.rotation = Quaternion.LookRotation(m_tempFaceDir);
			}
			else
			{
				//Sets the player models rotation
				playerModel.transform.rotation = Quaternion.LookRotation(faceDir);
			}
		}
	}

	private bool Grounded()
	{
		//Checks if the player is standing on an object
		return Physics.BoxCast(transform.position, groundCheckBoxSize, Vector3.down, transform.rotation, groundCheckDistance);
	}
	#endregion

	public void SpawnPlayer()
	{
		//Disables movement controls and makes the player invisible while spawning in
		m_rigidbody.useGravity = false;
		m_rigidbody.constraints = RigidbodyConstraints.FreezePosition;
		m_rigidbody.isKinematic = false;
		canMove = false;
		gameObject.GetComponent<BoxCollider>().enabled = false;

		foreach (SkinnedMeshRenderer m in playerModel.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			m.enabled = false;
		}
	}

	public void StartPlayer()
	{
		//Undoes the changes from the 'SpawnPlayer' script
		m_rigidbody.useGravity = true;
		m_rigidbody.constraints = RigidbodyConstraints.None;
		m_rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		m_rigidbody.isKinematic = false;
		canMove = true;
		gameObject.GetComponent<BoxCollider>().enabled = true;

		foreach (SkinnedMeshRenderer m in playerModel.GetComponentsInChildren<SkinnedMeshRenderer>())
		{
			m.enabled = true;
		}

		m_rigidbody.AddForce(new Vector3 (0, 350f, 0), ForceMode.Impulse);
	}
}
