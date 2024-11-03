using UnityEngine;
using UnityEngine.AI;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemySO enemy;

    protected Animator anim;
    protected NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }


    //todo: DALGA SEVÝYESÝNE GÖRE DÜÞMANIN CANI VE SALDIRISINI YÜKSELT


}
