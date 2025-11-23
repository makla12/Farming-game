using UnityEngine;

public class MousePanCamera : MonoBehaviour
{
    private PlayerControls controls;
    [SerializeField] private Camera cam;
    private Vector3 dragOrigin;
    private bool isPanning = false;

    [Header("Boundary Limits (Set to 0 to disable)")]
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 0f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 0f;

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Click.started += ctx => 
        {
            isPanning = true;
            dragOrigin = cam.ScreenToWorldPoint(controls.Player.Point.ReadValue<Vector2>());
        };
        controls.Player.Click.canceled += ctx => isPanning = false;

        controls.Player.Enable();
    }

    private void HandlePan()
    {
        Vector3 currentMouseWorldPos = cam.ScreenToWorldPoint(controls.Player.Point.ReadValue<Vector2>());
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

    void Update()
    {
        if(isPanning) HandlePan();
    }
}