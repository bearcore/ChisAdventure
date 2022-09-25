using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CatRotationStuff : MonoBehaviour
{
    public float WindStrength = 1;
    public float Speed = 0.3f;
    public float PressInfluence = 1f;
    public float Difficulty = 1f;
    public float DifficultyIncrease = 0.1f;

    public float Points;
    public TextMeshProUGUI PointsText;
    public GameObject RestartButton;
    public CanvasGroup PointsCanvasGroup;

    private float _initialDelay;

    private float z;
    private Vector3 _initialRotation;
    private bool _rotating = true;

    public static UnityEvent OnFall = new UnityEvent();

    private void OnEnable()
    {
        _initialRotation = transform.eulerAngles;
        Lerp.To(1f, t => PointsCanvasGroup.alpha = t);
    }

    public static UnityEvent OnLost = new UnityEvent();

    public List<KeyCode> LeftButtons = new List<KeyCode>
    {
        KeyCode.LeftArrow, KeyCode.A
    };
    public List<KeyCode> RightButtons = new List<KeyCode>
    {
        KeyCode.RightArrow, KeyCode.D
    };

    public void SetDifficulty(float difficulty)
    {
        Difficulty = difficulty;
    }

    private void Update()
    {
        if (_rotating)
        {
            HandleRotation();
        }
        else
        {
            Fall();
        }
    }

    private void HandleRotation()
    {
        Difficulty += DifficultyIncrease * Time.deltaTime;
        _initialDelay += Time.deltaTime / 2f;
        _initialDelay = Mathf.Clamp01(_initialDelay);

        var wind = Mathf.PerlinNoise(Time.time * Speed, Time.time * Speed) * 2 - 1;
        wind *= WindStrength * Difficulty * _initialDelay;
        z += wind * Time.deltaTime;

        if (LeftButtons.Any(x => Input.GetKey(x)))
        {
            z += PressInfluence * Time.deltaTime * Difficulty;
        }
        else if (RightButtons.Any(x => Input.GetKey(x)))
        {
            z -= PressInfluence * Time.deltaTime * Difficulty;
        }

        transform.eulerAngles = _initialRotation + new Vector3(0f, 0f, z);

        Points += (Difficulty * Difficulty) * Mathf.Abs(z) * 10 * Time.deltaTime;
        PointsText.text = ((int)Points).ToString();
        PointsText.transform.localScale = Vector3.one * Mathf.Clamp(1 + Points / 20000, 1, 2.5f);
        CameraShake.SetShake(Mathf.Abs(z) / 90f, 0);

        if (Mathf.Abs(z) > 90)
        {
            OnLost.Invoke();
            _rotating = false;
            var oldPos = new GameObject();
            oldPos.transform.position = transform.position;
            CameraShake.SetFollowTarget(oldPos.transform);
            OnFall.Invoke();

            var initialZ = transform.eulerAngles.z;
            Lerp.FromTo(1f, t => transform.eulerAngles = _initialRotation + new Vector3(0f, 0f, t), initialZ, 0f);
            CameraShake.SetShake(0f, 0);
            RestartButton.SetActive(true);
        }
    }

    private void Fall()
    {
        if (transform.position.y <= 0) return;
        transform.position = transform.position + Vector3.down * Time.deltaTime * 20f;
        transform.position = transform.position + Vector3.right * Time.deltaTime * 2f;
    }
}
