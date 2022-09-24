using DG.Tweening;
using UnityEngine;

public class WindPlane_Movement: MonoBehaviour
{
    //[SerializeField]
    private GameObject windPlaneGO;

    [SerializeField]
    private float windPlaneRotateValue;

    // Start is called before the first frame update
    void Start()
    {
        windPlaneGO = this.gameObject;

        //At Start, make sure the starting Z-value is the negative of windPlaneRotateValue
        var startWindPlaneRotation = new Vector3(0, 0, windPlaneRotateValue * -1);
        windPlaneGO.transform.eulerAngles = startWindPlaneRotation;

        //Start the pendulum Tween
        Wind_AutoTween();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Wind_AutoTween()
    {
        //windPlane's Rotation Z-value
        var windPlaneRotation_Z = windPlaneGO.transform.rotation.eulerAngles.z;

        var windPlaneTweenTo = new Vector3(0, 0, windPlaneRotateValue);

        //We now tween the Z-value between 2 values -> Pendulum-style
        windPlaneGO.transform.DORotate(windPlaneTweenTo, 1)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
