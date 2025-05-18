using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float mouseSensitivity = 2f; // 鼠标灵敏度

    private float rotationX = 0f;
    private float rotationY = 0f;

    // Update is called once per frame
    void Update()
    {
        // WASD 控制移动
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D 或 左/右
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;   // W/S 或 上/下
        transform.Translate(new Vector3(moveX, 0, moveZ));

        // 按住鼠标右键时控制镜头旋转
        if (Input.GetMouseButton(1)) // 鼠标右键
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX -= mouseY;
            rotationY += mouseX;

            // 限制上下旋转角度
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // 应用旋转
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }
}
