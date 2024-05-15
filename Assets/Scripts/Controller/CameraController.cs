using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private Transform PlayerTransform;

    [SerializeField] private float followSpeed = 5;
    [SerializeField] private float distance = 5;
    [SerializeField] private float hight = 5;

    void LateUpdate()
    {
        FollowPlayer();
        CameraRotate();
    }

    private void FollowPlayer()
    {
        var target = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, PlayerTransform.position.z);
        transform.position = Vector3.Lerp(transform.position,target,Time.deltaTime * followSpeed);

        camera.transform.localPosition = (Vector3.forward * (-distance)) + (Vector3.up * hight);
    }
    private void CameraRotate()
    {
        transform.eulerAngles = PlayerTransform.eulerAngles;
    }


}
