using System.Linq;

public class LockPuzzleManager : Singleton<LockPuzzleManager>
{
    public Switch[] switches;
    public Door lockedDoor;
    public DisappearingBlock[] disappearingBlocks = { };

    private bool completed = false;

    private void Update()
    {
        if (!completed && switches.All(s => s.Activated))
        {
            completed = true;

            if (lockedDoor)
                lockedDoor.Unlock();

            foreach (var block in disappearingBlocks)
            {
                Destroy(block.gameObject);
            }
        }
    }
}
