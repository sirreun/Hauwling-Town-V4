using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderAnimator : MonoBehaviour
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

    public void FadeOut()
    {
	    state = fadeOut;
    }

    public void FadeIn()
    {
        state = fadeIn;
    }

    #region Cached Properties

    private int _currentState;
	
    private static readonly int fadeOut = Animator.StringToHash("FadeOut");
    private static readonly int fadeIn = Animator.StringToHash("FadeIn");
    #endregion
}
