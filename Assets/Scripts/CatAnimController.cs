using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatAnimController : MonoBehaviour
{   /// <summary>
/// 0 = init(glas wigle) , 1 = idle , 2 = prepare/sit
/// </summary>
    private int state;
    private Animator catAnimator;
    // Start is called before the first frame update
    void Start()
    {
        catAnimator = GetComponent<Animator>();
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        catAnimator.SetInteger("State", state);
        
        
    }
    public int GetState()
    {
        return state;
    }
    public void SetState(int state)
    {
        this.state = state;
        Debug.Log("State was set to" + state);
    }
}