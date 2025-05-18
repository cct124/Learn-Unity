using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float mouseSensitivity = 2f; // 鼠标灵敏度

    public float maxSpeed = 20f; // 最大速度

    private float rotationX = 0f;
    private float rotationY = 0f;
    private Vector3 initialRotation; // 初始旋转角度

    // horizontal增加量
    private float verticalDeltaTimeIncrement = 0f;



    void Start()
    {
        // 记录初始旋转角度
        initialRotation = transform.localEulerAngles;
        rotationX = initialRotation.x;
        rotationY = initialRotation.y;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical"); // W/S 或 上/下
        if (vertical != 0)
        {
            verticalDeltaTimeIncrement += Time.deltaTime;
        }
        else
        {
            verticalDeltaTimeIncrement = 0f; // 重置增量
        }

        // WASD 控制移动
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // A/D 或 左/右
        float moveSpeedZ = Mathf.Clamp(moveSpeed + (verticalDeltaTimeIncrement * 5), 0, maxSpeed); // 计算移动速度
        float moveZ = vertical * moveSpeedZ * Time.deltaTime;   // W/S 或 上/下
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

            // 应用旋转时加上初始旋转角度
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0f);
        }
    }
}
