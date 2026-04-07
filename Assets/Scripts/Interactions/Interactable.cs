using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : DebugMonoBehaviour
{
    [Space(20)]
    private Collider2D _collider;
    [Tooltip("Sent to the UI manager to display when this object is interactable")]
    [SerializeField] private string controlText = "[E] "; // TODO only have e in the text UI tags?
    public string interactionText;
    private bool isInteractable = false;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        if (_collider == null)
        {
            Debug.LogError(this.name + " requires a collider.");
        }
        _collider.isTrigger = true;
        customDebugStatementTypes.Add(new StatementType("collision"));
        InitializeInteractable();
    }

    /// <summary>
    /// Called by Start(). Use this for an Interactable instead of Start().
    /// </summary>
    protected virtual void InitializeInteractable()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateInteractable();
    }

    /// <summary>
    /// Called by FixedUpdate(). Use instead of Update() or FixedUpdate().
    /// </summary>
    protected virtual void UpdateInteractable()
    {

    }

    /// <summary>
    /// Called by the interaction manager whenever the player presses
    /// the interaction button and this.isInteractable is true.
    /// </summary>
    public virtual void Interaction()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInteractable = true;
            InteractionsManager.instance.AddInteractable(this);
            //UIManager.instance.SetControlsText(interactionText);
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInteractable = false;
            InteractionsManager.instance.RemoveInteractable(this);
            //UIManager.instance.SetControlsText("");
        }
    }
}