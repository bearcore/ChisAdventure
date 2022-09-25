using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    public CatAnimController CatAnimController;
    public MoveOnTrunk MoveOnTrunk;
    public List<Transform> Targets;
    public AnimationCurve Curve;
    public float JumpDuration = 6f;

    public void JumpToTarget(int target)
    {
        if (target > 5) target = 5;

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
        });

    }
}
