using UnityEngine;

public class shelfTaskManager : IndividualTaskManager
{
    public Task pairedShelf;

	public override void StartTask()
	{
		base.StartTask();

		pairedShelf.StartingTask();
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedShelf.EndingTask();
	}
}
