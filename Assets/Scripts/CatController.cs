using UnityEngine;
using UnityEngine.UI;

public class CatController: MonoBehaviour
{
    [SerializeField]
    private CatBalancing catBalancing;

    [SerializeField]
    private WindPlane_Movement windPlane_Movement;

    [SerializeField]
    private Image arrowLeft, arrowRight;

    //The logic of this controller is basically to click the corresponding arrow
    //Depends on if the platform is going left or right
    //As the speed is random > the time window might be tiny

    //The cat will build up stabilization after each right click.
    //But punish heavily if mis-click
    //We shall access the bool Variables in WindPlane_Movement
    //To see if the plane is moving left/right


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
