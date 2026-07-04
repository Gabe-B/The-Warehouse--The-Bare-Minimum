using UnityEngine;

public class carryOutManager : IndividualTaskManager
{
	public Task pairedCarryout;

	public override void StartTask()
	{
		base.StartTask();

		pairedCarryout.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedCarryout.EndingTask();
	}
}
