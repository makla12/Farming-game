using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float sensivity = 0.0028f;
    [SerializeField] private float scrollSensivity = 0.1f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mouseDelta = new(Input.mousePositionDelta.x, 0, Input.mousePositionDelta.y);
            transform.position -= mouseDelta * sensivity * transform.position.y;
        }
        float newY = Mathf.Clamp(transform.position.y - scrollSensivity * transform.position.y * Input.mouseScrollDelta.y, 10, 80);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
