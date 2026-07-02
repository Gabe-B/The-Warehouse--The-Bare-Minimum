using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    //The camera game object
    private Camera cam;

    //Current player index
    private int index;

    //Current player count
    private int totalPlayers;

    private void Awake()
    {
        //Adds the function to the event so it gets called at the appropriate time
        PlayerInputManager.instance.onPlayerJoined += HandlePlayerJoined;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Gets current player index
        index = GetComponentInParent<PlayerInput>().playerIndex;
        //Gets all of the players
        totalPlayers = PlayerInput.all.Count;
        //Gets the camera component and sets the depth (or the layer) to the index
        cam = GetComponent<Camera>();
        cam.depth = index;

        SetupCamera();
    }

	// Update is called once per frame
	void Update()
    {

    }

    private void HandlePlayerJoined(PlayerInput obj)
    {
        totalPlayers = PlayerInput.all.Count;
        SetupCamera();
    }

    private void SetupCamera()
    {
        //Debug.Log(totalPlayers);

        //If theres 1 player, sets the camera to be full screenS
        if(totalPlayers == 1)
		{
            cam.rect = new Rect(0, 0, 1, 1);
		}
        //If there's two players
        else if (totalPlayers == 2)
        {
            //Checks if the index is 1, if it is, sets camera to be half width and on the left. If index is 2, does the same thing but on the right
            cam.rect = new Rect(index == 0 ? 0 : 0.5f, 0, 0.5f, 1);
        }
        //If there are 3 players
        else if (totalPlayers == 3)
        {
            //Checks if index is 1, 2, or 3. If 1 or 2, sets half height and width and goes on left or right respectively but on top half of screen. If 3, full width but on bottom half
            cam.rect = new Rect(
                index == 0 ? 0 : (index == 1 ? 0.5f : 0),
                index < 2 ? 0.5f : 0, 
                index < 2 ? 0.5f : 1, 
                0.5f);
        }
		else
		{
            //Separates into 4 quadrants
            cam.rect = new Rect((index % 2) * 0.5f, (index < 2) ? 0.5f : 0f, 0.5f, 0.5f);
		}
    }
}
