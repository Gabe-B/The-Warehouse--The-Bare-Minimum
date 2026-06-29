using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
	public Transform[] spawnPoints;

	public Dictionary<int, PlayerInput> controllers = new Dictionary<int, PlayerInput>();

	public List<GameObject> closedBoxes, openBoxes;

	public BasicPathFollow bpf;

	public static PlayerSpawn _instance;

	public delegate void Spawn();
	public static Spawn Spawned;
	public static Spawn StartGame;

	private PlayerInputManager playerInputManager;
	private int spawnedPlayerCount = 0;
	private bool inBox = true;

	private void Awake()
	{
		playerInputManager = GetComponent<PlayerInputManager>();

		if (_instance == null)
		{
			_instance = this;
		}
		else
		{
			Debug.Log("There can be only one player spawn script in a scene at a time");
			Destroy(_instance);
		}
	}

	private void Start()
	{
		SpawnPlayersOnStart();

		for (int p = 0; p < controllers.Count; p++)
		{
			Debug.Log(p);
			closedBoxes[p].SetActive(true);
		}
	}

	private void Update()
	{
		if(inBox)
		{
			for (int i = 0; i < controllers.Count; i++)
			{
				controllers[i].gameObject.transform.position = spawnPoints[i].position;

				if (controllers[0].actions.FindAction("Interact").WasPressedThisFrame() && bpf.isDoneLerping)
				{
					StartGame.Invoke();

					for (int x = 0; x < controllers.Count; x++)
					{
						controllers[x].gameObject.GetComponentInParent<Transform>().SetParent(null);

						closedBoxes[x].SetActive(false);
						openBoxes[x].SetActive(true);
					}

					inBox = false;
				}
			}
		}
	}

	private void SpawnPlayersOnStart()
	{
		var devices = InputSystem.devices;

		foreach (var device in devices)
		{
			if (device is Gamepad || device is Keyboard)
			{
				if (spawnedPlayerCount <= spawnPoints.Length)
				{
					PlayerInput newPlayer = playerInputManager.JoinPlayer(spawnedPlayerCount, spawnedPlayerCount, null, device);

					if (newPlayer != null)
					{
						Transform targetSpawn = spawnPoints[spawnedPlayerCount];

						if (newPlayer.TryGetComponent<Rigidbody>(out Rigidbody rb))
						{
							Spawned.Invoke();
							rb.GetComponentInParent<Transform>().SetParent(spawnPoints[spawnedPlayerCount]);
							rb.position = targetSpawn.position;
						}

						controllers.Add(spawnedPlayerCount, newPlayer);

						spawnedPlayerCount++;
					}
				}
			}
		}
	}
}
