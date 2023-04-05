using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Utils;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshController : Singleton<NavMeshController>
{
    public List<Stand> Stands = new List<Stand>();
    public MeshRenderer BusStopMesh;
    private NavMeshSurface _navMeshSurface;
    
    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        
        foreach (var stand in Stands)
        {
            if(stand.IsStandBuild)
                stand.EnvironmentsSetting();
        }
        
        RebuildNavMesh();
    }

    public void RebuildNavMesh()
    {
      
        BusStopMesh.enabled = true;
        _navMeshSurface.BuildNavMesh();
        BusStopMesh.enabled = false;
    }
}
