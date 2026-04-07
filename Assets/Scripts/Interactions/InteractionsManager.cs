using System.Collections;
using System.Collections.Generic;
using UnityEditor.Toolbars;
using UnityEngine;

public class InteractionsManager : DebugMonoBehaviour
{
    [Tooltip("Holds all interactables that have isInteractable set to true")]
    [Space(10)]
    public List<Interactable> Interactables = new List<Interactable>();
    private int selectedInteractable = -1;

    public static InteractionsManager instance { get; private set; }

	void Awake()
	{
    	/// Creates the instance for the manager if there isn't already one
    	if (instance != null)
    	{
        	Debug.LogWarning("InteractionsManager.cs: found more than one manager in the scene, the newest manager will be destroyed");
        	Destroy(this.gameObject);
        	return;
    	}
    	instance = this;
    	DontDestroyOnLoad(this.gameObject);
	}

    private void FixedUpdate()
    {
        if (Interactables.Count > 0)
        {
            // TODO: update interactables UI?
        }
    }

    public void AddInteractable(Interactable interactable)
    {
        Interactables.Add(interactable);
        // TODO: update interactables UI
        UIManager.instance.UpdateInteractionsText();
    }

    public void RemoveInteractable(Interactable interactable)
    {
        Interactables.Remove(interactable);
        // TODO: update interactables UI
        UIManager.instance.UpdateInteractionsText();
    }

    // TODO : make sure only the selected interactable in UI is interacted with
    /// <summary>
    /// Called by PlayerManager when interact is pressed. Calls Interaction
    /// for each Interactable that has isInteractable set to true.
    /// </summary>
    public void HandleInteractions()
    {
        int index = UIManager.instance.CurrentInteractableIndex;
        Interactables[index].Interaction();

    }
}
