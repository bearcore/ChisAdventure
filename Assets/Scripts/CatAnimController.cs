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
    private bool jiggle;
    private float jiggleTimeOut;
    public int JiggleAnimations = 3;

    private List<float> _wiggleStrength = new List<float>
    {
        0f, 0f, 0f, 0.25f, 0.5f, 1f, 1.5f
    };

    private List<float> _zoomStrength = new List<float>
    {
        1f, 1.1f, 1.3f, 1.5f, 1.75f, 2f, 2.5f
    };

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
        jiggleTimeOut += Time.deltaTime;

    }
    public int GetState()
    {
        return state;
    }
    /// <summary>
    /// Z?hlt den n?chsen State hoch
    /// </summary>
    /// <returns>Gibt den neuen State zur?ck</returns>
    public int NextState()
    {
        if(state < _wiggleStrength.Count)
        {
            Lerp.Delay(0.1f, () =>
            {
                if (state < _wiggleStrength.Count)
                {
                    CameraShake.SetShake(_wiggleStrength[state], 0);
                    CameraShake.SetZoom(_zoomStrength[state]);
                }
            });
            state++;
        }
        Debug.Log("State was set to" + state);
        return state;
    }
    public void Jiggle()
    {
        catAnimator.SetInteger("JiggleAnimationSelect", Random.Range(0, JiggleAnimations));
        catAnimator.SetTrigger("Jiggle");
        jiggleTimeOut = 0;
    }
    public void Release()
    {
        catAnimator.SetTrigger("Jump");
    }

    public void Land()
    {
        catAnimator.SetTrigger("Land");
    }
}