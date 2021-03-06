using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private Camera _mainCamera;
    
    #region Server

    [Command]
    private void CmdMove(Vector3 position)
    {
        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas ))
            return;

        _navMeshAgent.SetDestination(hit.position);
    }

    #endregion

    #region Client

    public override void OnStartAuthority()
    {
        _mainCamera = Camera.main;
    }

    [ClientCallback]
    private void Update()
    {
        if(!hasAuthority) return;

        if(!Input.GetMouseButtonDown(1)) return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            CmdMove(raycastHit.point);
        
    }

    #endregion
    
}
