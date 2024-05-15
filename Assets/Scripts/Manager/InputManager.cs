
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControllType { Type_A, Type_B, Type_C };
public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance;
    #endregion

    #region Private Field
    private PlayerActionMap playerActionMap;

    private Vector2 moveInput;
    private Vector2 axisInput;

    [SerializeField] private ControllType controllType;
    #endregion

    #region Public Accsess
    public Vector2 MoveInput { get { return moveInput; } private set { moveInput = value; } }
    public Vector2 AxisInput { get { return axisInput; } private set { axisInput = value; } }
    public ControllType ControllType { get { return controllType; } private set { controllType = value; } }
    #endregion

    #region Mono Func
    private void Awake()
    {
        Instance = this;
        playerActionMap = new PlayerActionMap();
    }
    private void OnEnable() => playerActionMap.Enable();

    private void OnDisable() => playerActionMap.Disable();
    void Update()
    {
        MoveInput = playerActionMap.Player.Move.ReadValue<Vector2>();
        AxisInput = playerActionMap.Player.Look.ReadValue<Vector2>();
    }
    #endregion
}
