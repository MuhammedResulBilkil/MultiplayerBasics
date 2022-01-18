using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private Renderer _playerRenderer;
    [SerializeField] private TextMeshProUGUI _playerDisplayText;
    
    [SyncVar(hook = nameof(HandleUpdatePlayerDisplayText))] [SerializeField] private string _playerDisplayName = "Missing Name";
    [SyncVar(hook = nameof(HandleUpdatePlayerColor))] [SerializeField] private Color _playerColor = Color.white;

    #region Server

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

    [Command]
    private void CmdSetPlayerDisplayName(string newPlayerDisplayName)
    {
        if(newPlayerDisplayName.Length < 2 || newPlayerDisplayName.Length > 15)
            return;
        
        RpcLogPlayerDisplayName(newPlayerDisplayName);
        
        SetPlayerDisplayName(newPlayerDisplayName);
    }

    #endregion
    
    
    #region Client

    private void HandleUpdatePlayerColor(Color oldColor, Color newColor)
    {
        _playerRenderer.material.SetColor("_BaseColor", newColor);
    }
    
    private void HandleUpdatePlayerDisplayText(string oldDisplayName, string newDisplayName)
    {
        _playerDisplayText.text = newDisplayName;
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetPlayerDisplayName("M");
    }
    
    [ClientRpc]
    private void RpcLogPlayerDisplayName(string newPlayerDisplayName)
    {
        Debug.Log($"Player New Name = {newPlayerDisplayName}");
    }

    #endregion
    
}
