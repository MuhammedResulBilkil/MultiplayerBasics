using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class MyNetworkManager : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        MyNetworkPlayer player = conn.identity.GetComponent<MyNetworkPlayer>();
        
        player.SetPlayerDisplayName($"Player {numPlayers}");
        player.SetPlayerColor(Random.ColorHSV());
    }
}
