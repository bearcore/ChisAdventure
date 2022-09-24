using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOver: MonoBehaviour
{
    [SerializeField]
    private CatScriptableObject catScriptableObject;

    [SerializeField]
    private TextMeshProUGUI gameOverTMP, timerTMP;

    [Header("WindPlane_Movement > Wind_RotateStop(); CatBalancing > ResetStabil")]
    [SerializeField]
    private UnityEvent gameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameOverTMP.enabled = false;
        catScriptableObject.HasCatFailed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameOver_Box")
        {
            gameOverTMP.enabled = true;
            gameOverTMP.text = "Game Over";

            catScriptableObject.HasCatFailed = true;


            //Invoke Wind_RotateStop Event
            gameOverEvent?.Invoke();

            timerTMP.enabled = false;
        }
        //Debug.Log("Collide with " + other.name);
    }
}
