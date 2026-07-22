using System.Collections.Generic;
using UnityEngine;

public class shelfManager : Task
{
    public List<boxManager> boxes;

    private List<bool> b_boxesCompleteStatus;
    private bool hasBeenCompleted = false;

	private void Start()
	{
        for (int i = 0; i <= boxes.Count; i++)
		{
            bool tempBool = false;
            b_boxesCompleteStatus.Add(tempBool);
		}
	}

	private void Update()
	{
        if(!isComplete && isInProgress)
		{
            for (int i = 0; i <= boxes.Count; i++)
            {
                if (boxes[i].b_hasBeenCleaned)
                {
                    b_boxesCompleteStatus[i] = true; //YOURE TRYING TO FIND A WAY TO CHECK THAT ALL OF THE BOXES HAVE BEEN MARKED AS COMPLETE
                }
            }
        }
    }

	public override void StartingTask()
    {
        Debug.Log("The laser line has begun");
        
        foreach(boxManager b in boxes)
		{
            b.b_isDirty = true;
            b.b_hasBeenCleaned = false;
		}

        isInProgress = true;
    }

    public override void EndingTask()
    {
        Debug.Log("The laser line has ended");
        isInProgress = false;
    }
}
