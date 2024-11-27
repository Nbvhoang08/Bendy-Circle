using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Screw : MonoBehaviour
{
    private bool isRotating = false;
    public float rotationSpeed = 120f; // Tốc độ xoay (độ/giây)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}

