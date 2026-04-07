using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIAnimator : MonoBehaviour
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

    public void Closed()
    {
        state = closed;
    }

    public void Open()
    {
        state = open;
    }

    #region Cached Properties

    private int _currentState;
	
    private static readonly int closed = Animator.StringToHash("Closed");
    private static readonly int open = Animator.StringToHash("Open");
    #endregion
}
