using UnityEngine;

public class cartRunManager : IndividualTaskManager
{
    public Task pairedCartRun;

	public override void StartTask()
	{
		base.StartTask();


		pairedCartRun.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedCartRun.EndingTask();
	}
}
