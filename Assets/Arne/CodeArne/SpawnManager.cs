using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public TrunkSpawner trunkSpawner;

    public void SpawnTriggerEntered()
    {
        trunkSpawner.moveBranch();
    }
}
