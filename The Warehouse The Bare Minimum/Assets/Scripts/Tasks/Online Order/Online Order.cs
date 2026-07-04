using UnityEngine;

public class OnlineOrder : Task
{
    public override void StartingTask()
    {
        Debug.Log("The online order has begun");
    }

    public override void EndingTask()
    {
        Debug.Log("The online order has ended");
    }
}
