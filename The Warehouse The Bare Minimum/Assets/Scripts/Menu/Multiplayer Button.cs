using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiplayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool p_pointerIsHovering = false;
	private Image b_buttonBackgroundColor;
	private Color b_defaultButtonColor;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		b_buttonBackgroundColor = gameObject.GetComponent<Image>();
		b_defaultButtonColor = b_buttonBackgroundColor.color;
	}

    // Update is called once per frame
    void Update()
    {
		if (p_pointerIsHovering)
		{
			//Makes the button flash between white and red using the built in PingPong algorithm
			b_buttonBackgroundColor.color = Color.Lerp(b_defaultButtonColor, Color.red, Mathf.PingPong(Time.time, 1));
		}
		else
		{
			b_buttonBackgroundColor.color = b_defaultButtonColor;
		}
	}

	//Uses the pointer enter/exit handlers to detect when the cursor is hovering over the gameobject this script is attached to
	public void OnPointerEnter(PointerEventData eventData)
	{
		p_pointerIsHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		p_pointerIsHovering = false;
	}
}
