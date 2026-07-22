using UnityEngine;

public class shelfTaskManager : IndividualTaskManager
{
    public Task pairedShelf;

	private void Update()
	{
		if(pairedShelf.isComplete)
		{
			EndTask();
		}
	}

	public override void StartTask()
	{
		base.StartTask();

		pairedShelf.StartingTask();
		hasBeenCompleted = false;
	}

	public override void EndTask()
	{
		base.EndTask();

		pairedShelf.EndingTask();
		hasBeenCompleted = true;
	}
}
