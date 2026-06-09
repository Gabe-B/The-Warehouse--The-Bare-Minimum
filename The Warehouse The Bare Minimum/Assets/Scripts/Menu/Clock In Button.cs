using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class clockInButton : MonoBehaviour
{
    public List<Scene> maps;
    //Setting positions for the Lobby Jack
    public GameObject lobbyJack;
    public Transform startPosition,endPosition;
    private float desiredDuration = .75f;
    private float elapsedTime;
    private bool buttonPressed;

    public int randomSeed { get; private set; }


	private void Awake()
	{
        randomSeed = Mathf.RoundToInt(Random.Range(-2147483648, 2147483648));

        Random.InitState(randomSeed);
        buttonPressed = false;
    }

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            //Debug.Log(SceneManager.GetSceneByBuildIndex(i).name);
            maps.Add(SceneManager.GetSceneByBuildIndex(i));
        }
        //Debug.Log((SceneManager.GetActiveScene().buildIndex + 1) + ", " + maps.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonPressed == true)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / desiredDuration;

            lobbyJack.transform.position = Vector3.Lerp(startPosition.position,endPosition.position,percentageComplete);
        }

        if(lobbyJack.transform.position == endPosition.position)
        {
            int randomMap = Random.Range(SceneManager.GetActiveScene().buildIndex + 1, maps.Count + 1);

            Debug.Log(randomMap);

            SceneManager.LoadScene(randomMap);
        }
                
    }
    
    public void OnStartPressed()
    {
        buttonPressed = true;
        
    }
}
