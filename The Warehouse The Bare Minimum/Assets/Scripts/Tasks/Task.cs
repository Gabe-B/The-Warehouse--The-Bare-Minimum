using UnityEngine;

public class Task : MonoBehaviour
{
	public bool isComplete = false;
	public bool isInProgress = false;

	public virtual void StartingTask()
	{
		Debug.Log("The carryout has begun");
	}

	public virtual void EndingTask()
	{
		Debug.Log("The carryout has ended");
	}

}
