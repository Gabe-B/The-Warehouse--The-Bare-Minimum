using UnityEngine;

public class cartRun : Task
{
    public override void StartingTask()
    {
        Debug.Log("The cart run has begun");
    }

    public override void EndingTask()
    {
        Debug.Log("The cart run has ended");
    }
}
