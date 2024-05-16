using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface Player
{
    void Init(GameObject gameObject,float Speed,float RotateSpeed);
    void Movment();
}

public class PlayerController : MonoBehaviour
{
    // player PlayerType = key numbers 1,2,3
    public class PlayerTypeA : Player
    {
        private float speed;
        private float rotateSpeed;

        private Vector3 moveDirection;
        private Vector3 MouseAxis;

        private Transform transform;

        public void Init(GameObject gameObject, float speed, float rotateSpeed)
        {
            this.speed = speed;
            this.rotateSpeed = rotateSpeed;
            transform = gameObject.transform;
        }
        public void Movment()
        {
            moveDirection = InputManager.Instance.MoveInput;
            MouseAxis = InputManager.Instance.AxisInput;

            transform.Translate(new Vector3(moveDirection.x * speed, 0, moveDirection.y * speed) * Time.deltaTime);
            transform.Rotate(Vector3.up, MouseAxis.x * rotateSpeed);
        }
    }
    public class PlayerTypeB : Player
    {
        private Transform selectPosObject;
        private float speed;
        private float rotateSpeed;
        private const float maxDisOfTarget = 1; 

        private Transform transform;

        private Vector3 playerSelectedPosition;

        public void SetSelectPosObject(Transform posSle) => selectPosObject = posSle;

        public void Init(GameObject gameObject, float speed, float rotateSpeed)
        {
            this.speed = speed;
            this.rotateSpeed = rotateSpeed * 10;
            transform = gameObject.transform;
        }
        public void Movment()
        {
            playerSelectedPosition = InputManager.Instance.PlayerSelectedPosition;
            playerSelectedPosition.y = 0;
            transform.position = Vector3.MoveTowards(transform.position,playerSelectedPosition,Time.deltaTime * speed);

            var disOfTargt = Vector3.Distance(transform.position, playerSelectedPosition);
            if (disOfTargt <= maxDisOfTarget)
                return;

            Vector3 relativePos = playerSelectedPosition - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation,rotation,Time.deltaTime * rotateSpeed);

            selectPosObject.position = playerSelectedPosition;
        }

    }
    public class PlayerTypeC : Player
    {
        private float speed;
        private float rotateSpeed;
        private float rotateSpeedFactor = .5f;

        private Vector3 moveDirection;

        private Transform transform;

        public void Init(GameObject gameObject, float speed, float rotateSpeed)
        {
            this.speed = speed;
            this.rotateSpeed = rotateSpeed * rotateSpeedFactor;
            transform = gameObject.transform;
        }
        public void Movment()
        {
            moveDirection = InputManager.Instance.MoveInput;

            transform.Translate(new Vector3(0, 0, moveDirection.y * speed) * Time.deltaTime);
            transform.Rotate(Vector3.up, moveDirection.x * rotateSpeed );
        }
    }

    [SerializeField] private float Speed;
    [SerializeField] private float RotateSpeed;

    [SerializeField] private Transform selectPosObject;

    private List<Player> players = new List<Player>();

    void Start()
    {
        PlayerTypeA playerTypeA = new PlayerTypeA();
        PlayerTypeB playerTypeB = new PlayerTypeB();
        PlayerTypeC playerTypeC = new PlayerTypeC();

        players.Add(playerTypeA);
        players.Add(playerTypeB);
        players.Add(playerTypeC);

        foreach (Player itm in players)
            itm.Init(gameObject,Speed,RotateSpeed);

        InputManager.Instance.SetPlayerTransform(transform);
        playerTypeB.SetSelectPosObject(selectPosObject);
    }

    void Update()
    {
        switch (InputManager.Instance.ControllType)
        {
            case ControllType.Type_A:
                players[0].Movment();
                break;
            case ControllType.Type_B:
                players[1].Movment();
                break;
            case ControllType.Type_C:
                players[2].Movment();
                break;
        }
        selectPosObject.gameObject.SetActive(InputManager.Instance.ControllType == ControllType.Type_B);
    }

}
