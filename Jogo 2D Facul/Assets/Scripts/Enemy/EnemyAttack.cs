using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Enemy Attack")]
    public float timeBetweenAttacks = 0.5f; //Intervalo entre ataques
    private float damage;
    public float attackDamage;
    public float distanceAttack;
    protected bool canAttack = false;
    protected float timer;

    [Header("Enemy Components")]
    public Transform attackCheck;
    private audioController audioController;
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
        audioController = FindObjectOfType(typeof(audioController)) as audioController;
    }

    void Update()
    {
        timer += Time.deltaTime;
        damage = attackDamage;
    }

    private void FixedUpdate()
    {
        checkAreaAttack();
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
        audioController.tocarFx(audioController.fxEspadaOrc, 1);
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