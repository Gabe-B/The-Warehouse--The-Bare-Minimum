using UnityEngine;

public class IndividualTaskManager : MonoBehaviour
{
	public new string name;

	private void OnEnable()
	{
        masterTaskManager.TaskStart += StartTask;
		masterTaskManager.TaskEnd += EndTask;
    }

	private void OnDisable()
	{
		masterTaskManager.TaskStart -= StartTask;
		masterTaskManager.TaskEnd -= EndTask;
	}

    public virtual void StartTask()
	{
		Debug.Log($"starting task: {name}");
	}

	public virtual void EndTask()
	{
		Debug.Log($"ending task: {name}");
	}
}
