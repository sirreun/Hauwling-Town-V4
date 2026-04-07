using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionsManager : DebugMonoBehaviour
{
    [Tooltip("Holds all interactables that have isInteractable set to true")]
    [Space(10)]
    public List<Interactable> interactables = new List<Interactable>();
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
        if (interactables.Count > 0)
        {
            // Show UI list
            //
        }
    }

    public void AddInteractable(Interactable interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Interactable interactable)
    {
        interactables.Remove(interactable);
    }

    // TODO : make sure only the selected interactable in UI is interacted with
    /// <summary>
    /// Called by PlayerManager when interact is pressed. Calls Interaction
    /// for each Interactable that has isInteractable set to true.
    /// </summary>
    public void HandleInteractions()
    {
        for (int i = 0; i < interactables.Count; i++)
        {
            interactables[i].Interaction();
        }
    }
}
