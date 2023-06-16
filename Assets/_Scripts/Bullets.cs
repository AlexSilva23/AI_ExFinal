using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float time;
    public float Damage;
    void Start()
    {
        StartCoroutine(Despawn(time));
    }

    IEnumerator Despawn(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out AIHealth hp))
        {
            //Debug.Log(GetComponent<Rigidbody>().velocity.x);
            if (GetComponent<Rigidbody>().velocity.x > 3 || GetComponent<Rigidbody>().velocity.x < -3)
            {
                AIHealth Enemy = collision.gameObject.GetComponent<AIHealth>();
                Enemy.HP -= Random.Range(Damage - 2, Damage + 2);
                Enemy.CheckDeath();
                Destroy(gameObject);
            }
        }

    }

}
