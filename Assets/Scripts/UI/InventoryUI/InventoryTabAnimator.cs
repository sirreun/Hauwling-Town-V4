using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTabAnimator : MonoBehaviour
{
    private Animator _anim;
    private SpriteRenderer _renderer;

    public int state;
    private int stateIndex = 0;
    private const int numberOfTabs = 4;

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

    public InventoryTab ChangeTab(int direction)
    {
        if ((stateIndex + direction) > (numberOfTabs - 1))
        {
            stateIndex = 0;
        }
        else if ((stateIndex + direction) < 0)
        {
            stateIndex = numberOfTabs - 1;
        }
        else
        {
            stateIndex = stateIndex + direction;
        }

        switch (stateIndex)
        {
            case 0:
                state = items;
                return InventoryTab.Items;
            case 1:
                state = phoneNumbers;
                return InventoryTab.PhoneNumbers;
            case 2:
                state = keys;
                return InventoryTab.Keys;
            case 3:
                state = ids;
                return InventoryTab.IDs;
            default:
                state = items;
                return InventoryTab.Items;
        }
    }

    #region Cached Properties

    private int _currentState;
	
    private static readonly int items = Animator.StringToHash("Items");
    private static readonly int phoneNumbers = Animator.StringToHash("PhoneNumbers");
    private static readonly int keys = Animator.StringToHash("Keys");
    private static readonly int ids = Animator.StringToHash("IDs");
    #endregion
}
