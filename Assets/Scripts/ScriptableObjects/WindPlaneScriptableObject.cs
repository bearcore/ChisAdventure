using UnityEngine;

namespace Assets.PlayerPrefs
{
    [CreateAssetMenu(fileName = "WindPlaneData", menuName = "ScriptableObject/WindPlaneValue", order = 1)]
    public class WindPlaneScriptableObject: ScriptableObject
    {
        [Header("READ_ONLY")]
        [Header("Rotation values")]
        [SerializeField]
        private bool isWindPlaneMoving = false;
        [SerializeField]
        private bool isWindPlaneMovingLeft, isWindPlaneMovingRight;

        [Header("Move around")]
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

        public bool IsWindPlaneMoving
        {
            get => isWindPlaneMoving;
            set => isWindPlaneMoving = value;
        }
        public bool IsWindPlaneMovingLeft
        {
            get => isWindPlaneMovingLeft;
            set => isWindPlaneMovingLeft = value;
        }
        public bool IsWindPlaneMovingRight
        {
            get => isWindPlaneMovingRight;
            set => isWindPlaneMovingRight = value;
        }
        public float MinRotationSpeed
        {
            get => minRotationSpeed;
            set => minRotationSpeed = value;
        }
        public float MaxRotationSpeed
        {
            get => maxRotationSpeed;
            set => maxRotationSpeed = value;
        }
        public int WindPlaneMoveDuration
        {
            get => windPlaneMoveDuration;
            set => windPlaneMoveDuration = value;
        }
    }
}