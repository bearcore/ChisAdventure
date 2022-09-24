using Assets.PlayerPrefs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WindPlane_Movement: MonoBehaviour
{
    [SerializeField]
    private CatScriptableObject catScriptableObject;

    [SerializeField]
    private WindPlaneScriptableObject windPlaneScriptableObject;

    [SerializeField]
    private GameObject catGameObject;
    //[SerializeField]
    private GameObject windPlaneGO;

    //[Header("Rotate through Tween")]
    //[SerializeField]
    //private float windPlaneRotateValue;

    [SerializeField]
    private Vector3 rotation;

    [Header("Timer")]
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private TextMeshProUGUI timerTMP;

    [Header("SuccessClearManager")]
    [SerializeField]
    private UnityEvent successEvent;

    //[SerializeField]
    private int action;

    // Start is called before the first frame update
    void Start()
    {
        windPlaneGO = this.gameObject;

        currentTime = windPlaneScriptableObject.WindPlaneMoveDuration;
        action = Random.Range(0, 4);
        Wind_Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerTMP.text = currentTime.ToString("0");

        //If timer hasn't reach 0 -> Plane continue moving
        if (currentTime > 0)
        {
            windPlaneScriptableObject.IsWindPlaneMoving = true;
        }

        //If timer hit 0
        if (currentTime <= 0)
        {
            currentTime = 0;
            //Debug.Log("Timer Stop");

            //If player succeed eg. Plane was still moving before
            if (windPlaneScriptableObject.IsWindPlaneMoving)
            {
                Vector3 resetRotation = new Vector3(0, 0, 0);
                catGameObject.transform.DORotate(resetRotation, 0.1f);

                //After clearing, disabled the timer;
                timerTMP.enabled = false;
                //Invoke clear event
                successEvent?.Invoke();
                //Stop the wind and make sure it return to OG position
                Wind_RotateStop();
            }
        }
    }

    /*private void Wind_AutoTween()
    {
        //windPlane's Rotation Z-value
        var windPlaneRotation_Z = windPlaneGO.transform.rotation.eulerAngles.z;

        //At Start, make sure the starting Z-value is the negative of windPlaneRotateValue
        var startWindPlaneRotation = new Vector3(0, 0, windPlaneRotateValue * -1);
        windPlaneGO.transform.eulerAngles = startWindPlaneRotation;

        var windPlaneTweenTo = new Vector3(0, 0, windPlaneRotateValue);

        //We now tween the Z-value between 2 values -> Pendulum-style
        windPlaneGO.transform.DORotate(windPlaneTweenTo, 1)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }*/

    //public so I can access in per UnityEvent in GameOver
    public void Wind_RotateStop()
    {
        //Kill all tween
        DOTween.KillAll();

        //Reset plane to null pos
        Wind_RotateReset();

        //isPlaneMoving = false
        windPlaneScriptableObject.IsWindPlaneMoving = false;
        windPlaneScriptableObject.IsWindPlaneMovingLeft = false;
        windPlaneScriptableObject.IsWindPlaneMovingRight = false;

        //Debug.Log("Wind Stop");
    }

    //Wind Rotate but random whether it start from left or right
    private void Wind_Rotate()
    {
        switch (action)
        {
            case 0:
                Wind_Rotate_Left();
                break;
            case 1:
                Wind_Rotate_Right();
                break;
            case 2:
                Wind_Rotate_Left();
                break;
            case 3:
                Wind_Rotate_Right();
                break;
        }
    }

    //Start rotate to left
    private void Wind_Rotate_Left()
    {
        //We random the duration number
        //The smaller the range, the harsher the wind.
        var duration = Random.Range(windPlaneScriptableObject.MinRotationSpeed,
                                         windPlaneScriptableObject.MaxRotationSpeed);

        DOTween.SetTweensCapacity(2000, 100);
        //Create a tween sequence
        Sequence windSequence = DOTween.Sequence();
        //First move left
        windSequence.Append(windPlaneGO.transform.DORotate(rotation, duration))
                    .InsertCallback(0.01f, Change_isWindPlaneMovingLeft)
        //After that, move right
                    .Append(windPlaneGO.transform.DORotate(-rotation, duration))
                    //This call back is called AFTER the first Append -> duration + 0.01f
                    .InsertCallback(duration + 0.01f, Change_isWindPlaneMovingRight)
        .SetEase(Ease.Linear) //Make sure the tween is smooth
        .SetLoops(-1, LoopType.Yoyo) //We loop the amount equal of windPlaneMoveduration
        .OnComplete(onTweenComplete);

        //Debug.Log("Rotate Left");
    }

    //Start rotate to right
    private void Wind_Rotate_Right()
    {
        //We random the duration number
        //The smaller the range, the harsher the wind.
        var duration = Random.Range(windPlaneScriptableObject.MinRotationSpeed,
                                         windPlaneScriptableObject.MaxRotationSpeed);

        DOTween.SetTweensCapacity(2000, 100);
        //Create a tween sequence
        Sequence windSequence = DOTween.Sequence();
        //First move right
        windSequence.Append(windPlaneGO.transform.DORotate(-rotation, duration))
                    .InsertCallback(0.01f, Change_isWindPlaneMovingRight)
        //After that, move left
                    .Append(windPlaneGO.transform.DORotate(rotation, duration))
                    //This call back is called AFTER the first Append -> duration + 0.01f
                    .InsertCallback(duration + 0.01f, Change_isWindPlaneMovingLeft)
        .SetEase(Ease.Linear) //Make sure the tween is smooth
        .SetLoops(-1, LoopType.Yoyo) //We loop the amount equal of windPlaneMoveduration
        .OnComplete(onTweenComplete);

        //Debug.Log("Rotate Right");
    }

    private void Change_isWindPlaneMovingLeft()
    {
        windPlaneScriptableObject.IsWindPlaneMovingLeft = true;
        windPlaneScriptableObject.IsWindPlaneMovingRight = false;
        //Debug.Log("Moving Left");
    }

    private void Change_isWindPlaneMovingRight()
    {
        windPlaneScriptableObject.IsWindPlaneMovingLeft = false;
        windPlaneScriptableObject.IsWindPlaneMovingRight = true;
        //Debug.Log("Moving Right");
    }

    private void Wind_RotateReset()
    {
        Vector3 resetPos = new Vector3(0, 0, 0);
        windPlaneGO.transform.DORotate(resetPos, 1);
        //Debug.Log("Reset plane to 0 position");
    }

    private void onTweenComplete()
    {
        //+1 to the complete phase
        //Debug.Log("Finished 1 phase");
    }

}
