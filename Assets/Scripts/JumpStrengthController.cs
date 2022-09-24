using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStrengthController : MonoBehaviour
{
    public CatAnimController CatAnimController;
    private int state;
    private int old_state;
    private float deltaTimeSum;
    private bool running;
    [SerializeField] [Range(0f,3f)] float pitchSpeed;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        old_state = 0;
        running = false;

    }

    // Update is called once per frame
    void Update()
    {
        deltaTimeSum += Time.deltaTime * pitchSpeed;

        if (deltaTimeSum % 4 > 3.5f && !running)
        {
            CatAnimController.Jiggle();
            
        }

        if (Input.GetKey("space"))
        {
            if(!running)
            {
                deltaTimeSum = 1;
                running = true;
            }
            //  Debug.Log(state);
            state = (int)deltaTimeSum;
            if (state != old_state)
            {
                CatAnimController.NextState();
                old_state = state;
            }
           

        }
        if (Input.GetKeyDown("space") )
        {
 
           
            

        }
        if (Input.GetKeyUp("space"))
        {
            CatAnimController.Release();
            this.enabled = false;
        }

    }
}
