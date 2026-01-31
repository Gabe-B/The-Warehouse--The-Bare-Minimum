using UnityEngine;

public class PlayerCam : MonoBehaviour
{
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
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySensitivity;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        //Stops the player camera from just rotating over the player
        xRotation = Mathf.Clamp(xRotation, -30f, 30f);

        //Rotates the camera
        cameraXOrientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
