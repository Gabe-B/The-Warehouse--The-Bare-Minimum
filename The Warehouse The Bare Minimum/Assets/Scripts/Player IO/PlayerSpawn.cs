using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
	//   public bool isInGame = false;
	//public GameObject playerPrefab;
	//   public Transform[] spawnPoints;

	//   private int playerCount;

	public Transform[] spawnPoints;

	private PlayerInputManager playerInputManager;
	private int spawnedPlayerCount = 0;

	private void Awake()
	{
		playerInputManager = GetComponent<PlayerInputManager>();
	}

	private void Start()
	{
		//int i = 0;
		//foreach (InputDevice id in InputSystem.devices)
		//{
		//	//Debug.Log(id.displayName);
		//	//Debug.Log(i);

		//	if (i < InputSystem.devices.Count)
		//	{
		//		if (id.displayName != "Mouse")
		//		{
		//			var temp = PlayerInput.Instantiate(playerPrefab);

		//			//OnPlayerJoined(temp);
		//			i++;
		//		}
		//		else
		//		{
		//			i++;
		//		}
		//	}
		//}

		SpawnPlayersOnStart();
	}

	private void Update()
	{
		
	}

	private void SpawnPlayersOnStart()
	{
		var devices = InputSystem.devices;

		foreach (var device in devices)
		{
			if(device is Gamepad || device is Keyboard)
			{
				if(spawnedPlayerCount <= spawnPoints.Length)
				{
					PlayerInput newPlayer = playerInputManager.JoinPlayer(spawnedPlayerCount, spawnedPlayerCount, null, device);

					if(newPlayer != null)
					{
						Transform targetSpawn = spawnPoints[spawnedPlayerCount];
						newPlayer.transform.position = targetSpawn.position;
						newPlayer.transform.rotation = targetSpawn.rotation;

						if (newPlayer.TryGetComponent<Rigidbody>(out Rigidbody rb))
						{
							rb.position = targetSpawn.position;
						}
					}

					spawnedPlayerCount++;
				}
			}
		}
	}

	//public void OnPlayerJoined(PlayerInput playerInput)
	//{
	//	//Debug.Log(playerInput.transform.position);
	//	playerInput.transform.position = spawnPoints[playerCount].transform.position;
	//       playerInput.transform.rotation = spawnPoints[playerCount].transform.rotation;
	//       //Debug.Log(playerInput.transform.position);

	//       playerCount++;           
	//}
}
