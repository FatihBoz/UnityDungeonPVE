using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUI : MonoBehaviour
{
    public Button Host;
    public Button Client;

    private void Awake()
    {
        Host.onClick.AddListener(() =>
        {
            Debug.Log("HO�T");
            NetworkManager.Singleton.StartHost();
            Hide();
        });

        Client.onClick.AddListener(() =>
        {
            Debug.Log("K�lay�nt");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }



    void Hide()
    {
        gameObject.SetActive(false);
    }
}
