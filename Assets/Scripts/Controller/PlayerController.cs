using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface Player
{
    void Init(GameObject gameObject,float Speed,float RotateSpeed);
    void InternalUpdate();
}

public class PlayerController : MonoBehaviour
{
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
        public void InternalUpdate()
        {
            moveDirection = InputManager.Instance.MoveInput;
            MouseAxis = InputManager.Instance.AxisInput;

            transform.Translate(new Vector3(moveDirection.x * speed, 0, moveDirection.y * speed) * Time.deltaTime);
            transform.Rotate(Vector3.up, MouseAxis.x * rotateSpeed);
        }
    }
    public class PlayerTypeB : Player
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
        public void InternalUpdate()
        {
            moveDirection = InputManager.Instance.MoveInput;
            MouseAxis = InputManager.Instance.AxisInput;

            transform.Translate(new Vector3(moveDirection.x * speed, 0, moveDirection.y * speed) * Time.deltaTime);
            transform.Rotate(Vector3.up, MouseAxis.x * rotateSpeed);
        }
    }
    public class PlayerTypeC : Player
    {
        private float speed;
        private float rotateSpeed;
        private float rotateSpeedFactor = .2f;

        private Vector3 moveDirection;

        private Transform transform;

        public void Init(GameObject gameObject, float speed, float rotateSpeed)
        {
            this.speed = speed;
            this.rotateSpeed = rotateSpeed * rotateSpeedFactor;
            transform = gameObject.transform;
        }
        public void InternalUpdate()
        {
            moveDirection = InputManager.Instance.MoveInput;

            transform.Translate(new Vector3(0, 0, moveDirection.y * speed) * Time.deltaTime);
            transform.Rotate(Vector3.up, moveDirection.x * rotateSpeed);
        }
    }

    [SerializeField] private float Speed;
    [SerializeField] private float RotateSpeed;

    private List<Player> players = new List<Player>();

    void Start()
    {
        PlayerTypeA playerTypeA = new PlayerTypeA();
        PlayerTypeA playerTypeB = new PlayerTypeA();
        PlayerTypeC playerTypeC = new PlayerTypeC();

        players.Add(playerTypeA);
        players.Add(playerTypeB);
        players.Add(playerTypeC);

        foreach (Player itm in players)
            itm.Init(gameObject,Speed,RotateSpeed);
    }

    void Update()
    {
        switch (InputManager.Instance.ControllType)
        {
            case ControllType.Type_A:
                players[0].InternalUpdate();
                break;
            case ControllType.Type_B:
                players[1].InternalUpdate();
                break;
            case ControllType.Type_C:
                players[2].InternalUpdate();
                break;
        }
    }

}
