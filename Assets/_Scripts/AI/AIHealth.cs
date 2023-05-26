using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float HP;

    public void CheckDeath()
    {
        if (HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Die");
    }
}
