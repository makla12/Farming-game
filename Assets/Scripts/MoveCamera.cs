using UnityEngine;

public class MousePanCamera : MonoBehaviour
{
    private PlayerControls controls;
    // The main camera component
    [SerializeField] private Camera cam;
    private Vector3 dragOrigin;

    [Header("Boundary Limits (Set to 0 to disable)")]
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 0f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0f;

    private void HandlePan()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            return;
        }

        if (!Input.GetKey(KeyCode.Mouse0)) 
        {
            return;
        }

        Vector3 currentMouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = dragOrigin - currentMouseWorldPos;
        Vector3 newPosition = transform.position + difference;

        if (minX != 0 || maxX != 0)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        }
        if (minY != 0 || maxY != 0)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }

        transform.position = newPosition;
    }

    private void HandleScroll()
    {
        float scrollInput = Input.mouseScrollDelta.y;
        if (scrollInput != 0f)
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - scrollInput * 5f, 2f, 20f);
        }
    }

    void Update()
    {
        HandlePan();
        HandleScroll();
    }
}