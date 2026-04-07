using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTagAnimator : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _renderer;

    public int state;

    void Awake()
    {
        _anim = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == _currentState) return;
        _anim.CrossFade(state, 0, 0);
        _currentState = state;
    }

    public void Highlight(int index)
    {
	    switch (index)
        {
            case 0:
                state = highlightZero;
                break;
            case 1:
                state = highlightOne;
                break;
            case 2:
                state = highlightTwo;
                break;
        }
    }


    #region Cached Properties

    private int _currentState;
	
    private static readonly int highlightZero = Animator.StringToHash("HighlightZero");
    private static readonly int highlightOne = Animator.StringToHash("HighlightOne");
    private static readonly int highlightTwo = Animator.StringToHash("HighlightTwo");
    #endregion
}
