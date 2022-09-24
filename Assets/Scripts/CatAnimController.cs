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
        jiggleTimeOut  += Time.deltaTime;
        if (jiggleTimeOut > 1)
        {
            catAnimator.SetBool("Jiggle", false);
        }

    }
    public int GetState()
    {
        return state;
    }
    /// <summary>
    /// Zählt den nächsen State hoch
    /// </summary>
    /// <returns>Gibt den neuen State zurück</returns>
    public int NextState()
    {
        state++;
        Debug.Log("State was set to" + state);
        return state;
    }
    public void Jiggle()
    {
        if (!catAnimator.GetBool("Jiggle"))
        {
            catAnimator.SetBool("Jiggle", true);
            //Debug.Log("Jiggle");
            jiggleTimeOut = 0;
        }
    }
    public void Release()
    {
        catAnimator.SetTrigger("Jump");
    }
}