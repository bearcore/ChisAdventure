using UnityEngine;

[CreateAssetMenu(fileName = "CatData", menuName = "ScriptableObject/CatValue", order = 0)]
public class CatScriptableObject: ScriptableObject
{
    [SerializeField]
    private bool hasCatFailed;

    public bool HasCatFailed
    {
        get
        {
            return hasCatFailed;
        }
        set
        {
            hasCatFailed = value;
        }
    }

    [SerializeField]
    private bool hasCatSucceed;

    public bool HasCatSucceed
    {
        get
        {
            return hasCatSucceed;
        }
        set
        {
            hasCatSucceed = value;
        }
    }

    //Value for cat's stable and wind
    [Tooltip("How much the cat will stay stable on heavy wind without tipping over")]
    [SerializeField]
    [Range(0f, 10f)]
    private float stabilizationValue;

    public float StabilizationValue
    {
        get => stabilizationValue;
        set => stabilizationValue = value;
    }

    [SerializeField]
    [Tooltip("My saved High score")]
    private int highScore;

    public int HighScore
    {
        get
        {
            return highScore;
        }
        set
        {
            highScore = value;
        }
    }

    [SerializeField]
    private int currentScore;
    [Tooltip("My current score")]

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
        }
    }

    private void OnEnable()
    {
        currentScore = 0;

        highScore = PlayerPrefs.GetInt("HighScore");
    }
}
