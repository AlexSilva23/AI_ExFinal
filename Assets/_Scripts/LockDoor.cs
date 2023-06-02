using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoor : MonoBehaviour
{
    public DoorScript door;

    private void OnTriggerEnter(Collider other)
    {
        if (door.isOpen)
        {
            Debug.Log("is in!");
            door.openClose();
        }
        door.isLocked = true;
        //Destroy(gameObject);
    }
}
