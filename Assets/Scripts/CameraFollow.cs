using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera chaseCamera;
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offset;

    void Awake()
    {
        chaseCamera = GetComponent<Camera>();
    }

    void Update()
    {
        chaseCamera.transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, player.position.z + offset.z);
        chaseCamera.transform.LookAt(player);
    }
}
