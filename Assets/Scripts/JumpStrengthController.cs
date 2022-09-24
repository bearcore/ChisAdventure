using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStrengthController : MonoBehaviour
{
    public CatAnimController CatAnimController;
    private int state;
    private int old_state;
    private float deltaTimeSum;
    [SerializeField] [Range(0f,3f)] float pitchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //state = 0;
    
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKey("space"))
        {
            deltaTimeSum += Time.deltaTime*pitchSpeed;
            state = (int)deltaTimeSum;
          //  Debug.Log(state);
            if (state != old_state)
            {
                CatAnimController.SetState(state);
                old_state = state;
            }
            
            
        }
        if (Input.GetKeyDown("space"))
        {
            deltaTimeSum = 0;
            state = 0;
            CatAnimController.SetState(state);
        }
        if (Input.GetKeyUp("space"))
        {
            CatAnimController.SetState(state);
        }

    }
}
