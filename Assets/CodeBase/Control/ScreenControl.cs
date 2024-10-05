using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 5f;
    public float edgeThreshold = 50f;
    public Vector2 minBounds;
    public Vector2 maxBounds;

    public Animator animator;

    private Vector2 screenBounds;

    void Start()
    {
        screenBounds = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        ChangeState();
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("IsMoveRight", true);
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
        if (direction == Vector3.left)
        {
            ChangeState("IsMoveLeft");
        }
        else
        {
            ChangeState("IsMoveRight");
        }
    }

    void ClampCameraPosition()
    {
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minBounds.x, maxBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minBounds.y, maxBounds.y);

        transform.position = clampedPosition;
    }

    private void ChangeState(string state)
    {
        animator.SetBool("IsMoveRight", false);
        animator.SetBool("IsMoveLeft", false);

        animator.SetBool(state, true);
    }
    private void ChangeState()
    {
        animator.SetBool("IsMoveRight", false);
        animator.SetBool("IsMoveLeft", false);
    }
}
