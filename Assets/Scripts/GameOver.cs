using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameOver: MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI gameOverTMP, timerTMP;

    [Header("WindPlane_Movement > Wind_RotateStop()")]
    [SerializeField]
    private UnityEvent gameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        gameOverTMP.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GameOver_Box")
        {
            gameOverTMP.enabled = true;
            gameOverTMP.text = "Game Over";

            //Invoke Wind_RotateStop Event
            gameOverEvent?.Invoke();

            timerTMP.enabled = false;
        }
        Debug.Log("Collide with " + other.name);
    }
}
