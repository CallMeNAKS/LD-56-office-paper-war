using System.Collections;
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
        Debug.Log($"Создано существо: {CreatureName}, Здоровье: {Health}, Скорость: {Speed}");
    }

    public void BonusInitialize(int damage)
    {
        Damage += damage;
        Debug.Log($"Существо улучшено: добавлено {damage} урона");
    }

    public void AddEraser()
    {
        _animator.SetBool("IsEraser", true);
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
        HandleCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (IsTarget(collision.gameObject))
        {
            _animator.SetBool("IsEnemyNear", false);
        }
    }

    private void HandleCollision(Collision2D collision)
    {
        if (IsFriendlyCollision(collision.gameObject))
        {
            Physics2D.IgnoreCollision(collision.collider, _collider);
            return;
        }

        if (IsTarget(collision.gameObject) && !_isAttacking)
        {
            _animator.SetBool("IsEnemyNear", true);
            StartCoroutine(DelayedAttack());
        }
    }

    private bool IsFriendlyCollision(GameObject other)
    {
        return (_isEnemy && other.CompareTag("Enemy")) || (!_isEnemy && other.CompareTag("Ally"));
    }

    private bool IsTarget(GameObject other)
    {
        return _isEnemy && other.CompareTag("Ally") || !_isEnemy && other.CompareTag("Enemy");
    }

    private IEnumerator DelayedAttack()
    {
        _isAttacking = true;
        yield return new WaitForSeconds(1f);

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        GameObject target = null;

        foreach (var hitCollider in hitColliders)
        {
            if (IsTarget(hitCollider.gameObject))
            {
                target = hitCollider.gameObject;
                break;
            }
        }

        if (target != null)
        {
            Attack(target);
        }

        _isAttacking = false;
    }

    private void Attack(GameObject target)
    {
        Creature targetCreature = target.GetComponent<Creature>();
        if (targetCreature != null)
        {
            targetCreature.TakeDamage(Damage);
        }
        else
        {
            Base baseComponent = target.GetComponent<Base>();
            if (baseComponent != null)
            {
                baseComponent.Hit(Damage / 2);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"{CreatureName} получил {damage} урона, осталось здоровья: {Health}");

        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{CreatureName} умер!");
        if (_isEnemy)
        {
            CashManager.Instance.AddMoney(Coast);
        }
        Destroy(gameObject);
    }
}
