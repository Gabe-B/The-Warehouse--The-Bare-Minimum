using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerSpawn : MonoBehaviour
{
	//The spawn points for the players
	public Transform[] spawnPoints;

	//Maps the inputs of connected controllers to their player number (i.e. player 0 would be tied to the first connected input device. player 1 would be connected to the second. etc up to player 3 [a.k.a the 4th connected input])
	public Dictionary<int, PlayerInput> controllers = new Dictionary<int, PlayerInput>();

	//Lists of the closed/opened box gameobjects
	public List<GameObject> closedBoxes, openBoxes;

	//Gets reference for the spawning pallets intro 'animation'
	public BasicPathFollow bpf;

	//A global singleton reference for this script/object
	public static PlayerSpawn _instance { get; private set; }

	//Events system setup
	public delegate void Spawn();
	public static Spawn Spawned;
	public static Spawn StartGame;

	//Reference for the PIM component
	private PlayerInputManager playerInputManager;

	//Default value for the spawned player count
	private int spawnedPlayerCount = 0;

	//Default value for the bool that tells the script whether or not the game has started
	private bool inBox = true;

	private void Awake()
	{
		//Makes object singleton
		if (_instance != null && _instance != this)
		{
			Debug.Log("Duplicate PlayerSpawn found. Deleting instance.");
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

		//Sets reference for the PIM
		playerInputManager = GetComponent<PlayerInputManager>();
	}

	private void Start()
	{
		//Spawns the players
		SpawnPlayersOnStart();

		//Sets the # of closed-box gameobjects to 'active' based on the number of players
		for (int p = 0; p < controllers.Count; p++)
		{
			Debug.Log(p);
			closedBoxes[p].SetActive(true);
		}
	}

	private void Update()
	{
		//Checks if the players are on spawn pallet
		if(inBox)
		{
			//Repeats process for each controller connected
			for (int i = 0; i < controllers.Count; i++)
			{
				//Sets the gameobejcts position based on the players number (player 0 goes to poistion 0, etc)
				controllers[i].gameObject.transform.position = spawnPoints[i].position;

				//Checks for the lerp action of the spawn pallet to finish while also checking for the 'interact' button of player 0 to be pressed
				if (controllers[0].actions.FindAction("Interact").WasPressedThisFrame() && bpf.isDoneLerping)
				{
					//Calls the event to trigger
					StartGame.Invoke();
					
					//Sets the parent of the player to null (which removes them from being parented to the pallet) and sets the box they're in to be the open-box object
					for (int x = 0; x < controllers.Count; x++)
					{
						controllers[x].gameObject.GetComponentInParent<Transform>().SetParent(null);

						closedBoxes[x].SetActive(false);
						openBoxes[x].SetActive(true);
					}

					//Ends the loop
					inBox = false;
				}
			}
		}
	}

	private void SpawnPlayersOnStart()
	{
		//List of all devices connected to current computer (or console? maybe?)
		var devices = InputSystem.devices;

		//Runs for each device connected
		foreach (var device in devices)
		{
			//Checks if the device is a controller or keyboard (ignores mice because they pass their values in together with the keyboard)
			if (device is Gamepad || device is Keyboard)
			{
				//Only runs as many times as there are spawn points
				if (spawnedPlayerCount <= spawnPoints.Length)
				{
					//Spawns in the player and ties them to the device
					PlayerInput newPlayer = playerInputManager.JoinPlayer(spawnedPlayerCount, spawnedPlayerCount, null, device);

					//Double checks the player isn't null
					if (newPlayer != null)
					{
						//Sets their spawn position to the spawn point on the pallet
						Transform targetSpawn = spawnPoints[spawnedPlayerCount];

						//Checks that the player object has a rigidbody
						if (newPlayer.TryGetComponent<Rigidbody>(out Rigidbody rb))
						{
							//Calles the event for spawning in the players
							Spawned.Invoke();

							//Sets the parent of the player to the spawn point on the pallet
							rb.GetComponentInParent<Transform>().SetParent(spawnPoints[spawnedPlayerCount]);
							rb.position = targetSpawn.position;
						}

						//Adds the player input to a dictionary and ties it to a player number (player 0 is tied to the first input device)
						controllers.Add(spawnedPlayerCount, newPlayer);

						//increments the player count
						spawnedPlayerCount++;
					}
				}
			}
		}
	}
}
