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
        //https://stackoverflow.com/questions/66811360/c-sharp-unity-wait-for-function-to-be-called
        //TODO destory gameobject after itterable in inventory manager uis done
    }

    public void PickUpItem()
    {
        InventoryManager.instance.AddItem(this);
        Destroy(this.gameObject);
    }
}