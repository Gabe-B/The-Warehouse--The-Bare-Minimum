using System.Collections.Generic;
using UnityEngine;

public class shelfManager : Task
{
    public List<boxManager> boxes;

	private bool hasBeenCompleted = false;
    private int cleanedBoxes;
    private bool b_hasAddedBoxes = false;

	private void Awake()
	{
        Collider[] box = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity);

        foreach (Collider c in box)
        {
            if (c.GetComponent<boxManager>() && !boxes.Contains(c.GetComponent<boxManager>()))
            {
                boxes.Add(c.GetComponent<boxManager>());
            }
        }
    }

	private void Update()
	{
        if (!isComplete && isInProgress)
		{
            cleanedBoxes = 0;

            for (int i = 0; i <= boxes.Count; i++)
            {
                if (boxes[i].b_hasBeenCleaned && cleanedBoxes < boxes.Count)
                {
                    cleanedBoxes++;

                    //Debug.Log($"Comparing cleaned boxes: {cleanedBoxes} to boxes count: {boxes.Count}");

                    if (cleanedBoxes == boxes.Count)
                    {
                        EndingTask();
                    }
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
        cleanedBoxes = 0;
        isInProgress = true;
        isComplete = false;
    }

    public override void EndingTask()
    {
        Debug.Log("The laser line has ended");
        isInProgress = false;
        isComplete = true;
    }
}
