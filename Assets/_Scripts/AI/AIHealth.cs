using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    Player,
    Patrol,
    Stacionary
}

public class AIHealth : MonoBehaviour
{

    public Type type;
    public float HP;

    public float cubeSize = 0.2f;
    public int cubesInRow = 5;

    float cubesPivotDistance;
    Vector3 cubesPivot;

    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    public Color cubesColor;

    PatrolBehavior patrolController;

    private void Start()
    {
        cubesPivotDistance = cubeSize * cubesInRow / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance);
        patrolController = GetComponent<PatrolBehavior>();
    }

    public void CheckDeath()
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (type == Type.Stacionary)
        {
            patrolController.UnlockDoors();
        }
        GetComponentInParent<DestroyGameobject>().enabled = true;
        gameObject.SetActive(false);

        for (int x = 0; x < cubesInRow; x++)
        {
            for (int y = 0; y < cubesInRow; y++)
            {
                for (int z = 0; z < cubesInRow; z++)
                {
                    createPiece(x, y, z);
                }
            }
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, explosionUpward);
            }
        }

        if (type == Type.Player)
        {
            //Menu Gameover

        }
    }

    void createPiece(int x, int y, int z)
    {

        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        piece.transform.position = transform.position + new Vector3(cubeSize * x, cubeSize * y, cubeSize * z) - cubesPivot;
        piece.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = cubeSize;
        piece.transform.parent = transform.parent;
        piece.GetComponent<Renderer>().material.SetColor("_Color", cubesColor);
    }
}
