using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public static float timeBetweenAttacks = 0.5f; //Intervalo entre ataques
    public static float damage;
    protected float attackDamage;
    public float distanceAttack;
    protected bool playerInRange;
    protected float timer;

    protected GameObject player; //Pegar a classe player para detectar a colisão e pegar o tanto de vida do player
    protected Transform playerTransform; 
    protected PlayerHealth playerHealth;
    protected EnemyHealth enemyHealth;
    protected Animator anim;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerTransform = player.GetComponent<Transform>();
        playerHealth = player.GetComponent <PlayerHealth>();
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

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
        Debug.Log(other);
    }

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0f)
        {
            Attack();
        }

        /*if(playerHealth.currentHealth <= 0f)
        {
            anim.SetTrigger ("PlayerDead");
        }*/

        if (gameObject.CompareTag("Orc"))
        {
            damage = attackDamage;
        }

        if (gameObject.CompareTag("BossOrc"))
        {
            damage = attackDamage;
        }
    }

    protected void Attack ()
    {
        //timer = 0f;

        /*if(playerHealth.currentHealth > 0)
        {
            anim.SetTrigger("Attack");
            playerHealth.TakeDamage (damage);
        }*/
        anim.SetTrigger("Attack");
    }
}
