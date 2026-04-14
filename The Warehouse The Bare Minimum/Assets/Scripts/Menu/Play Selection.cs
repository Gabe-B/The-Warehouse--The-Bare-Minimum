using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlaySelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    public GameObject multiplayerButton, shopButton, startButton;

    public Vector3 multiplayerButtonFinalDestination, shopButtonFinalDestination;
    public float buttonTransitionTime;

    private float elapsedTime;
    private Vector3 multiButtonOriginalPosition, shopButtonOriginalPosition;
    private string b_deaultButtonText;
    private string b_singlePlayerText = "Single Player";

    private bool p_pointerIsHovering = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b_deaultButtonText = buttonText.text;
        multiButtonOriginalPosition = multiplayerButton.transform.localPosition;
        shopButtonOriginalPosition = shopButton.transform.localPosition;
        multiplayerButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Animates the Start and Shop buttons
        //Checks if the pointer is hovering over the start
        if(p_pointerIsHovering)
		{
            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / buttonTransitionTime;

            multiplayerButton.transform.localPosition = Vector3.Lerp(multiplayerButton.transform.localPosition, multiplayerButtonFinalDestination, percentLerpComplete);
            shopButton.transform.localPosition = Vector3.Lerp(shopButton.transform.localPosition, shopButtonFinalDestination, percentLerpComplete);
        }
		else
		{
            elapsedTime += Time.deltaTime;
            float percentLerpComplete = elapsedTime / buttonTransitionTime;

            multiplayerButton.transform.localPosition = Vector3.Lerp(multiplayerButton.transform.localPosition, multiButtonOriginalPosition, percentLerpComplete);

            if(shopButton.transform.localPosition != shopButtonOriginalPosition)
			{
                shopButton.transform.localPosition = Vector3.Lerp(shopButton.transform.localPosition, shopButtonOriginalPosition, percentLerpComplete);
            }

            if(multiplayerButton.transform.localPosition.y > 129f)
			{
                multiplayerButton.SetActive(false);
			}
        }
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        //_buttonBackgroundColor.color = Color.Lerp(_defaultButtonColor, Color.green, Mathf.PingPong(Time.time, 5
        multiplayerButton.SetActive(true);
        elapsedTime = 0;

        p_pointerIsHovering = true;
        buttonText.text = b_singlePlayerText;
        buttonText.fontSize = 66;
	}

    public void OnPointerExit(PointerEventData eventData)
	{
        //_buttonBackgroundColor.color = Color.Lerp(Color.green, _defaultButtonColor, Mathf.PingPong(Time.time, 5));
        elapsedTime = 0;
        p_pointerIsHovering = false;
        buttonText.text = b_deaultButtonText;
        buttonText.fontSize = 100;
    }
}
