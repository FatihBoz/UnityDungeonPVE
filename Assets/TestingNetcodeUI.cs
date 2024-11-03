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
            Debug.Log("HOÞT");
            NetworkManager.Singleton.StartHost();
            Hide();
        });

        Client.onClick.AddListener(() =>
        {
            Debug.Log("Kýlayýnt");
            NetworkManager.Singleton.StartClient();
            Hide();
        });
    }



    void Hide()
    {
        gameObject.SetActive(false);
    }
}
