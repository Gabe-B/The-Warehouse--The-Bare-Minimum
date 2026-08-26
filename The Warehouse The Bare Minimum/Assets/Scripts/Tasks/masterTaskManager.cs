using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

public class masterTaskManager : MonoBehaviour
{
	public delegate void TaskHandler();
	public static TaskHandler TaskStart;

	public List<IndividualTaskManager> taskManagerList, selectedTaskManagers;
	public GameObject taskSelectedObj;

	private bool t_tasksHaveBeenCompleted = false;
	private bool t_tasksHaveBeenGiven = false;

	//Run the task initializing setup
	void Start()
	{
		//Gets each instance of the task managers
		foreach (IndividualTaskManager itm in FindObjectsOfType<IndividualTaskManager>())
		{
			taskManagerList.Add(itm);
		}
	}

	// Update is called once per frame
	void Update()
	{
		UpdateTaskStatus();

		//Selects tasks to be done
		if (Input.GetKeyDown(KeyCode.L))
		{
			t_tasksHaveBeenCompleted = false;
			givingTasks();
		}

		//Starts the tasks
		if (Input.GetKeyDown(KeyCode.K))
		{
			TaskStart.Invoke();
		}

		//Ends the tasks and handlers
		if (Input.GetKeyDown(KeyCode.H))
		{
			endAllTask();
			ClearTaskHandlers();
		}
	}

	void UpdateTaskStatus()
	{
		Debug.Log($"Selected tasks count: {selectedTaskManagers.Count}");
		if (selectedTaskManagers.Count > 0 && !t_tasksHaveBeenCompleted)
		{
			foreach (IndividualTaskManager itm in selectedTaskManagers)
			{
				//Debug.Log($"im here: {itm.hasBeenCompleted}");
				if (itm.hasBeenCompleted)
				{
					Debug.Log($"removing completed task: {itm.name}");
					TaskStart -= itm.StartTask;
					itm.hasBeenSelected = false;
					itm.hasBeenCompleted = false;
					selectedTaskManagers.Remove(itm);
				}

				if(selectedTaskManagers.Count == 0)
				{
					t_tasksHaveBeenCompleted = true;
				}
			}
		}
		else if (t_tasksHaveBeenCompleted)
		{
			Debug.Log("All tasks have been completed");
			t_tasksHaveBeenGiven = false;
			ClearTaskHandlers();
		}
	}

	void givingTasks()
	{
		if (t_tasksHaveBeenGiven) return;

		//Getting random number for initial tasks
		int initTaskCount = Random.Range(1, taskManagerList.Count);

		//Clears list from last run
		//selectedTaskManagers.Clear(); //Commented out because the update task status function should handle it

		//Debug.Log($"The number of selected tasks is {initTaskCount}");

		//Looping through the list to find the initial tasks 
		for (int i = 0; i < initTaskCount; i++)
		{
			//Gets a random task from the list
			int randomIndex = Random.Range(0, taskManagerList.Count);
			taskSelectedObj = taskManagerList[randomIndex].gameObject;

			//Checks if individual task has already been selected and rerolls if it has (there can be multiple instances of the same task)
			while (selectedTaskManagers.Contains(taskSelectedObj.GetComponent<IndividualTaskManager>()))
			{
				randomIndex = Random.Range(0, taskManagerList.Count);
				taskSelectedObj = taskManagerList[randomIndex].gameObject;
			}

			//Double checks that the task wasn't selected previously and adds it to the selected list. Also adds the "StartTask" event to the handler
			if (taskSelectedObj.GetComponent<IndividualTaskManager>().hasBeenSelected == false)
			{
				selectedTaskManagers.Add(taskSelectedObj.GetComponent<IndividualTaskManager>());
				selectedTaskManagers[i].hasBeenSelected = true;

				//Debug.Log($"{selectedTaskManagers[i]} has been selected");
				TaskStart += selectedTaskManagers[i].StartTask;
			}

			t_tasksHaveBeenGiven = true;
		}
	}

	//Marks each selected task as unselected
	void endAllTask()
	{
		foreach (IndividualTaskManager itm in selectedTaskManagers)
		{
			itm.hasBeenSelected = false;
		}
	}

	//Clears the handlers
	void ClearTaskHandlers()
	{
		foreach (IndividualTaskManager itm in selectedTaskManagers)
		{
			TaskStart -= itm.StartTask;
		}
	}
}