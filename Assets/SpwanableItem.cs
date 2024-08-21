using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SpwanableItem : NetworkBehaviour
{
    [SerializeField] private GameObject spawnObject;


    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log($"Enabling {spawnObject.name}");
        CmdSpawnObject(spawnObject);
        
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnObject(GameObject obj)
    {
        Debug.Log($"Hello from server! {obj}");
        GameObject instance = Instantiate(obj, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(obj);
        Debug.Log("Calling in server");
        RpcSpawnObjectClient();
    }

    [ClientRpc]
    public void RpcSpawnObjectClient()
    {
        Debug.Log("Clanning in client");
    }
}
