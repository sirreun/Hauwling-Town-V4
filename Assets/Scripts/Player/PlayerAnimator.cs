using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _renderer;
    private enum Direction { Left, Right }

    private Direction direction = Direction.Left;
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

    public void Idle()
    {
	    switch (direction)
        {
            case Direction.Left:
                state = idleLeft;
                break;
            case Direction.Right:
                state = idleRight;
                break;
        }
    }

    public void WalkingLeft()
    {
        state = walkLeft;
        direction = Direction.Left;
    }

    public void WalkingRight()
    {
        state = walkRight;
        direction = Direction.Right;
    }


    #region Cached Properties

    private int _currentState;
	
    private static readonly int idleLeft = Animator.StringToHash("Idle_Left");
    private static readonly int idleRight = Animator.StringToHash("Idle_Right");
    private static readonly int walkLeft = Animator.StringToHash("Walk_Left");
    private static readonly int walkRight = Animator.StringToHash("Walk_Right");

    #endregion
}
