
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControllType { Type_A, Type_B, Type_C };
public delegate void OnChangeControllType(ControllType controllType);

public class InputManager : MonoBehaviour
{
    #region Singleton
    public static InputManager Instance;
    #endregion

    #region Private Field
    private PlayerActionMap playerActionMap;

    private Vector2 moveInput;
    private Vector2 axisInput;

    private Vector3 playerSelectedPosition;

    private ControllType controllType;

    private Transform playerTransform;
    #endregion

    #region Public Accsess
    public Vector2 MoveInput { get { return moveInput; } private set { moveInput = value; } }
    public Vector2 AxisInput { get { return axisInput; } private set { axisInput = value; } }
    public ControllType ControllType { get { return controllType; } private set { controllType = value; } }
    public Vector3 PlayerSelectedPosition {get { return playerSelectedPosition;} private set { playerSelectedPosition = value; } }
    public void SetPlayerTransform(Transform transform) => playerTransform = transform; 
    #endregion

    #region Mono Func
    private void Awake()
    {
        Instance = this;
        playerActionMap = new PlayerActionMap();
    }
    private void OnEnable() => playerActionMap.Enable();
    private void OnDisable() => playerActionMap.Disable();

    public event OnChangeControllType OnChange;

    void Update()
    {
        MoveInput = playerActionMap.Player.Move.ReadValue<Vector2>();
        AxisInput = playerActionMap.Player.Look.ReadValue<Vector2>();

        if (Keyboard.current[Key.Digit1].wasPressedThisFrame || Keyboard.current[Key.Numpad1].wasPressedThisFrame)
            controllType = ControllType.Type_A;
        if (Keyboard.current[Key.Digit2].wasPressedThisFrame || Keyboard.current[Key.Numpad2].wasPressedThisFrame)
            controllType = ControllType.Type_B;
        if (Keyboard.current[Key.Digit3].wasPressedThisFrame || Keyboard.current[Key.Numpad3].wasPressedThisFrame)
        controllType = ControllType.Type_C;

        OnChange += (ControllType type) => { type = controllType; };

        if (controllType == ControllType.Type_B && Mouse.current.leftButton.IsPressed())
            PlayerSelectedPosition = SelectedPosition();
    }

    // Get select position on world
    private Vector3 SelectedPosition()
    {
        if (playerTransform == null)
            return Vector3.zero;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return playerTransform.position;
    }
    #endregion
}
