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

            //Kill all tween
            DOTween.KillAll();
            Debug.Log("Timer Stop");

            //Reset plane to null pos
            Wind_RotateReset();

            //isPlaneMoving = false
            isWindPlaneMoving = false;
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

    private void Wind_Rotate()
    {
        //We random the duration number
        //The smaller the range, the harsher the wind.
        var duration = Random.Range(minRotationSpeed, maxRotationSpeed);

        DOTween.SetTweensCapacity(2000, 100);
        //Create a tween sequence
        Sequence windSequence = DOTween.Sequence();
        //First move left
        windSequence.Append(windPlaneGO.transform.DORotate(rotation, duration))
        //After that, move right
                    .Append(windPlaneGO.transform.DORotate(-rotation, duration))
        .SetEase(Ease.Linear) //Make sure the tween is smooth
        .SetLoops(-1, LoopType.Yoyo) //We loop the amount equal of windPlaneMoveduration
        .OnComplete(onTweenComplete);
    }

    private void Wind_RotateReset()
    {
        Vector3 resetPos = new Vector3(0, 0, 0);
        windPlaneGO.transform.DORotate(resetPos, 1);
        Debug.Log("Reset plane to 0 position");
    }

    private void onTweenComplete()
    {
        //+1 to the complete phase
        Debug.Log("Finished 1 phase");
    }

}
