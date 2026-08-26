using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LobbyPlayerJoin : MonoBehaviour
{
	//The spawn points for the players
	public Transform[] spawnPoints;

	//Lobby prefab object for player
	public GameObject playerLobbyModel;

	private int i_spawnedPlayerCount = 0;

	public void OnPlayerJoined(PlayerInput playerInput)
	{
		if (i_spawnedPlayerCount <= spawnPoints.Length)
		{
			playerInput.transform.position = spawnPoints[i_spawnedPlayerCount].transform.position;
			i_spawnedPlayerCount++;
		}
	}
}
