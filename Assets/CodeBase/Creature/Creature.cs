using UnityEngine;

public class Creature : MonoBehaviour
{
    public string creatureName;
    public int health;
    public float speed;
    public Sprite creatureSprite;

    private Rigidbody2D rb;

    public void Initialize(string name, int health, float speed, Sprite sprite)
    {
        this.creatureName = name;
        this.health = health;
        this.speed = speed;
        this.creatureSprite = sprite;

        GetComponent<SpriteRenderer>().sprite = sprite;

        Debug.Log($"A creature was created: {creatureName}, Health: {health}, Speed: {speed}");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
