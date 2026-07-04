using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

public class masterTaskManager : MonoBehaviour
{
    public delegate void Task();
    public static Task TaskStart;
    public static Task TaskEnd;

    //public List<GameObject> taskList = new List<GameObject>();
    public List<IndividualTaskManager> taskManagerList;
    //public List<GameObject> activeTasks = new List<GameObject>();
    public Dictionary<string,int> counts = new Dictionary<string, int>();
    public GameObject taskSelected;

    //setting references to scripts | first letter of each word of task
    //public carryOutManager coRef;
    //public cartRunManager crRef;
    //public laserLineManager llRef;
    //public onlineOrdersManager ooRef;
    //public helpCustomersManager hcRef;
    //public hangTvManager htRef;

    //Run the task initializing setup
    void Start()
    {
        foreach (IndividualTaskManager itm in FindObjectsOfType<IndividualTaskManager>())
		{
            taskManagerList.Add(itm);
		}

        givingTasks();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void givingTasks()
    {
        //Getting random number for initial tasks
        int initTaskCount = Random.Range(3, 6);
        //Looping through the list to find the initial tasks 
        for (int i = 0; i < initTaskCount; i++)
        {
            int randomIndex = Random.Range(0,  taskManagerList.Count/*taskList.Count*/);
			taskSelected = taskManagerList[randomIndex].gameObject;
            string taskName = taskSelected.name;

            //Access gameobject script and change the bool activating that tasks manager script
            if (taskName == "Cart Run")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }
            if (taskName == "Carry Out")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }
            if (taskName == "Laser Line")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }
            if (taskName == "Help Customer")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }
            if (taskName == "Hang TV")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }
            if (taskName == "Online Order")
            {
                TaskStart += taskManagerList[randomIndex].StartTask;
            }

            //Add gameobject to activetask list
            //activeTasks.Add(taskSelected);
		}

        //Count quantity of each unique item in active task list (can be rewritten easily had to google)
        //foreach (var item in activeTasks)
        //{
        //    counts[item.name] = counts.TryGetValue(item.name, out int count) ? count + 1 : 1;
        //    Debug.Log(counts[item.name]+"X "+item.name);
        //}
        ////Shows total active tasks
        //Debug.Log(activeTasks.Count);
    }
}
//get random number n of initial tasks
//loop through the list of tasks n number of times each time picking a random task
//Access the gameobjects script and changing its activeTask bool to true
//and adding the task to the activetasks list
//if a task gets completed find it in the activetasks list change the bool to false and remove it from the list