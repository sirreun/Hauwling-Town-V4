using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : DebugMonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private InventoryUIAnimator inventoryUIAnimator;
    [SerializeField] private InventoryTabAnimator inventoryTabAnimator;
    public static UIManager instance { get; private set; }

	void Awake()
	{
    	/// Creates the instance for the manager if there isn't already one
    	if (instance != null)
    	{
        	Debug.LogWarning("UIManager.cs: found more than one manager in the scene, the newest manager will be destroyed");
        	Destroy(this.gameObject);
        	return;
    	}
    	instance = this;
    	DontDestroyOnLoad(this.gameObject);
	}

    // PUBLIC FUNCTIONS
    public void OpenWindow(UIWindow window)
    {
        switch (window)
        {
            case UIWindow.Inventory:
                inventoryUIAnimator.Open();
                break;
            case UIWindow.Journal:

                break;
            case UIWindow.PauseMenu:

                break;
        }
    }

    public void CloseWindow(UIWindow window)
    {
        switch (window)
        {
            case UIWindow.Inventory:
                inventoryUIAnimator.Closed();
                break;
            case UIWindow.Journal:

                break;
            case UIWindow.PauseMenu:

                break;
        }
    }

    public void NavigateTab(UIWindow window, float direction)
    {
        switch (window)
        {
            case UIWindow.Inventory:
                inventoryTabAnimator.ChangeTab((int)direction);
                break;
            case UIWindow.Journal:

                break;
            case UIWindow.PauseMenu:

                break;
        }
    }
}

public enum InventoryTab
{
    Items,
    PhoneNumbers,
    Keys,
    IDs
}

public enum UIWindow
{
    Inventory,
    Journal,
    PauseMenu
}
