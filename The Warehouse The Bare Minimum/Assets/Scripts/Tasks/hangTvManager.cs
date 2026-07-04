using UnityEngine;

public class hangTvManager : IndividualTaskManager
{
	public Task pairedTVHangTask;

	public override void StartTask()
	{
		base.StartTask();

		pairedTVHangTask.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedTVHangTask.EndingTask();
	}
}
