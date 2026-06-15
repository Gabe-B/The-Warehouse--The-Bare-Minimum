using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ProBuilder;

public class taskManager : MonoBehaviour
{
    public List<GameObject> taskList = new List<GameObject>();
    public List<GameObject> activeTasks = new List<GameObject>();
    public Dictionary<string,int> counts = new Dictionary<string, int>();
    public GameObject taskSelected;

    //setting references to scripts | first letter of each word of task
    public carryOutManager coRef;
    public cartRunManager crRef;
    public laserLineManager llRef;
    public onlineOrdersManager ooRef;
    public helpCustomersManager hcRef;
    public hangTvManager htRef;

    //Run the task initializing setup
    void Start()
    {
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
            int randomIndex = Random.Range(0, taskList.Count);
            taskSelected = taskList[randomIndex];
            string taskName = taskList[randomIndex].name;

            //Access gameobject script and change the bool activating that tasks manager script
            if (taskName == "Cart Run")
            {
                crRef.taskActive = true;
            }
            if (taskName == "Carry Out")
            {
                coRef.taskActive = true;
            }
            if (taskName == "Laser Line")
            {
                llRef.taskActive = true;
            }
            if (taskName == "Help Customer")
            {
                hcRef.taskActive = true;
            }
            if (taskName == "Hang TV")
            {
                htRef.taskActive = true;
            }
            if (taskName == "Online Order")
            {
                ooRef.taskActive = true;
            }

            //Add gameobject to activetask list
            activeTasks.Add(taskSelected);
        }

        //Count quantity of each unique item in active task list (can be rewritten easily had to google)
        foreach (var item in activeTasks)
        {
            counts[item.name] = counts.TryGetValue(item.name, out int count) ? count + 1 : 1;
            Debug.Log(counts[item.name]+"X "+item.name);
        }
        //Shows total active tasks
        Debug.Log(activeTasks.Count);
    }
}
//get random number n of initial tasks
//loop through the list of tasks n number of times each time picking a random task
//Access the gameobjects script and changing its activeTask bool to true
//and adding the task to the activetasks list
//if a task gets completed find it in the activetasks list change the bool to false and remove it from the list