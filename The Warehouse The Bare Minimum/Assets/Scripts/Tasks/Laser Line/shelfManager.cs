using System.Collections.Generic;
using UnityEngine;

public class shelfManager : Task
{
    public List<boxManager> boxes;

    [HideInInspector]
    private bool b_isComplete;

    public override void StartingTask()
    {
        Debug.Log("The laser line has begun");
        
        foreach(boxManager b in boxes)
		{
            b.b_isDirty = true;
		}

        b_isComplete = false;
    }

    public override void EndingTask()
    {
        Debug.Log("The laser line has ended");
    }
}
