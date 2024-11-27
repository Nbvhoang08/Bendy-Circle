using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform targetPosition; // Vị trí mục tiêu theo localPosition
    public float moveSpeed = 5f; // Tốc độ di chuyển
    public float detectionRadius = 10f; // Bán kính phát hiện đối tượng "Screw"
    public LayerMask screwLayer; // Lớp của đối tượng "Screw"

    private bool isMoving = false;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            // Kiểm tra xem có đối tượng "Screw" trong phạm vi phát hiện không
            Collider2D[] screws = Physics2D.OverlapCircleAll(transform.position, detectionRadius, screwLayer);
            if (screws.Length == 0)
            {
                // Nếu không phát hiện thấy đối tượng "Screw", bắt đầu di chuyển
                isMoving = true;
            }
        }

        if (isMoving)
        {
            // Di chuyển về vị trí mục tiêu
            Vector2 targetLocalPosition = transform.parent.TransformPoint(targetPosition.localPosition);
            transform.position = Vector2.MoveTowards(transform.position, targetLocalPosition, moveSpeed * Time.deltaTime);

            // Kiểm tra nếu đã đến vị trí mục tiêu
            if (Vector2.Distance(transform.position, targetLocalPosition) < 0.01f)
            {
                // Dừng lại khi đến nơi
                isMoving = false;
            }
        }
    }

    // Vẽ bán kính phát hiện trong chế độ Scene để dễ dàng kiểm tra
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
