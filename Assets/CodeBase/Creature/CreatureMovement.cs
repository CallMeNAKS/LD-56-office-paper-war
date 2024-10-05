using UnityEngine;

public class CreatureMovement : MonoBehaviour
{
    public float speed = 2f;
    private Animator animator;
    private Vector2 direction;

    void Start()
    {
        animator = GetComponent<Animator>();
        direction = Vector2.left;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        animator.SetBool("isWalking", true);
    }
}
