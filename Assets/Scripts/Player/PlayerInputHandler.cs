using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{

    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string ActionMapName = "Player";

    [Header("Action Name Reference")]
    [SerializeField] private string Move = "Move";
    [SerializeField] private string Dash = "Dash";
    [SerializeField] private string Parry = "Parry";
    [SerializeField] private string Attack = "Attack";

    private InputAction MoveAction;
    private InputAction DashAction;
    private InputAction ParryAction;
    private InputAction AttackAction;

    public Vector2 MoveInput { get; private set; }
    public bool DashTriggered { get; private set; }
    public bool ParryTriggered { get; private set; }
    public bool AttackTriggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        MoveAction = playerControls.FindActionMap(ActionMapName).FindAction(Move);
        DashAction = playerControls.FindActionMap(ActionMapName).FindAction(Dash);
        ParryAction = playerControls.FindActionMap(ActionMapName).FindAction(Parry);
        AttackAction = playerControls.FindActionMap(ActionMapName).FindAction(Attack);
        RegisterInputActions();

    }

    void RegisterInputActions()
    {
        MoveAction.performed += context => MoveInput = context.ReadValue<Vector2>();
        MoveAction.canceled += context => MoveInput = Vector2.zero;

        DashAction.performed += Context => DashTriggered = true;
        DashAction.canceled += Context => DashTriggered = false;

        ParryAction.performed += Context => ParryTriggered = true;
        ParryAction.canceled += Context => ParryTriggered = false;

        AttackAction.performed += Context => AttackTriggered = true;
        AttackAction.canceled += Context => AttackTriggered = false;
    }

    private void OnEnable()
    {
        MoveAction.Enable();
        DashAction.Enable();
        ParryAction.Enable();
        AttackAction.Enable();
    }

    private void OnDisable()
    {
        MoveAction.Disable();
        DashAction.Disable();
        ParryAction.Disable();
        AttackAction.Disable();
    }
}
