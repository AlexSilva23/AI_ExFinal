using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed;
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform exitPoint;
    public float ShootForce;
    public float timeBetweenFire;
    public float LastShoot;

    private Rigidbody _rb;
    private Camera m_camera;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        m_camera = Camera.main;  // Don't keep calling Camera.main
    }

    private void Update()
    {
        //Player rotation
        Ray mouseRay = m_camera.ScreenPointToRay(Input.mousePosition);
        Plane p = new Plane(Vector3.up, transform.position);
        if (p.Raycast(mouseRay, out float hitDist))
        {
            Vector3 hitPoint = mouseRay.GetPoint(hitDist);
            transform.LookAt(hitPoint);
        }

        //Raycast
        //Debug.DrawLine(transform.position, transform.forward, Color.red, 2);

        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
            {
                if (hit.transform.GetComponent<DoorScript>() != null)
                {
                    hit.transform.GetComponent<DoorScript>().openClose();
                }
            }
        }

        //Shot
        LastShoot -= Time.deltaTime;
            
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (LastShoot <= 0)
            {
                Shoot();
            }
        }
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.z += moveZ * MoveSpeed * Time.deltaTime;
        pos.x += moveX * MoveSpeed * Time.deltaTime;
        _rb.MovePosition(pos);
    }

    public void Shoot()
    {
        GameObject Bullet = Instantiate(bulletPrefab, exitPoint.position, Quaternion.identity);
        Bullet.GetComponent<Rigidbody>().AddForce(transform.forward * ShootForce);
        LastShoot = timeBetweenFire;
    }
}
