using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YBot_Controller : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            animator.SetInteger("AnimationState", 0);//Idle
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("AnimationState", 1);//Walk
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("AnimationState", 2);//Punch
        }
    }
}
