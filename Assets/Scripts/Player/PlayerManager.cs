using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : DebugMonoBehaviour, IDataPersistence
{
    private Rigidbody2D _rigidbody;
    public float Horizontal { get; private set; } // used for movement and ui navigating
    public float Vertical { get; private set; } // used for movement and ui navigating
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
        //For debug purposes (in main menu, would be ui probably)
        playerMode = PlayerMode.Interact;
        playerInput.SwitchCurrentActionMap("Player");
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
        Vertical = context.ReadValue<Vector2>().y;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Print("Pressed E", "interact");
            InteractionsManager.instance.HandleInteractions();
        }
    }

    //TODO: bug, need to press inventory twice for it to open the first time
    public void OnPressInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
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

    public void TabNavigate(InputAction.CallbackContext context)
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
