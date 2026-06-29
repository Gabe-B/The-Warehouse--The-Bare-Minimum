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

	public float gravity;

	[SerializeField]
	public bool canMove = true;

	private void OnEnable()
	{
		InputActions.FindActionMap("Player").Enable();
		PlayerSpawn.Spawned += SpawnPlayer;
		PlayerSpawn.StartGame += StartPlayer;
	}

	private void OnDisable()
	{
		InputActions.FindActionMap("Player").Disable();
		PlayerSpawn.Spawned -= SpawnPlayer;
		PlayerSpawn.StartGame -= StartPlayer;
	}

	private void Awake()
	{
		m_moveAction = InputSystem.actions.FindAction("Move");
		m_lookAction = InputSystem.actions.FindAction("Look");
		m_jumpAction = InputSystem.actions.FindAction("Jump");
		m_sprintAction = InputSystem.actions.FindAction("Sprint");
		m_interactAction = InputSystem.actions.FindAction("Interact");

		m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		m_currentMoveSpeed = WalkSpeed;
		groundCheckBoxSize = gameObject.GetComponent<BoxCollider>().size;
		groundCheckBoxSize.y = 1;
	}

	private void Update()
	{
		m_grounded = Grounded();

		Rotating();
		AdjustDirectionFacing();

		if (m_grounded)
		{
			m_rigidbody.AddForce(Vector3.zero);
		}
		else
		{
			m_rigidbody.AddForce(0, gravity, 0);
		}

		if (pi.actions.FindAction("Sprint").WasPressedThisFrame())
		{
			isSprinting = true;
		}
		else if (pi.actions.FindAction("Sprint").WasReleasedThisFrame())
		{
			isSprinting = false;
		}
	}

	private void FixedUpdate()
	{
		Moving();
	}

	#region PlayerIO
	public void OnMove(InputValue value)
	{
		m_moveAmt = value.Get<Vector2>();
	}

	public void OnLook(InputValue value)
	{
		m_lookAmt = value.Get<Vector2>();
	}

	public void OnJump(InputValue value)
	{
		if (value.isPressed && m_grounded)
		{
			m_rigidbody.AddForceAtPosition(new Vector3(0, JumpSpeed, 0), Vector3.up, ForceMode.Impulse);
		}
	}

	private void Moving()
	{
		if (canMove)
		{
			Vector3 moveDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
			moveDir.y = 0;

			if (isSprinting)
			{
				Sprinting();
			}
			else
			{
				Walking();
			}

			m_rigidbody.AddForce(moveDir.normalized * m_currentMoveSpeed, ForceMode.Force);
		}
	}

	private void Walking()
	{
		m_currentMoveSpeed = WalkSpeed;
	}

	private void Sprinting()
	{
		m_currentMoveSpeed = SprintSpeed;
	}

	private void Rotating()
	{
		rotationAmtX += m_lookAmt.x * RotateSpeed * Time.deltaTime;
		rotationAmtY -= m_lookAmt.y * RotateSpeed * Time.deltaTime;

		rotationAmtY = Mathf.Clamp(rotationAmtY, lowerCamRotatioClamp, upperCamRotationClamp);

		cameraPivot.transform.rotation = Quaternion.Euler(rotationAmtY, rotationAmtX, 0);
	}

	private void AdjustDirectionFacing()
	{
		Vector3 faceDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
		faceDir.y = 0;

		//Debug.Log(faceDir);

		if (faceDir != Vector3.zero)
		{
			playerModel.transform.rotation = Quaternion.LookRotation(faceDir);
			m_tempFaceDir = faceDir;
		}
		else if (faceDir == Vector3.zero && m_tempFaceDir != null)
		{
			playerModel.transform.rotation = Quaternion.LookRotation(m_tempFaceDir);
		}
		else
		{
			playerModel.transform.rotation = Quaternion.LookRotation(faceDir);
		}
	}

	private bool Grounded()
	{
		return Physics.BoxCast(transform.position, groundCheckBoxSize, Vector3.down, transform.rotation, groundCheckDistance);
	}
	#endregion

	public void SpawnPlayer()
	{
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
