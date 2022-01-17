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

    private void HandleUpdatePlayerColor(Color oldColor, Color newColor)
    {
        _playerRenderer.material.SetColor("_BaseColor", newColor);
    }
    
    private void HandleUpdatePlayerDisplayText(string oldDisplayName, string newDisplayName)
    {
        _playerDisplayText.text = newDisplayName;
    }
}
