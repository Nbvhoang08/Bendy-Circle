using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRubber : MonoBehaviour
{
    public Transform pointA; // Điểm đầu
    public Transform pointB; // Điểm cuối
    public LineRenderer lineRenderer;

    void Update()
    {
        if (pointA != null && pointB != null && lineRenderer != null)
        {
            // Lấy vị trí của các điểm và đặt tọa độ Z bằng 0
            Vector3 positionA = new Vector3(pointA.position.x, pointA.position.y, 0);
            Vector3 positionB = new Vector3(pointB.position.x, pointB.position.y, 0);

            // Đặt vị trí cho Line Renderer
            lineRenderer.SetPosition(0, positionA);
            lineRenderer.SetPosition(1, positionB);
        }
    }
}
