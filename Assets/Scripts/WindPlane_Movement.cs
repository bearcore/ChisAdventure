using DG.Tweening;
using TMPro;
using UnityEngine;

public class WindPlane_Movement: MonoBehaviour
{
    //[SerializeField]
    private GameObject windPlaneGO;

    //[Header("Rotate through Tween")]
    //[SerializeField]
    //private float windPlaneRotateValue;

    [Header("Rotation values")]
    [SerializeField]
    private bool isWindPlaneMoving = false;
    [SerializeField]
    private bool isWindPlaneMovingLeft, isWindPlaneMovingRight;

    [SerializeField]
    private Vector3 rotation;

    [Tooltip("How slow is the tween")]
    [SerializeField]
    [Range(0.5f, 1f)]
    private float minRotationSpeed = 0.5f;
    [Tooltip("How fast is the tween")]
    [SerializeField]
    [Range(1f, 3f)]
    private float maxRotationSpeed = 1f;

    [Tooltip("How long is the windy duration")]
    [SerializeField]
    [Range(1, 6)]
    private int windPlaneMoveDuration = 1;

    [Header("Timer")]
    [SerializeField]
    private float currentTime;
    [SerializeField]
    private TextMeshProUGUI timerTMP;

    public bool IsWindPlaneMovingLeft
    {
        get => isWindPlaneMovingLeft;
        set => isWindPlaneMovingLeft = value;
    }
    public bool IsWindPlaneMovingRight
    {
        get => isWindPlaneMovingRight;
        set => isWindPlaneMovingRight = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        windPlaneGO = this.gameObject;

        currentTime = windPlaneMoveDuration;
        Wind_Rotate();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerTMP.text = currentTime.ToString("0");

        if (currentTime > 0)
        {
            isWindPlaneMoving = true;
        }

        //If timer hit 0
        if (currentTime <= 0)
        {
            currentTime = 0;
            //Debug.Log("Timer Stop");

            if (isWindPlaneMoving)
            {
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
        isWindPlaneMoving = false;
        isWindPlaneMovingLeft = false;
        isWindPlaneMovingRight = false;

        //Debug.Log("Wind Stop");
    }

    //Wind Rotate but random whether it start from left or right
    private void Wind_Rotate()
    {
        int action = Random.Range(0, 1);
        switch (action)
        {
            case 0:
                Wind_Rotate_Left();
                break;
            case 1:
                Wind_Rotate_Right();
                break;
        }
    }

    //Start rotate to left
    private void Wind_Rotate_Left()
    {
        //We random the duration number
        //The smaller the range, the harsher the wind.
        var duration = Random.Range(minRotationSpeed, maxRotationSpeed);

        DOTween.SetTweensCapacity(2000, 100);
        //Create a tween sequence
        Sequence windSequence = DOTween.Sequence();
        //First move left
        windSequence.Append(windPlaneGO.transform.DORotate(rotation, duration))
                    .InsertCallback(0.01f, Change_isWindPlaneMovingLeft)
        //After that, move right
                    .Append(windPlaneGO.transform.DORotate(-rotation, duration))
                    .InsertCallback(duration + 0.01f, Change_isWindPlaneMovingRight)
        .SetEase(Ease.Linear) //Make sure the tween is smooth
        .SetLoops(-1, LoopType.Yoyo) //We loop the amount equal of windPlaneMoveduration
        .OnComplete(onTweenComplete);

        Debug.Log("Rotate Left");
    }

    //Start rotate to right
    private void Wind_Rotate_Right()
    {
        //We random the duration number
        //The smaller the range, the harsher the wind.
        var duration = Random.Range(minRotationSpeed, maxRotationSpeed);

        DOTween.SetTweensCapacity(2000, 100);
        //Create a tween sequence
        Sequence windSequence = DOTween.Sequence();
        //First move right
        windSequence.Append(windPlaneGO.transform.DORotate(-rotation, duration))
                    .InsertCallback(0.01f, Change_isWindPlaneMovingRight)
        //After that, move left
                    .Append(windPlaneGO.transform.DORotate(rotation, duration))
                    .InsertCallback(duration + 0.01f, Change_isWindPlaneMovingLeft)
        .SetEase(Ease.Linear) //Make sure the tween is smooth
        .SetLoops(-1, LoopType.Yoyo) //We loop the amount equal of windPlaneMoveduration
        .OnComplete(onTweenComplete);

        Debug.Log("Rotate Right");
    }

    private void Change_isWindPlaneMovingLeft()
    {
        isWindPlaneMovingLeft = true;
        isWindPlaneMovingRight = false;
        Debug.Log("Moving Left");
    }

    private void Change_isWindPlaneMovingRight()
    {
        isWindPlaneMovingLeft = false;
        isWindPlaneMovingRight = true;
        Debug.Log("Moving Right");
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
