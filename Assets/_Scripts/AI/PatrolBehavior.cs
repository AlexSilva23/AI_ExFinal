using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class PatrolBehavior : MonoBehaviour
{
    public NavMeshAgent agent;
    float random_destination_radius = 1.0f;
    RaycastHit hit;
    public Transform player;
    public Vector3 startPos;
    public PatrolBehavior[] enemies;
    public Vector3 LastPos;
    public bool AwareOfPlayer;

    public GameObject bulletPrefab;
    public Transform exitPoint;
    public float ShootForce;
    public float timeBetweenFire;
    public float LastShoot;

    private void Start()
    {
        startPos = transform.position;
        enemies = FindObjectsOfType<PatrolBehavior>();
    }

    private void Update()
    {
        LastShoot += Time.deltaTime;
    }

    [Task]
    bool isMoving()
    {
        return agent.velocity != Vector3.zero;
    }

    [Task]
    bool SeesPlayer()
    {
        if (Physics.Raycast(transform.position, (player.position - transform.position), out hit))
        {
            if (hit.transform == player)
            {
                LastPos = player.position;
                AwareOfPlayer = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    [Task]
    bool SetDestination_Random(int radius)
    {
        random_destination_radius = radius;
        return SetDestination_Random();
    }

    [Task]
    bool SetDestination_Random()
    {
        agent.SetDestination((agent.transform.position + Random.insideUnitSphere * random_destination_radius));
        return true;
    }

    [Task]
    void ChasePlayer()
    {
        agent.SetDestination(LastPos);
        Task.current.Succeed();
    }

    [Task]
    void AlertEnemies()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<PandaBehaviour>().Reset();
            enemies[i].agent.SetDestination(LastPos);
        }
    }

    [Task]
    void ReturnToBase()
    {
        agent.SetDestination(startPos);
    }

    [Task]
    bool LastPosFunc()
    {
        Debug.Log(Vector3.Distance(transform.position, LastPos) < .7);
        return Vector3.Distance(transform.position, LastPos) < .7;
    }

    [Task]
    void Panic()
    {
        agent.transform.eulerAngles = new Vector3(agent.transform.rotation.x + Random.Range(0, 360),
            agent.transform.rotation.y + Random.Range(0, 360),
            agent.transform.rotation.z + Random.Range(0, 360));
    }

    [Task]
    void ForgetPlayer()
    {
        AwareOfPlayer = false;
        Task.current.Succeed();
    }

    [Task]
    bool AwarePlayer()
    {
        return AwareOfPlayer;
    }

    [Task]
    void ShootBalls()
    {
        if (SeesPlayer())
        {
            if (timeBetweenFire < LastShoot)
            {
                GameObject Bullet = Instantiate(bulletPrefab, exitPoint.position, Quaternion.identity);
                Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * ShootForce);
                LastShoot = timeBetweenFire;
                LastShoot = 0;
                agent.ResetPath();
            }
        }
        Task.current.Fail();
    }

    [Task]
    void openDoor()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
        {
            if (hit.transform.GetComponent<DoorScript>() != null)
            {
                if (!hit.transform.GetComponent<DoorScript>().isOpen)
                {
                    hit.transform.GetComponent<DoorScript>().openClose();
                }
            }
        }
        Task.current.Succeed();
    }
}
