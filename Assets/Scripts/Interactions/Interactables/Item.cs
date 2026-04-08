using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Interactable
{
    public string ID;
    public string Name;
    public GameObject Sprite;
    // Called before the first frame.
    protected override void InitializeInteractable()
    {
        //interactionText = Name; TODO
    }

    // Called once per frame.
    protected override void UpdateInteractable()
    {
        
    }

    // Called by the InteractionManager when isInteractable (inherited) is true.
    public override void Interaction()
    {
        PickUpItem();
    }

    public void PickUpItem()
    {
        InventoryManager.instance.AddItem(this);
        Destroy(GetComponent<Collider2D>()); // Remove so as not to confuse interactionsmanager
        InteractionsManager.instance.RemoveInteractable(this);
        Destroy(this.gameObject);
    }
}