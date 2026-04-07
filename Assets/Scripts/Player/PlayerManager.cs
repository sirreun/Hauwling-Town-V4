using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : DebugMonoBehaviour, IDataPersistence
{
    private Rigidbody2D _rigidbody;
    public float Horizontal { get; private set; } // used for movement
    public float Speed = 7f;
    
    public PlayerAnimator PlayerAnimator;
    private enum InputMap
    {
        Player,
        UI
    }
    private InputMap inputMap = InputMap.Player;
    private PlayerInput playerInput;
    public enum PlayerMode
    {
        Interact,
        Dialogue,
        Inventory,
        Journal,
        Cutscene,
        Pause,
        Menu
    }
    public PlayerMode playerMode { get; private set; }

    public static PlayerManager instance { get; private set; }

    // DATA PERSISTENCE
    // data being handled:
    // > player
    public void LoadData()//GameData data)
    {
        //transform.position = data.PlayerPosition;
    }

    public void SaveData()//GameData data)
    {
        //data.PlayerPosition = transform.position;
    }

    void Awake()
	{
    	/// Creates the instance for the manager if there isn't already one
    	if (instance != null)
    	{
        	Debug.LogWarning("PlayerManager.cs: found more than one manager in the scene, the newest manager will be destroyed");
        	Destroy(this.gameObject);
        	return;
    	}
    	instance = this;
    	DontDestroyOnLoad(this.gameObject);
	}

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        ChangeMode(PlayerMode.Interact); //TODO: this should be UI for main menu, sceneloadermanager will need to change this to interact
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        _rigidbody.linearVelocity = new Vector2(Horizontal * Speed, _rigidbody.linearVelocityY);

        if (Horizontal > 0)
        {
            PlayerAnimator.WalkingRight();
        }
        else if (Horizontal < 0)
        {
            PlayerAnimator.WalkingLeft();
        }
        else
        {
            PlayerAnimator.Idle();
        }
    }
    
    private void UpdateInputMap()
    {
        switch (playerMode)
        {
            case PlayerMode.Interact:
                inputMap = InputMap.Player;
                playerInput.SwitchCurrentActionMap("Player");
                break;
            case PlayerMode.Cutscene:
                // maybe change
            default:
                inputMap = InputMap.UI;
                playerInput.SwitchCurrentActionMap("UI");
                break;
        }
    }

    // INPUT FUNCTIONS
    public void OnMove(InputAction.CallbackContext context)
    {
        Horizontal = context.ReadValue<Vector2>().x;
    }

    public void OnInteractionNavigate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().y;
        if (value != 0)
        {
            UIManager.instance.NavigateInteractionOptions((int)value);
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Print("Pressed E", "interact");
            InteractionsManager.instance.HandleInteractions();
        }
    }

    public void OnPressInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Print("Pressed I", "inventory");
            switch (playerMode)
            {
                case PlayerMode.Inventory:
                    InventoryManager.instance.CloseUI();
                    break;
                case PlayerMode.Pause:
                case PlayerMode.Menu:
                    break;
                default:
                    InventoryManager.instance.OpenUI();
                    break;
            }
        }
    }

    public void OnTabNavigate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<Vector2>().x;
        UIManager.instance.NavigateTab(UIWindow.Inventory, value);
    }

    // PUBLIC FUNCTIONS
    public void ChangeMode(PlayerMode mode)
    {
        playerMode = mode;
        UpdateInputMap();
    }
}
