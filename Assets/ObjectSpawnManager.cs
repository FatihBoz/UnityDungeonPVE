using Unity.Netcode;
using UnityEngine;

public class ObjectSpawnManager : NetworkBehaviour
{
    //!KULLANILMIYOR
    public static ObjectSpawnManager Instance { get;private set; }

    private void Awake()
    {
        Instance = this;
    }



}
