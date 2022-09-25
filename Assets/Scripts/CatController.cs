using System.Collections.Generic;
using System.Linq;
using Assets.PlayerPrefs;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CatController: MonoBehaviour
{
    [SerializeField]
    private WindPlaneScriptableObject windPlaneScriptableObject;
    [SerializeField]
    private CatScriptableObject catScriptableObject;

    [Tooltip("Added amount to Stabilization if player click the correct button")]
    [SerializeField]
    private float correctClickStabil_Value;

    [SerializeField]
    private WindPlane_Movement windPlane_Movement;

    [Header("Arrows' Logic")]
    [SerializeField]
    private float fadeDuration = 0.01f;

    [SerializeField]
    private Image arrowLeft, arrowRight;

    public List<KeyCode> LeftButtons = new List<KeyCode>
    {
        KeyCode.LeftArrow, KeyCode.A
    };
    public List<KeyCode> RightButtons = new List<KeyCode>
    {
        KeyCode.RightArrow, KeyCode.D
    };

    public static UnityEvent LeftPressed = new UnityEvent();
    public static UnityEvent RightPressed = new UnityEvent();


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
        //Reset all Arrows' Alpha value to 0.2f at start
        ArrowIndication();

        ArrowPressDifficulty();
    }

    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ArrowIndication();
        ArrowQTE();
    }

    private void ArrowIndication()
    {
        //If plane is moving left -> Arrow right
        if (windPlaneScriptableObject.IsWindPlaneMovingLeft)
        {
            arrowLeft.DOFade(0.2f, fadeDuration);
            arrowRight.DOFade(1, fadeDuration);
        }

        //If plane is moving Right -> Click right/arrow Left
        else if (windPlaneScriptableObject.IsWindPlaneMovingRight)
        {
            arrowLeft.DOFade(1, fadeDuration);
            arrowRight.DOFade(0.2f, fadeDuration);
        }

        else
        {
            arrowLeft.DOFade(0.2f, fadeDuration);
            arrowRight.DOFade(0.2f, fadeDuration);
        }
    }

    private void ArrowQTE()
    {
        //If plane is moving left -> Player need to click Right
        if (windPlaneScriptableObject.IsWindPlaneMovingLeft)
        {
            if (LeftButtons.Any(x => Input.GetKeyDown(x)))
            {
                Debug.Log("Correct to the Left!");
                catScriptableObject.StabilizationValue += correctClickStabil_Value;
                LeftPressed.Invoke();
            }

            else if (RightButtons.Any(x => Input.GetKeyDown(x)))
            {
                catScriptableObject.StabilizationValue -= correctClickStabil_Value;
                Debug.Log("FALSE!!! (left)");
                RightPressed.Invoke();
            }
        }

        //If plane is moving Right -> Player need to click Left
        else if (windPlaneScriptableObject.IsWindPlaneMovingRight)
        {
            if (LeftButtons.Any(x => Input.GetKeyDown(x)))
            {
                Debug.Log("Correct to the Right!");
                catScriptableObject.StabilizationValue -= correctClickStabil_Value;
                LeftPressed.Invoke();
            }

            else if (RightButtons.Any(x => Input.GetKeyDown(x)))
            {
                catScriptableObject.StabilizationValue += correctClickStabil_Value;
                Debug.Log("FALSE!!! (right)");
                RightPressed.Invoke();
            }
        }

        else
        {
            //Debug.Log("Nothing is happening");
        }
    }

    private void ArrowPressDifficulty()
    {
        //The difficulty will depends on how long the duration
        //If duration is 1 -> Start stability is 1

        switch (windPlaneScriptableObject.WindPlaneMoveDuration)
        {
            case 0:
                break;
            case 1:
                catScriptableObject.StabilizationValue = 1;
                break;
            case 2:
                catScriptableObject.StabilizationValue = 0.9f;
                break;
            case 3:
                catScriptableObject.StabilizationValue = 0.8f;
                break;
            case 4:
                catScriptableObject.StabilizationValue = 0.6f;
                break;
            case 5:
                catScriptableObject.StabilizationValue = 0.4f;
                break;
            case 6:
                catScriptableObject.StabilizationValue = 0.2f;
                break;
        }
    }
}
