using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public List<Scene> maps;

	public int randomSeed { get; private set; }

	private bool p_pointerIsHovering = false;
	private Image b_buttonBackgroundColor;
	private Color b_defaultButtonColor;

	private void Awake()
	{
		randomSeed = Mathf.RoundToInt(Random.Range(-2147483648, 2147483648));

		Random.InitState(randomSeed);
	}

	private void Start()
	{
		for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
		{
			//Debug.Log(SceneManager.GetSceneByBuildIndex(i).name);
			maps.Add(SceneManager.GetSceneByBuildIndex(i));
		}
		//Debug.Log((SceneManager.GetActiveScene().buildIndex + 1) + ", " + maps.Count);

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

	public void OnStartPressed()
	{
		int randomMap = Random.Range(SceneManager.GetActiveScene().buildIndex+1, maps.Count+1);

		Debug.Log(randomMap);

		SceneManager.LoadScene(randomMap);
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
