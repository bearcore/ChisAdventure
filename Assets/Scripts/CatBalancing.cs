using UnityEngine;

public class CatBalancing: MonoBehaviour
{
    //Private var for cat's rb
    [SerializeField]
    private Rigidbody catRigidbody;

    //Value for cat's stable and wind
    [Tooltip("How much the cat will stay stable on heavy wind without tipping over")]
    [SerializeField]
    [Range(0f, 10f)]
    private float stabilizationValue;

    [Tooltip("How much wind power effect the cat -> less = more flop")]
    [SerializeField]
    [Range(0, 5f)]
    private float dampingValue;

    //[SerializeField]
    private Vector3 torqueValue;

    public float StabilizationValue
    {
        get => stabilizationValue;
        set => stabilizationValue = value;
    }
    public float DampingValue
    {
        get => dampingValue;
        set => dampingValue = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        catRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        torqueValue = stabilizationValue * Vector3.Cross(transform.up, Vector3.up) - DampingValue * catRigidbody.angularVelocity;
        catRigidbody.AddTorque(torqueValue);
    }
}
