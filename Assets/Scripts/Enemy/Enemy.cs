using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{

    [Header("Enemy")]
    private NavMeshAgent agent;
    private PlayerController target; // цель
    [SerializeField] private float speed = 5f; // скорость
    [SerializeField] private float sightRange = 10f; // радиус замечания
    [SerializeField] private float attackRange = 1.5f; // радиус замечания
    

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }


    public override void TakeDamage(float _damage) => base.TakeDamage(_damage);
    public override void Die() => base.Die();

}