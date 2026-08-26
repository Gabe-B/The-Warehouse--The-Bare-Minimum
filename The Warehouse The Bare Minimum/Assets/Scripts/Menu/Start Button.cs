using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
	public GameObject MainMenuPanel, LocalLobbyPanel;

	public void StartButtonPressed ()
	{
		MainMenuPanel.SetActive(false);
		LocalLobbyPanel.SetActive(true);
	}
}
