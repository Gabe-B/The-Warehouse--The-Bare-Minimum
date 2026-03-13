using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public InputActionAsset cameraControlsMap;
    private InputActionMap cc;
    private InputAction cameraControls;

    //The sensitivity of the camera
    public float xSensitivity, ySensitivity;

    [HideInInspector]
    //The transform of the parent object the camera follows
    public Transform orientation;

    public Transform cameraXOrientation;

    //Handles current rotation of the player
    private float yRotation, xRotation;

    private Vector2 lookDirection;

    private void Awake()
    {
        cameraControlsMap = GetComponent<PlayerInput>().actions;
        cc = cameraControlsMap.FindActionMap("Player");
    }

    private void OnEnable()
    {
        cc.FindAction("Look").started += OnLook;
        cameraControls = cc.FindAction("Look");
        cc.Enable();
    }

    private void OnDisable()
    {
        cc.FindAction("Look").started -= OnLook;
        cc.Disable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Hides and locks the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        orientation = gameObject.GetComponent<Transform>();
    }

	// Update is called once per frame
	void Update()
    {
        //Gets the mouse inputs from the player. Mouse X & Y are flipped from unity
        //float mouseY = look.action.ReadValue<float>() * Time.fixedDeltaTime * ySensitivity;
        //float mouseX = look.action.ReadValue<float>() * Time.fixedDeltaTime * xSensitivity;

        yRotation += lookDirection.x * xSensitivity;
        xRotation -= lookDirection.y * ySensitivity;

        //Stops the player camera from just rotating over the player
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        //Rotates the camera
        cameraXOrientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void OnLook(InputAction.CallbackContext ctx)
	{
        lookDirection = cameraControls.ReadValue<Vector2>() * Time.deltaTime;
    }
}
