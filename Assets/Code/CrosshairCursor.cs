using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairCursor : MonoBehaviour
{
    public float rotationSpeed = -20f;
    public float expansionAmount = 1.05f;
    public float maxExpansionAmount = 2f;
    public float resetTime = 0.2f;
    private Vector3 originalScale;

    void Start()
    {
        Cursor.visible = false;
        originalScale = transform.localScale;
    }

    void Update()
    {
        // track mouse position
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        // rotation
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

        // expand crosshair
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 newScale = transform.localScale * expansionAmount;
            newScale.x = Mathf.Min(newScale.x, maxExpansionAmount);
            newScale.y = Mathf.Min(newScale.y, maxExpansionAmount);
            newScale.z = Mathf.Min(newScale.z, maxExpansionAmount);

            transform.localScale = newScale;

            StartCoroutine(ResetScale());
        }
    }

    private IEnumerator ResetScale()
    {
        yield return new WaitForSeconds(resetTime);
        transform.localScale = originalScale;
    }
}
