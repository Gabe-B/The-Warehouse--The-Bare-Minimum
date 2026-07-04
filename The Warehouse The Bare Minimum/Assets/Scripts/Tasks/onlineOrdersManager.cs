using UnityEngine;

public class onlineOrdersManager : IndividualTaskManager
{
    public Task pairedOnlineOrder;

	public override void StartTask()
	{
		base.StartTask();

		pairedOnlineOrder.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedOnlineOrder.EndingTask();
	}
}
