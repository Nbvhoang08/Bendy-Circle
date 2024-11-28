using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Screw : MonoBehaviour
{
    [SerializeField] private bool isRotating = false;
    public float rotationSpeed = 120f; // Tốc độ xoay (độ/giây)
    public float detectionRadius = 5f; // Bán kính phát hiện
    public LayerMask playerLayer; // Layer của player

    void OnEnable() 
    {
        StartCoroutine(RotateOnEnable());
        StartCoroutine(CheckForPlayer());
    }

    void OnMouseDown()
    {
        if (!isRotating)
        {
            // Bắt đầu coroutine để xoay đối tượng
            StartCoroutine(RotateAndDisable());
        }
    }

    private IEnumerator RotateAndDisable()
    {
        isRotating = true;
        float totalRotation = 0f;

        while (totalRotation < 90f)
        {
            // Tính toán góc xoay trong khung hình hiện tại
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationThisFrame);
            totalRotation += rotationThisFrame;

            // Đợi đến khung hình tiếp theo
            yield return null;
        }

        // Đảm bảo đối tượng xoay đúng 90 độ
        transform.Rotate(0, 0, 90f - totalRotation);

        yield return new WaitForSeconds(0.3f);
        // Tắt đối tượng
        gameObject.SetActive(false);
    }

    private IEnumerator RotateOnEnable()
    {
        isRotating = true;
        float totalRotation = 0f;

        while (totalRotation < 90f)
        {
            // Tính toán góc xoay trong khung hình hiện tại
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.Rotate(0, 0, -rotationThisFrame);
            totalRotation += rotationThisFrame;

            // Đợi đến khung hình tiếp theo
            yield return null;
        }

        // Đảm bảo đối tượng xoay đúng 90 độ
        transform.Rotate(0, 0, 90f + totalRotation);

        yield return new WaitForSeconds(0.3f);
        // Tắt đối tượng
        isRotating = false;
    }

    private IEnumerator CheckForPlayer()
    {
        while (true)
        {
            // Kiểm tra xem có đối tượng "Player" trong phạm vi phát hiện không
            Collider2D player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

            if (player == null)
            {
                // Nếu không phát hiện thấy đối tượng "Player", chờ 1 giây rồi tắt đối tượng
                yield return new WaitForSeconds(0.2f);
                player = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);
                if (player == null)
                {
                    gameObject.SetActive(false);
                }
            }

            // Đợi đến khung hình tiếp theo
            yield return null;
        }
    }
     void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}


