using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    public GameObject mainCamera;
    public Transform camStartPosition, camSinglePlayerPosition;
    public float transitionTime;

    public List<GameObject> mainMenuButtonsToHide;
    public List<GameObject> singlePlayerButtonsToHide;

    private bool _singleHasBeenPressed = false;
    private bool _backHasBeenPressed = false;
    private float elapsedTime;

    [HideInInspector]
    public enum MENU_SCREEN
    {
        Main,
        Single,
        Multi
    };

    [SerializeField]
    public MENU_SCREEN currentScreen = MENU_SCREEN.Main;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScreen = MENU_SCREEN.Main;
    }

    // Update is called once per frame
    void Update()
    {
        if(_singleHasBeenPressed && currentScreen == MENU_SCREEN.Main)
		{
            gameObject.GetComponent<Image>().raycastTarget = true;

            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / transitionTime;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camSinglePlayerPosition.position, percentLerpComplete);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camSinglePlayerPosition.rotation, percentLerpComplete);

            if(mainMenuButtonsToHide[0].activeSelf)
			{
                foreach (GameObject g in mainMenuButtonsToHide)
                {
                    g.gameObject.SetActive(false);

					try
					{
                        g.GetComponent<StartButton>().p_pointerIsHovering = false;
					}
                    catch(Exception e)
					{
                        //Debug.Log(g.name + " doesn't have a Start Button Script Attached!");
					}

                    try
                    {
                        g.GetComponent<MultiplayerButton>().p_pointerIsHovering = false;
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(g.name + " doesn't have a Multiplayer Button Script Attached!");
                    }
                }
            }

            if(mainCamera.transform.localPosition.x >= (camSinglePlayerPosition.localPosition.x - 0.02f) && mainCamera.transform.localPosition.x <= camSinglePlayerPosition.localPosition.x)
			{
                mainCamera.transform.position = camSinglePlayerPosition.position;
                mainCamera.transform.rotation = camSinglePlayerPosition.rotation;
                currentScreen = MENU_SCREEN.Single;
                _singleHasBeenPressed = false;

                if (!singlePlayerButtonsToHide[0].activeSelf)
                {
                    foreach (GameObject g in singlePlayerButtonsToHide)
                    {
                        g.gameObject.SetActive(true);
                    }
                }
            }
        }
		else if (_backHasBeenPressed && currentScreen == MENU_SCREEN.Single)
		{
            gameObject.GetComponent<Image>().raycastTarget = false;

            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / transitionTime;

            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, camStartPosition.position, percentLerpComplete);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, camStartPosition.rotation, percentLerpComplete);

            if (singlePlayerButtonsToHide[0].activeSelf)
            {
                foreach (GameObject g in singlePlayerButtonsToHide)
                {
                    g.gameObject.SetActive(false);

                    try
                    {
                        g.GetComponent<StartButton>().p_pointerIsHovering = false;
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(g.name + " doesn't have a Start Button Script Attached!");
                    }

                    try
                    {
                        g.GetComponent<MultiplayerButton>().p_pointerIsHovering = false;
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(g.name + " doesn't have a Multiplayer Button Script Attached!");
                    }
                }
            }

            if (mainCamera.transform.localPosition.x >= camStartPosition.localPosition.x && mainCamera.transform.localPosition.x <= (camStartPosition.localPosition.x + 0.02f))
            {
                mainCamera.transform.position = camStartPosition.position;
                mainCamera.transform.rotation = camStartPosition.rotation;
                currentScreen = MENU_SCREEN.Main;
                _backHasBeenPressed = false;

                if (!mainMenuButtonsToHide[0].activeSelf)
                {
                    foreach (GameObject g in mainMenuButtonsToHide)
                    {
                        g.gameObject.SetActive(true);
                    }
                }
            }
        }
	}

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
