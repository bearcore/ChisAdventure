using Assets.PlayerPrefs;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class SucessClear: MonoBehaviour
{
    [SerializeField]
    private CatScriptableObject catScriptableObject;
    [SerializeField]
    private WindPlaneScriptableObject windPlaneScriptableObject;

    [SerializeField]
    private GameObject catGameObject;

    [SerializeField]
    private TextMeshProUGUI successTMP;

    private void Start()
    {
        successTMP.enabled = false;
        catScriptableObject.HasCatSucceed = false;
    }

    //public method to access in WindPlane_Movement
    public void SuccessfulClear()
    {
        //Check if cat has NOT failed before
        if (!catScriptableObject.HasCatFailed)
        {
            Vector3 resetRotation = new Vector3(0, 0, 0);
            catGameObject.transform.DORotate(resetRotation, 0.1f);

            successTMP.enabled = true;
            successTMP.text = "Clear!";

            //Add score based on the duration
            catScriptableObject.CurrentScore += windPlaneScriptableObject.WindPlaneMoveDuration;


            //If clear, make sure cat is stable regardless
            catScriptableObject.StabilizationValue = 10;
            catScriptableObject.HasCatSucceed = true;
        }
    }
}
