using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public InputActionReference look;

    //The sensitivity of the camera
    public float xSensitivity, ySensitivity;

    [HideInInspector]
    //The transform of the parent object the camera follows
    public Transform orientation;

    public Transform cameraXOrientation;

    //Handles current rotation of the player
    private float yRotation, xRotation;

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

        Vector2 lookDirection = look.action.ReadValue<Vector2>() * Time.deltaTime;

        yRotation += lookDirection.x * xSensitivity;
        xRotation -= lookDirection.y * ySensitivity;

        //Stops the player camera from just rotating over the player
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        //Rotates the camera
        cameraXOrientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
