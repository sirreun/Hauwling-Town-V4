using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string ID;
    public string Name;

    public InventoryItem(string id, string name)
    {
        // make GUID thingy from that one tutorial
        ID = id; // TODO : change
        Name = name;
    }
}