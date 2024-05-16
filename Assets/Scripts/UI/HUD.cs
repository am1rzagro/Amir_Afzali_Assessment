using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject Root;
    [SerializeField] private Text[] typeTexts;

    [SerializeField] private Color selectColor = Color.white;
    [SerializeField] private Color unselectColor = Color.black;

    private int selectSize = 80;
    private int unselectSize = 50;

    private void Update()
    {
        ActiveText((int)InputManager.Instance.ControllType);
        Show(InputManager.Instance.ControllType == ControllType.Type_C);
    }
    private void Show(bool value) => Root.SetActive(value);
    private void ActiveText(int Index)
    {
        for (int i = 0; i< typeTexts.Length;i++)
        {
            if (i == Index)
            {
                typeTexts[i].color = selectColor;
                typeTexts[i].fontSize = selectSize;
                continue;
            }
            typeTexts[i].color = unselectColor;
            typeTexts[i].fontSize = unselectSize;
        }
    }
}
