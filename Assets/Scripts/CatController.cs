using DG.Tweening;
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

    [SerializeField]
    private float fadeDuration = 0.01f;

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
        arrowIndication();
    }

    private void arrowIndication()
    {
        var leftArrowColor = arrowLeft.color;
        var rightArrowColor = arrowRight.color;
        //If plane is moving left -> Click right/arrow right

        if (windPlane_Movement.IsWindPlaneMovingLeft)
        {
            arrowLeft.DOFade(1, fadeDuration);
            arrowRight.DOFade(0.5f, fadeDuration);
        }

        else if (windPlane_Movement.IsWindPlaneMovingRight)
        {
            arrowLeft.DOFade(0.5f, fadeDuration);
            arrowRight.DOFade(1, fadeDuration);
        }

        else
        {
            arrowLeft.DOFade(0.5f, fadeDuration);
            arrowRight.DOFade(0.5f, fadeDuration);
        }
    }
}
