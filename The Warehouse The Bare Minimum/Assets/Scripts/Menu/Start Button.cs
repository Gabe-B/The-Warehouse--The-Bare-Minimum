using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
	public List<Scene> maps;

	public int randomSeed { get; private set; }

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
		Debug.Log((SceneManager.GetActiveScene().buildIndex + 1) + ", " + maps.Count);	
	}

	public void OnStartPressed()
	{
		int randomMap = Random.Range(SceneManager.GetActiveScene().buildIndex+1, maps.Count+1);

		Debug.Log(randomMap);

		SceneManager.LoadScene(randomMap);
	}
}
