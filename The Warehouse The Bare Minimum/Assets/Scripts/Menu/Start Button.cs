using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool p_pointerIsHovering = false;
	private Image b_buttonBackgroundColor;
	private Color b_defaultButtonColor;

	private void Start()
	{
		b_buttonBackgroundColor = gameObject.GetComponent<Image>();
		b_defaultButtonColor = b_buttonBackgroundColor.color;
	}

	private void Update()
	{
		if (p_pointerIsHovering)
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
		p_pointerIsHovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		p_pointerIsHovering = false;
	}
}
