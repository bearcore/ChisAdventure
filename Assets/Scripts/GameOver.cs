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
            SaveHighScore();

            gameOverTMP.enabled = true;
            gameOverTMP.text = "Game Over";

            catScriptableObject.HasCatFailed = true;


            //Invoke Wind_RotateStop Event
            gameOverEvent?.Invoke();

            timerTMP.enabled = false;
        }
        //Debug.Log("Collide with " + other.name);
    }

    private void SaveHighScore()
    {
        //Save score upon Game over
        //Check if the currentScore is bigger than highScore => If yes: Save it to playerPref
        if (catScriptableObject.CurrentScore >= catScriptableObject.HighScore)
        {
            PlayerPrefs.SetInt("HighScore", catScriptableObject.CurrentScore);
            Debug.Log("Game Over. Saving NEW high score");
        }
    }

    public void ResetCurrentScore()
    {
        //If cat has failed before > reset currentScore to 0
        if (catScriptableObject.HasCatFailed)
        {
            catScriptableObject.CurrentScore = 0;
            //Reset hasFailed status back to false after score Reset
            catScriptableObject.HasCatFailed = false;
        }
    }
}
