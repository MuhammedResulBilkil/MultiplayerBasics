using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] private string _playerDisplayName = "Missing Name";
    [SyncVar] [SerializeField] private Color _playerColor = Color.white;

    [Server]
    public void SetPlayerDisplayName(string newPlayerDisplayName)
    {
        _playerDisplayName = newPlayerDisplayName;
    }

    [Server]
    public void SetPlayerColor(Color newColor)
    {
        _playerColor = newColor;
    }
}
