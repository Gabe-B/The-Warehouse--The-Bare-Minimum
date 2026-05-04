using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerControls : MonoBehaviour
{
    //All of the action references for the player IO
    public InputActionAsset InputActions;
    private InputAction m_moveAction;
    private InputAction m_lookAction;
    private InputAction m_jumpAction;
    private InputAction m_sprintAction;

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

    private void OnEnable()
	{
        InputActions.FindActionMap("Player").Enable();
	}

	private void OnDisable()
	{
        InputActions.FindActionMap("Player").Enable();
    }

	private void Awake()
	{
        m_moveAction = InputSystem.actions.FindAction("Move");
        m_lookAction = InputSystem.actions.FindAction("Look");
        m_jumpAction = InputSystem.actions.FindAction("Jump");
        m_sprintAction = InputSystem.actions.FindAction("Sprint");

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

        if(m_grounded)
		{
            m_rigidbody.AddForce(Vector3.zero);
        }
        else
		{
            m_rigidbody.AddForce(0, gravity, 0);
        }

        if (m_sprintAction.WasReleasedThisFrame())
        {
            //Debug.Log("Sprint released!");
            isSprinting = false;
        }
    }

	private void FixedUpdate()
	{
        Moving();
    }

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

    public void OnSprint(InputValue value)
	{
        
    }

    private void Moving()
	{
        Vector3 moveDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
        moveDir.y = 0;

        if(isSprinting)
		{
            Sprinting();
		}
        else
		{
            Walking();
		}

        m_rigidbody.AddForce(moveDir.normalized * m_currentMoveSpeed, ForceMode.Force);
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

        if(faceDir != Vector3.zero)
		{
            playerModel.transform.rotation = Quaternion.LookRotation(faceDir);
            m_tempFaceDir = faceDir;
        }
        else if(faceDir == Vector3.zero && m_tempFaceDir != null)
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
}
