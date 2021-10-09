using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMan : NetworkManager
{
    bool playerSpawned;
    NetworkConnection connection;
    bool playerConnected;

    public void OnCreateCharacter(NetworkConnection conn, PosMessage message)
    {
        GameObject go = Instantiate(playerPrefab, message.vector3, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, go);
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<PosMessage>(OnCreateCharacter);
    }

    public void ActivatePlayerSpawn()
    {
        Vector3 pos = new Vector3(-4, 7, -6);
        //pos = Camera.main.ScreenToWorldPoint(pos);

        PosMessage m = new PosMessage() { vector3 = pos };
        connection.Send(m);
        playerSpawned = true;
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        connection = conn;
        playerConnected = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !playerSpawned && playerConnected)
        {
            ActivatePlayerSpawn();
        }
    }
}

public struct PosMessage : NetworkMessage
{
    public Vector3 vector3;
}