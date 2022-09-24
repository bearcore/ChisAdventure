using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrunkSpawner : MonoBehaviour
{

    public List<GameObject> trunks;
    
    private float offset = 2400f;
    // Start is called before the first frame update
    void Start()
    {
        if(trunks != null && trunks.Count > 0)
        {
            trunks = trunks.OrderBy(b => b.transform.position.z).ToList();
        }
    }

    public void moveBranch()
    {
        GameObject moveBranch = trunks[0];
        trunks.Remove(moveBranch);
        float newZ = trunks[trunks.Count - 1].transform.position.z + offset;
        moveBranch.transform.position = new Vector3(0, 0, newZ);
        trunks.Add(moveBranch);
    }
}
