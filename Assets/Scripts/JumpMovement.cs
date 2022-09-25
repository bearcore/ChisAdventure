using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    public CatAnimController CatAnimController;
    public MoveOnTrunk MoveOnTrunk;
    public CatRotationStuff CatRotation;
    public CatSounds Sounds;
    public List<Transform> Targets;
    public AnimationCurve Curve;
    public float JumpDuration = 6f;
    public GameObject LaunchClip;

    public List<float> Difficulties = new List<float>
    {
        0.7f, 0.9f, 1f, 1.1f, 1.2f, 1.3f
    };

    public void JumpToTarget(int target)
    {
        if (target > 5) target = 5;

        Sounds.PlayJumpSound();
        if(target > 3)
        LaunchClip.SetActive(true);

        var initialPos = transform.position;
        Lerp.To(JumpDuration, t =>
        {
            var e = Curve.Evaluate(t);
            transform.position = Vector3.Lerp(initialPos, Targets[target].position - new Vector3(0f,0f, 10), e);
            transform.eulerAngles = Vector3.Lerp(Vector3.zero, new Vector3(0f, 180f), e);
        });
        Lerp.Delay(JumpDuration, () =>
        {
            CatAnimController.Land();
            CameraShake.SetShake(0f);
            CameraShake.SetFollowTarget(transform);
            MoveOnTrunk.enabled = true;
            CatRotation.enabled = true;
            CatRotation.SetDifficulty(Difficulties[target]);
        });

    }
}
