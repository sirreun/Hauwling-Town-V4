using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : DebugMonoBehaviour
{
    [Header("Inventory UI")]
    [SerializeField] private InventoryUIAnimator inventoryUIAnimator;
    [SerializeField] private InventoryTabAnimator inventoryTabAnimator;
    [SerializeField] private TextMeshProUGUI inventoryHolderText;
    private InventoryTab currentTab = InventoryTab.Items;

    [Space(10)]
    [Header("Interactions UI")]
    [SerializeField] private InteractionTagAnimator interactionTagAnimator;
    [SerializeField] private TextMeshPro[] interactionOptionTexts;
    public int CurrentInteractableIndex = 0;
    private int currentOptionText = 0;
    private const int numberOfOptionTexts = 3;

    public static UIManager instance { get; private set; }

    //TODO: For changign scenes and not breaking stuff, might have to make UIManager and PlayerMaanger not DDOL
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

    void Start()
    {

    }

    // INVENTORY FUNCTIONS
    private void UpdateInventoryUI()
    {
        inventoryHolderText.text = "";
        List<(string, int)> items = InventoryManager.instance.GetInventoryItems(currentTab);

        foreach ((string, int) itemTuple in items)
        {
            inventoryHolderText.text += itemTuple.Item1 + " - " + itemTuple.Item2.ToString() + "\n";
        }
    }

    // PUBLIC FUNCTIONS
    public void OpenWindow(UIWindow window)
    {
        switch (window)
        {
            case UIWindow.Inventory:
                Print("Opening inventory", "inventory");
                UpdateInventoryUI();
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
                Print("Closing Inventory", "inventory");
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
                currentTab = inventoryTabAnimator.ChangeTab((int)direction);
                UpdateInventoryUI();
                break;
            case UIWindow.Journal:

                break;
            case UIWindow.PauseMenu:

                break;
        }
    }

    /// <summary>
    /// Updates the interaction options text to match interactables available to the Player.
    /// </summary>
    public void UpdateInteractionsText()
    {
        int totalInteractables = InteractionsManager.instance.Interactables.Count;
        int interactableIndex = CurrentInteractableIndex;
        for (int i = 0; i < numberOfOptionTexts; i++)
        {
            string updatedText = "";
            if ((interactableIndex + i) < totalInteractables && totalInteractables > 0)
            {
                // In index range for InteractionsManager.instance.Interactables
                updatedText = InteractionsManager.instance.Interactables[interactableIndex + i].interactionText;
            }
            interactionOptionTexts[i].text = updatedText;
        }
    }

    /// <summary>
    /// Updates interaction options text when navigated, and determines if a highlight should 
    /// be changed or text needs to be updated.
    /// </summary>
    /// <param name="direction"></param>
    public void NavigateInteractionOptions(int direction)
    {
        int numberOfInteractables = InteractionsManager.instance.Interactables.Count;
        if (numberOfInteractables < 1)
        {
            interactionTagAnimator.Highlight(0);
            return;
        }

        int numberOfOptions = Mathf.Min(numberOfOptionTexts, numberOfInteractables);

        if ((currentOptionText + direction) > (numberOfOptions - 1))
        {
            // check if changes highlight, or changes texts
            if (CurrentInteractableIndex == numberOfInteractables - 1)
            {
                Print("current interactblae index == max", "interactions");
                currentOptionText = 0;
                CurrentInteractableIndex = currentOptionText;
                interactionTagAnimator.Highlight(currentOptionText);
            }
            else
            {
                Print("current interactblae index is under max", "interactions");
                CurrentInteractableIndex++;
                UpdateInteractionsText();
            }
        }
        else if ((currentOptionText + direction) < 0)
        {
            // check if changes highlight, or changes texts
            if (CurrentInteractableIndex == 0)
            {
                currentOptionText = numberOfOptions - 1;
                CurrentInteractableIndex = currentOptionText;
                interactionTagAnimator.Highlight(currentOptionText);
            }
            else
            {
                CurrentInteractableIndex--;
                UpdateInteractionsText();
            }
        }
        else
        {
            currentOptionText += direction;
            CurrentInteractableIndex += direction;

            interactionTagAnimator.Highlight(currentOptionText);
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
