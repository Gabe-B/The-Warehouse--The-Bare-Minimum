using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    public Transform[] spawnPoints;

    private int playerCount;

    public void OnPlayerJoined(PlayerInput playerInput)
	{
        Debug.Log(playerInput.transform.position);
        playerInput.transform.position = spawnPoints[playerCount].transform.position;
        playerInput.transform.rotation = spawnPoints[playerCount].transform.rotation;
        Debug.Log(playerInput.transform.position);

        playerCount++;           
	}
}
