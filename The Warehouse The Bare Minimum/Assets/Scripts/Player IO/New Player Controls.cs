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

    //Used to move the player
    private Vector2 m_moveAmt;
    private Vector2 m_lookAmt;
    private Rigidbody m_rigidbody;

    //Used for camera controls
    public GameObject playerModel;
    public float upperCamRotationClamp = 30;
    public float lowerCamRotatioClamp = -30;
    private float rotationAmtX = 0;
    private float rotationAmtY = 0;

    //Rates at which to move the players IO
    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpSpeed = 5;

    public GameObject cameraPivot;

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

        m_rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	private void Update()
	{
        m_moveAmt = m_moveAction.ReadValue<Vector2>();
        m_lookAmt = m_lookAction.ReadValue<Vector2>();

        Rotating();
        AdjustDirectionFacing();

        if (m_jumpAction.WasPressedThisFrame())
		{
            Jump();
		}
    }

	private void FixedUpdate()
	{
        Walking();
    }

	private void Jump()
	{
        m_rigidbody.AddForceAtPosition(new Vector3(0, JumpSpeed, 0), Vector3.up, ForceMode.Impulse);
	}

    private void Walking()
	{
        Vector3 moveDir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
        moveDir.y = 0;

        m_rigidbody.AddForce(moveDir.normalized * WalkSpeed, ForceMode.Force);
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
        Vector3 dir = (cameraPivot.transform.forward * m_moveAmt.y) + (cameraPivot.transform.right * m_moveAmt.x);
        dir.y = 0;

        playerModel.transform.rotation = Quaternion.LookRotation(dir);
	}
}
