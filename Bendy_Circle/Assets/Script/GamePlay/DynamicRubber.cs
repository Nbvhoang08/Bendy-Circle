using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicRubber : MonoBehaviour
{
     public Transform pointA; // Điểm đầu
    public Transform pointB; // Điểm cuối
    public LineRenderer lineRenderer;
    [SerializeField] private BoxCollider2D boxCollider;

    void Start()
    {
        // Thêm thành phần BoxCollider2D nếu chưa có
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
    }

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

            // Lấy độ dày của đường line từ LineRenderer
            float lineWidth = lineRenderer.startWidth;

            // Tính toán vị trí và kích thước cho BoxCollider2D
            float length = Vector3.Distance(positionA, positionB);
            boxCollider.size = new Vector2(length*2, lineWidth*2);

            // Tính toán góc xoay cho BoxCollider2D
            float angle = Mathf.Atan2(positionB.y - positionA.y, positionB.x - positionA.x) * Mathf.Rad2Deg;
            //boxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

            // Tính toán offset cho BoxCollider2D
            Vector3 midPoint = (positionA + positionB) / 2;
            Vector2 offset = boxCollider.transform.InverseTransformPoint(midPoint);
            boxCollider.offset = offset;
        }
    }
}
