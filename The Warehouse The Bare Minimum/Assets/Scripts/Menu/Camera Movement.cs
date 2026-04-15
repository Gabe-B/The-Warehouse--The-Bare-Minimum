using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform camStartPosition, camSinglePlayerPosition;
    public float transitionTime;

    public GameObject mainMenuPanel;
    public GameObject singlePlayerLobbyPanel;

    private bool _singleHasBeenPressed = false;
    private bool _backHasBeenPressed = false;
    private float elapsedTime;

    //The script uses this to know where to set the camera
    [HideInInspector]
    public enum MENU_SCREEN
    {
        Main,
        Single,
        Multi
    };

    //Setting the default to the main menu
    [SerializeField]
    public MENU_SCREEN currentScreen = MENU_SCREEN.Main;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Insuring that the start screen in the main menu
        currentScreen = MENU_SCREEN.Main;
    }

    // Update is called once per frame
    void Update()
    {
        //If the single player button was pressed the the current camera position is on the main menu
        if(_singleHasBeenPressed && currentScreen == MENU_SCREEN.Main)
		{
            //Enables the ability for the game object to be interacted with
            //gameObject.GetComponent<Image>().raycastTarget = true;

            //The next few lines here handle the smooth transition from one camera position to the next
            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / transitionTime;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camSinglePlayerPosition.position, percentLerpComplete);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camSinglePlayerPosition.rotation, percentLerpComplete);

            //Hides the lsit of buttons on the main menu
            if(mainMenuPanel.activeSelf)
			{
                mainMenuPanel.SetActive(false);
            }

            //The lerp function isn't exact, so this checks for when the camera is close enough to the end position and sets it to the end point
            if(mainCamera.transform.localPosition.x >= (camSinglePlayerPosition.localPosition.x - 0.02f) && mainCamera.transform.localPosition.x <= camSinglePlayerPosition.localPosition.x)
			{
                mainCamera.transform.position = camSinglePlayerPosition.position;
                mainCamera.transform.rotation = camSinglePlayerPosition.rotation;

                //The current screen is set to the single player screen
                currentScreen = MENU_SCREEN.Single;
                _singleHasBeenPressed = false;

                //Enables all of the single player screen buttons
                if (!singlePlayerLobbyPanel.activeSelf)
                {
                    singlePlayerLobbyPanel.SetActive(true);
                }
            }
        }
        //If the back button was pressed on the single player screen
		else if (_backHasBeenPressed && currentScreen == MENU_SCREEN.Single)
		{
            //Disables the ability for the game object to be interacted with
            //gameObject.GetComponent<Image>().raycastTarget = false;

            //The next few lines here handle the smooth transition from one camera position to the next
            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / transitionTime;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camStartPosition.position, percentLerpComplete);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camStartPosition.rotation, percentLerpComplete);

            //Hides the lsit of buttons on the single player screen
            if (singlePlayerLobbyPanel.activeSelf)
            {
                singlePlayerLobbyPanel.SetActive(false);
            }

            //The lerp function isn't exact, so this checks for when the camera is close enough to the end position and sets it to the end point
            if (mainCamera.transform.localPosition.x >= camStartPosition.localPosition.x && mainCamera.transform.localPosition.x <= (camStartPosition.localPosition.x + 0.02f))
            {
                mainCamera.transform.position = camStartPosition.position;
                mainCamera.transform.rotation = camStartPosition.rotation;

                //The current screen is set to the single player screen
                currentScreen = MENU_SCREEN.Main;
                _backHasBeenPressed = false;

                //Enables all of the main menu buttons
                if (!mainMenuPanel.activeSelf)
                {
                    mainMenuPanel.SetActive(true);
                }
            }
        }
	}

    //Booleans in the functions below determine what to do in the Update function
    public void OnSinglePressed()
	{
        _singleHasBeenPressed = true;
        elapsedTime = 0;
	}

    public void OnBackPressed()
	{
        _backHasBeenPressed = true;
        elapsedTime = 0;
    }
}
