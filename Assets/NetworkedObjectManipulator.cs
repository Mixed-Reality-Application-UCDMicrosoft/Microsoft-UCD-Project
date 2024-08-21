using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class NetworkedObjectManipulator : NetworkBehaviour
{
    public int ownerID;
    public void TransferOwnershipObject()
    {
        Debug.Log($"Client: {NetworkClient.connection.connectionId} {connectionToServer}");
        CmdTransferOwn(NetworkClient.connection.connectionId);
    }

    [Command(requiresAuthority = false)]
    public void CmdTransferOwn(int connectionId)
    {
        NetworkIdentity id = gameObject.GetComponentInParent<NetworkIdentity>();
        Debug.Log($"{connectionToClient} client and server {connectionId} {NetworkServer.connections[connectionId].connectionId}");
        //id.AssignClientAuthority(connectionToClient);
    }
}
