using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleAnimationController : MonoBehaviour
{
    [Tooltip("动画类型")]
    public int animationType = 0;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void OnToggleChanged(bool isOn)
    {
        Debug.Log("Toggle changed: " + isOn);
        if (isOn)
        {
            anim.SetInteger("Dance", animationType);
        }
        else
        {
            anim.SetInteger("Dance", 0);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
