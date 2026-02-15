using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MultiplayerButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private bool p_pointerIsHovering = false;
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
			b_buttonBackgroundColor.color = Color.Lerp(b_defaultButtonColor, Color.red, Mathf.PingPong(Time.time, 1));
		}
		else
		{
			b_buttonBackgroundColor.color = b_defaultButtonColor;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		p_pointerIsHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		p_pointerIsHovering = false;
	}
}
