using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private float timeBetweenFire;
    public float LastShoot;
    public GameObject bulletPrefab;
    public Transform exitPoint;
    public float ShootForce;


    // Update is called once per frame
    void Update()
    {
        LastShoot += Time.deltaTime;

        if (timeBetweenFire < LastShoot)
        {
            GameObject Bullet = Instantiate(bulletPrefab, exitPoint.position, Quaternion.identity);
            Bullet.GetComponent<Rigidbody>().AddForce(transform.right * ShootForce);
            LastShoot = timeBetweenFire;
            LastShoot = 0;
            SetRandomTime();
        }

    }

    void SetRandomTime()
    {
        timeBetweenFire = Random.Range(1f, 2f);
    }
}
