using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTrunk : MonoBehaviour
{
    public float MovementSpeed = 50f;
    public Vector3 MoveDirection = new Vector3(0f, 0f, -1f);
    private bool _moving = true;

    private void Start()
    {
        CatRotationStuff.OnFall.AddListener(() => _moving = false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_moving) return;
        transform.position += MoveDirection * Time.deltaTime * MovementSpeed;
    }
}
