using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isOpen;
    public bool isLocked;

    private Animator anim;

    public GenerateNavmesh navMesh;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void openClose()
    {
        if (!isLocked)
        {
            isOpen = !isOpen;
            PlayAnimation();
        }
    }

    public void PlayAnimation()
    {
        anim.SetBool("OpenClose", isOpen);
    }

    public void UpdateMesh()
    {
        navMesh.UpdateNavmesh();
    }
}
