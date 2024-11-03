using Unity.Netcode;
using UnityEngine;

public class Minimap : NetworkBehaviour
{
    public RectTransform PlayerIndicator;
    public RectTransform IndicatorParent;
    public GameObject MinimapPanel;
    public float yOffset = 40f;

    private RectTransform indic;
    private Transform target;

    private void OnEnable()
    {
        Character.OnCharacterSpawn += Minimap_OnCharacterSpawn;
    }

    private void OnDisable()
    {
        Character.OnCharacterSpawn -= Minimap_OnCharacterSpawn;
    }

    private void Minimap_OnCharacterSpawn(Character character)
    {
        // Ensure we only update the minimap for the local player
        if (character.GetComponent<NetworkObject>().IsOwner)
        {
            target = character.gameObject.transform;
            MinimapPanel.SetActive(true);
            indic = Instantiate(PlayerIndicator, IndicatorParent);
        }
    }


    private void FixedUpdate()
    {
        if (target != null && indic != null)
        {
            transform.position = target.position + new Vector3(0, yOffset, 0);
            Vector3 viewportPosition = GetComponent<Camera>().WorldToViewportPoint(transform.position);

            indic.anchoredPosition = viewportPosition;
        }
    }
}
