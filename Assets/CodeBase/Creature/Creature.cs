using UnityEngine;

public class Creature : MonoBehaviour
{
    public string CreatureName { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float Speed { get; private set; }
    public Sprite CreatureSprite { get; private set; }

    public int Coast;

    [SerializeField] private bool _isEnemy;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Collider2D _collider;
    private bool _isAttacking;

    public void Initialize(string name, int health, int damage, float speed, Sprite sprite)
    {
        CreatureName = name;
        Health = health;
        Damage = damage;
        Speed = speed;
        CreatureSprite = sprite;

        GetComponent<SpriteRenderer>().sprite = sprite;
        Debug.Log($"A creature was created: {CreatureName}, Health: {Health}, Speed: {Speed}");
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        float direction = _isEnemy ? -1 : 1;
        _rb.velocity = new Vector2(Speed * direction, _rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((_isEnemy && collision.gameObject.CompareTag("Enemy")) ||
            (!_isEnemy && collision.gameObject.CompareTag("Ally")))
        {
            Physics2D.IgnoreCollision(collision.collider, _collider);
        }

        if (_isEnemy && collision.gameObject.CompareTag("Ally") ||
            !_isEnemy && collision.gameObject.CompareTag("Enemy"))
        {
                _animator.SetBool("IsEnemyNear", true);
            if (!_isAttacking)
            {
                Invoke(nameof(DelayedAttack), 1f); // Задержка в 1 секунду перед первой атакой
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_isEnemy && collision.gameObject.CompareTag("Ally")) ||
            (!_isEnemy && collision.gameObject.CompareTag("Enemy")))
        {
            _animator.SetBool("IsEnemyNear", false);
        }
    }

    private void DelayedAttack()
    {
        _isAttacking = true;

        // Найти объект цели для нанесения урона
        GameObject target = null;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f); // Проверяем наличие объектов рядом
        foreach (var hitCollider in hitColliders)
        {
            if (_isEnemy && hitCollider.CompareTag("Ally") ||
                !_isEnemy && hitCollider.CompareTag("Enemy"))
            {
                target = hitCollider.gameObject;
                break;
            }
        }

        if (target != null)
        {
            Attack(target);
        }
    }

    private void Attack(GameObject target)
    {
        Creature targetCreature = target.GetComponent<Creature>();
        if (targetCreature != null)
        {
            targetCreature.TakeDamage(10);
        }

        _isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{CreatureName} took {damage} damage, health remaining: {Health}");

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{CreatureName} has died!");
        if (_isEnemy == true)
        {
            CashManager.Instance.AddMoney(Coast);
        }
        Destroy(gameObject);
    }
}
