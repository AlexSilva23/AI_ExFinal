using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GenerateNavmesh : MonoBehaviour
{
    public NavMeshSurface surface;
    void Start()
    {
        UpdateNavmesh();
    }

    public void UpdateNavmesh()
    {
        //surface.BuildNavMesh(); 
    }
}
