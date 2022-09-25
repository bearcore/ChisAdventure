using UnityEngine;

public class CatBalancing : MonoBehaviour
{
    //Private var for cat's rb
    [SerializeField]
    private Rigidbody catRigidbody;

    [SerializeField]
    private CatScriptableObject catScriptableObject;

    [Tooltip("How much wind power effect the cat -> less = more flop")]
    [SerializeField]
    [Range(0, 5f)]
    private float dampingValue;

    //[SerializeField]
    private Vector3 torqueValue;


    public float DampingValue
    {
        get => dampingValue;
        set => dampingValue = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        catRigidbody = GetComponent<Rigidbody>();

        CatController.RightPressed.AddListener(RightPressed);
        CatController.RightPressed.AddListener(LeftPressed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        torqueValue = catScriptableObject.StabilizationValue * Vector3.Cross(transform.up, Vector3.up) - DampingValue * catRigidbody.angularVelocity;
        catRigidbody.AddTorque(torqueValue);
    }

    //public to access from GameOver
    public void ResetStabilizationValue()
    {
        catScriptableObject.StabilizationValue = 0;
    }

    private void RightPressed()
    {
        transform.eulerAngles = new Vector3(0, 0, -15);
    }

    private void LeftPressed()
    {
        transform.eulerAngles = new Vector3(0, 0, 15);
    }
}