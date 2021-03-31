using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; //Intervalo entre ataques
    private float damage;
    public float attackDamage;
    public float distanceAttack;
    protected bool canAttack = false;
    protected float timer;

    public Transform attackCheck;
    protected PlayerHealth playerHealth;
    protected GameObject player; //Pegar a classe player para detectar a colisão e pegar o tanto de vida do player
    protected Transform playerTransform;
    protected EnemyHealth enemyHealth;
    protected Animator anim;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerTransform = player.GetComponent<Transform>();
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator>();
        
        if (gameObject.CompareTag("Orc"))
        {
            damage = attackDamage;
        }

        if (gameObject.CompareTag("BossOrc"))
        {
            damage = attackDamage;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (gameObject.CompareTag("Orc"))
        {
            damage = attackDamage;
        }

        if (gameObject.CompareTag("BossOrc"))
        {
            damage = attackDamage;
        }

        checkAreaAttack();
    }

    private void FixedUpdate()
    {
        
    }

    protected void checkAreaAttack()
    {
        Collider2D[] colliders = new Collider2D[3];
        transform.Find("AttackCheck").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Player"))
            {
                if (playerHealth.currentHealth > 0 && timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0f)
                {
                    timer = 0f;
                    anim.SetTrigger("Attack");
                    canAttack = false;
                }
            }
            else
            {
                canAttack = false;
            }
        }
    }

    protected void Attack ()
    {
        Collider2D[] colliders = new Collider2D[3];
        transform.Find("AttackCheck").gameObject.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), colliders);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] != null && colliders[i].gameObject.CompareTag("Player"))
            {
                if (playerHealth.currentHealth > 0)
                {
                    playerHealth.TakeDamage(damage);
                }
            }
            else
            {
                checkAreaAttack();
            }
        }
    }
}