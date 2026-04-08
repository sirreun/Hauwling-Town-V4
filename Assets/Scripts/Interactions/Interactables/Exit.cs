using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : Interactable
{
    [SerializeField] private Location toLocation;

    // Called before the first frame.
    protected override void InitializeInteractable()
    {
        
    }

    // Called once per frame.
    protected override void UpdateInteractable()
    {
        
    }

    // Called by the InteractionManager when isInteractable (inherited) is true.
    public override void Interaction()
    {
        // call scene loader
	    // go to Location >> open toLocation.SceneName if not already,
        // then move player to toLocation.SpawnPosition
    }
}