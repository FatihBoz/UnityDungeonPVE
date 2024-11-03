using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance {  get; private set; }

    
    [SerializeField] private Button gameStartButton;

    public override void OnNetworkSpawn()
    {
        if (IsOwner && Instance == null)
        {
            Instance = this;
            AddListenerToStartButton(gameStartButton);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void AddListenerToStartButton(Button startButton)
    {
        startButton.onClick.AddListener(() =>
        {
            OnGameStart();
        });
    }
    
    private void OnGameStart()
    {
        //Invoke Game start event
    }

}
