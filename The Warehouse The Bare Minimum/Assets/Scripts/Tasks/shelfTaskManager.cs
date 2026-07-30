using UnityEngine;

public class shelfTaskManager : IndividualTaskManager
{
    public Task pairedShelf;

	private void Update()
	{
		if(pairedShelf.isComplete)
		{
			EndTask();
			pairedShelf.isComplete = false;
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

		hasBeenCompleted = true;
	}
}
