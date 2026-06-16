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
    public Transform startPosition, midPosition, endPosition;
    private float dDTime1 = .75f;
    private float dDTime2 = .25f;
    private float elapsedTime, elapsedTime2;
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
        if (buttonPressed == true)
        {
            elapsedTime += Time.deltaTime;
            float percentageComplete = elapsedTime / dDTime1;

            lobbyJack.transform.position = Vector3.Lerp(startPosition.position, midPosition.position, percentageComplete);
            lobbyJack.transform.rotation = Quaternion.Lerp(startPosition.rotation, midPosition.rotation, percentageComplete);
        }

        if (lobbyJack.transform.position == midPosition.position)
        {
            elapsedTime2 += Time.deltaTime;
            float percentageComplete2 = elapsedTime2 / dDTime2;

            lobbyJack.transform.position = Vector3.Lerp(midPosition.position, endPosition.position, percentageComplete2);
            lobbyJack.transform.rotation = Quaternion.Lerp(midPosition.rotation, endPosition.rotation, percentageComplete2);
            if (lobbyJack.transform.position == endPosition.position)
            {
                int randomMap = Random.Range(SceneManager.GetActiveScene().buildIndex + 1, maps.Count + 1);

                Debug.Log(randomMap);

                SceneManager.LoadScene(randomMap);
            }
        }

    }

    public void OnStartPressed()
    {
        buttonPressed = true;

    }
}
