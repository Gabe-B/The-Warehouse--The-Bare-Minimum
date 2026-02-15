using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class PlaySelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;

    private Image b_buttonBackgroundColor;
    private Color b_defaultButtonColor;
    private string b_deaultButtonText;
    private string b_singlePlayerText = "Single Player";

    private bool p_pointerIsHovering = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        b_buttonBackgroundColor = gameObject.GetComponent<Image>();
        b_defaultButtonColor = b_buttonBackgroundColor.color;
        b_deaultButtonText = buttonText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if(p_pointerIsHovering)
		{
            b_buttonBackgroundColor.color = Color.Lerp(b_defaultButtonColor, Color.green, Mathf.PingPong(Time.time, 1));
        }
		else
		{
            b_buttonBackgroundColor.color = b_defaultButtonColor;
        }
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
        //_buttonBackgroundColor.color = Color.Lerp(_defaultButtonColor, Color.green, Mathf.PingPong(Time.time, 5));
        p_pointerIsHovering = true;
        buttonText.text = b_singlePlayerText;
        buttonText.fontSize = 66;
	}

    public void OnPointerExit(PointerEventData eventData)
	{
        //_buttonBackgroundColor.color = Color.Lerp(Color.green, _defaultButtonColor, Mathf.PingPong(Time.time, 5));
        p_pointerIsHovering = false;
        buttonText.text = b_deaultButtonText;
        buttonText.fontSize = 100;
    }
}
