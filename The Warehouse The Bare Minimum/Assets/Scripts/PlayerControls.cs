using UnityEngine;
public class PlayerControls : MonoBehaviour

// If Something is wrong feel free to change but tell me so I know how to do it correctly

{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

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

    // Getting input and calling the control functions
    void FixedUpdate()
    {
        if (Input.GetKey("w"))
        {
            forward();
        }

        if (Input.GetKey("a"))
        {
            left();
        }

        if (Input.GetKey("s"))
        {
            reverse();
        }

        if (Input.GetKey("d"))
        {
            right();
        }

    
    }
}
