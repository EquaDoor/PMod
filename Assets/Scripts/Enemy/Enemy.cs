using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Damageable
{
    [Header("Debug")]
    [SerializeField] private bool ai = false; // мышление


    [Header("Attacking")]
    private NavMeshAgent agent;
    [SerializeField] private PlayerController target; // цель
    [SerializeField] private float speed = 5f; // скорость
    [SerializeField] private float sightRange = 16f; // радиус замечания
    [SerializeField] private float attackRange = 4f; // радиус замечания
    
    [SerializeField] private float damage = 15f;
    [SerializeField] private float attackCd = 1f; // скорость атаки
    private float attackTimer; // таймер атаки


    [Header("Patroling")]
    [SerializeField] private float walkPointRange = 5f; // радиус ходьбы
    [SerializeField] private LayerMask gMask; // слой пола
    private Vector3 walkPoint; // точка ходьбы
    private bool walkPointSet; // выставлен на ли точка ходьбы



    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    private void Update() {


        if(!ai) return;

        // атаковать если близко
        if(Vector3.Distance(transform.position, target.transform.position) <= attackRange) 
            Attack();
        // приследовать если цель в зрении
        else if(Vector3.Distance(transform.position, target.transform.position) <= sightRange) 
            Chase();
        // если цели нет в зрении
        else if(Vector3.Distance(transform.position, target.transform.position) > sightRange) 
            Patroling();

        // кд атаки
        if(attackTimer>0)
            attackTimer-=Time.deltaTime;
    }

    private void Attack(){
        if(attackTimer<=0)
        {
            target.TakeDamage(damage);
            attackTimer = attackCd;
        }
    }

    private void Chase(){
        agent.SetDestination(target.transform.position);
    }

    private void Patroling() {
        // если нет точки ходьбы
        if(!walkPointSet)
        {
            // выбираем точку ходьбы
            float rZ = Random.Range(-walkPointRange,walkPointRange);
            float rX = Random.Range(-walkPointRange,walkPointRange);

            // выставляем точку ходьбы
            walkPoint = new Vector3(transform.position.x + rX, transform.position.y, transform.position.z + rZ);

            // если точка на полу
            if(Physics.Raycast(walkPoint, Vector3.down, 2f, gMask)) walkPointSet=true;
        }
        // или идти
        else agent.SetDestination(walkPoint);

        // если добрался до точки искать новую
        if(Vector3.Distance(transform.position, walkPoint)<=1.1f) 
            walkPointSet=false;
        
    }


    public override void TakeDamage(float _damage) => base.TakeDamage(_damage);
    public override void Die() => base.Die();

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(walkPoint,.5f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }

}