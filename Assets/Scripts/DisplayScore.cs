using TMPro;
using UnityEngine;

public class DisplayScore: MonoBehaviour
{
    [SerializeField]
    private CatScriptableObject catScriptableObject;

    [SerializeField]
    private TextMeshProUGUI currentScoreTMP, highScoreTMP;

    // Start is called before the first frame update
    void Start()
    {
        //Load the highScore from PlayerPref
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreTMP.text = PlayerPrefs.GetInt("HighScore").ToString("0");
            Debug.Log("There is a High score of " + PlayerPrefs.GetInt("HighScore"));
        }
        else
        {
            Debug.Log("There is no High score");
        }

        //Make sure currentScore is also properly displayed
        currentScoreTMP.text = catScriptableObject.CurrentScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Public to access in WindPlane_Movement
    public void UpdateCurrentScore()
    {
        //Display the current score Add the amount of duration to score if clear
        currentScoreTMP.text = catScriptableObject.CurrentScore.ToString("0");
    }
}
