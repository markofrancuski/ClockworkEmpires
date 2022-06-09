using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float PanSpeed = 20f;
    public float ScrollSpeed = 2f;
    /// <summary>
    /// Value when the mouse position is within. Use as input for camera movement.
    /// </summary>
    public float PanBorderThickness = 10f;

    public Vector2 Limit;
    public float minY = 10;
    public float maxY = 50;

    private void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - PanBorderThickness)
        {
            position.z += PanSpeed * Time.deltaTime;
        }

        if (Input.GetKey("s") || Input.mousePosition.y <= PanBorderThickness)
        {
            position.z -= PanSpeed * Time.deltaTime;
        }

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - PanBorderThickness)
        {
            position.x += PanSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= PanBorderThickness)
        {
            position.x -= PanSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y += scroll * ScrollSpeed * 100f * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -Limit.x, Limit.x);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, -Limit.y, Limit.y);

        transform.position = position;
    }
}
