using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : DebugMonoBehaviour, IDataPersistence
{
    public Dictionary<string, (string, int)> Items = new Dictionary<string, (string, int)>();
    //public Dictionary<string, string> ItemNameLookUp = new Dictionary<string, string>();
	public static InventoryManager instance { get; private set; }

	void Awake()
	{
    	/// Creates the instance for the manager if there isn't already one
    	if (instance != null)
    	{
        	Debug.LogWarning("InventoryManager.cs: found more than one manager in the scene, the newest manager will be destroyed");
        	Destroy(this.gameObject);
        	return;
    	}
    	instance = this;
    	DontDestroyOnLoad(this.gameObject);
	}

    // DATA PERSISTENCE
    // data being handled:
    // > Items, into and from InventoryItem? or just keep dict
    public void LoadData()//GameData data)
    {
        //Items = data.Items;
    }

    public void SaveData()//GameData data)
    {
        //data.Items = Items;
    }

    // PUBLIC FUNCTIONS
    public void AddItem(Item item, int amount = 1)
    {
        (string, int) value;
        if (Items.TryGetValue(item.ID, out value))
        {
            Items[item.ID] = (value.Item1, value.Item2 + amount);
        }
        else
        {
            Items.Add(item.ID, (item.Name, amount));
        }

        Print("Added " + item.Name + " to inventory");
    }

    public void RemoveItem(Item item, int amount = 1)
    {
        (string, int) value = ("", 0);
        if (Items.TryGetValue(item.ID, out value))
        {
            if (value.Item2 - amount <= 0)
            {
                Items.Remove(item.ID);
            }
            else
            {
                Items[item.ID] = (value.Item1, value.Item2 - amount);
            }
        }
        else
        {
            Debug.LogWarning("InventoryManager: " + item.ID + ": " + item.Name + " not in inventory.");
        }
    }

    public bool InInventory(Item item)
    {
        if (!Items.ContainsKey(item.ID))
        {
            return false;
        }

        return true;
    }

    public void OpenUI()
    {
        PlayerManager.instance.ChangeMode(PlayerManager.PlayerMode.Inventory);

        UIManager.instance.OpenWindow(UIWindow.Inventory);
    }

    public void CloseUI()
    {
        PlayerManager.instance.ChangeMode(PlayerManager.PlayerMode.Interact);

        UIManager.instance.CloseWindow(UIWindow.Inventory);
    }

    public List<(string, int)> GetInventoryItems(InventoryTab tab)
    {
        List<(string, int)> output = new List<(string, int)>();
        switch (tab)
        {
            case InventoryTab.Items:
                output = Items.Values.ToList<(string,int)>();
                break;
            default:
                Debug.LogWarning("InventoryManager: GetInventoryItems(): Other tab does not have get inventory functionality");
                break;
        }

        return output;
    }

}
