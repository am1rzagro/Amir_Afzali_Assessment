using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [System.Serializable]
    public struct Setting
    {
        public float followSpeed;
        public float distance;
        public float hight;
    }

    [SerializeField] private Setting SettingTypeA;
    [SerializeField] private Setting SettingTypeB;
    [SerializeField] private Setting SettingTypeC;

    private Setting currentSetting;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform PlayerTransform;



    void LateUpdate()
    {
        followPlayer();
        cameraRotate();
        camraLook();

        ChangeCameraSetting(InputManager.Instance.ControllType);
    }

    private void followPlayer()
    {
        var target = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z);
        transform.position = Vector3.Lerp(transform.position,target,Time.deltaTime * currentSetting.followSpeed);

        camera.transform.localPosition = (Vector3.forward * (-currentSetting.distance)) + (Vector3.up * currentSetting.hight);
    }
    private void cameraRotate()
    {
        transform.eulerAngles = PlayerTransform.eulerAngles;
    }

    private void camraLook()
    {
        Vector3 relativePos = PlayerTransform.position - camera.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, rotation, Time.deltaTime * currentSetting.followSpeed);
    }

    public void ChangeCameraSetting(ControllType controllType)
    {
        switch (controllType)
        {
            case ControllType.Type_A:
                currentSetting = SettingTypeA;
                break;
            case ControllType.Type_B:
                currentSetting = SettingTypeB;
                break;
            case ControllType.Type_C:
                currentSetting = SettingTypeC;
                break;
        }
    }


}
