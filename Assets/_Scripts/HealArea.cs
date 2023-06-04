using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArea : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("hit!");
        if (collision.transform.TryGetComponent(out AIHealth player))
        {
            if (player.type == Type.Player)
            {
                Debug.Log("Heal");
                player.HP = Mathf.Clamp(player.HP + .1f, 0, player.MaxHP);
                player.UpdateHPSlider();
            }
        }
    }
}
