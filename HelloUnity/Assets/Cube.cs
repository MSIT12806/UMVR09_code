using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Cube : MonoBehaviour
{
    // Start is called before the first frame update

    Animator _animator;
    bool down;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo asi = _animator.GetCurrentAnimatorStateInfo(0);
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
            _animator.SetTrigger("Click Down");
        }
        else if(Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (asi.IsName("CubeDown"))
            {
                Debug.Log("HAHAHA");
            }
            _animator.SetTrigger("End Down");
        }
    }
    void OnKeyDown(KeyDownEvent ev)
    {
        Debug.Log("KeyDown:" + ev.keyCode);
        Debug.Log("KeyDown:" + ev.character);
        Debug.Log("KeyDown:" + ev.modifiers);
    }

    void OnKeyUp(KeyUpEvent ev)
    {
        Debug.Log("KeyUp:" + ev.keyCode);
        Debug.Log("KeyUp:" + ev.character);
        Debug.Log("KeyUp:" + ev.modifiers);
    }
}
