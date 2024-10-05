using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5f;
    public float edgeThreshold = 50f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MoveCamera(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveCamera(Vector3.left);
        }

        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x >= screenBounds.x - edgeThreshold)
        {
            MoveCamera(Vector3.right);
        }
        else if (mousePosition.x <= edgeThreshold)
        {
            MoveCamera(Vector3.left);
        }

        ClampCameraPosition();
    }

    void MoveCamera(Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void ClampCameraPosition()
    {
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);

        transform.position = clampedPosition;
    }
}
