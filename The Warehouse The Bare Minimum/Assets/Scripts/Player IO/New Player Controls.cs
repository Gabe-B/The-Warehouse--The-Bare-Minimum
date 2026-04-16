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

    //Rates at which to move the players IO
    public float WalkSpeed = 5;
    public float RotateSpeed = 5;
    public float JumpSpeed = 5;

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

        if(m_jumpAction.WasPressedThisFrame())
		{
            Jump();
		}
    }

	private void FixedUpdate()
	{
        Walking();
        Rotating();
    }

	private void Jump()
	{
        m_rigidbody.AddForceAtPosition(new Vector3(0, 5f, 0), Vector3.up, ForceMode.Impulse);
	}

    private void Walking()
	{
        //m_rigidbody.MovePosition((m_rigidbody.position + transform.forward) * m_moveAmt * WalkSpeed * Time.deltaTime);
        Debug.Log((m_rigidbody.position + transform.forward) * m_moveAmt * WalkSpeed * Time.deltaTime);
	}

    private void Rotating()
	{
        float rotationAmt = m_lookAmt.x * RotateSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmt, 0);

        //HERE YOU ARE ADDING THE ROTATION TO THE CAMERAS PIVOT POINT

        //m_rigidbody.MoveRotation(m_rigidbody.rotation * deltaRotation);
    }
}
