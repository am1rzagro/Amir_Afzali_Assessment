using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject Root;

    private void Update()
    {
        Show(InputManager.Instance.ControllType == ControllType.Type_C);
    }
    public void Show(bool value) => Root.SetActive(value);
}
