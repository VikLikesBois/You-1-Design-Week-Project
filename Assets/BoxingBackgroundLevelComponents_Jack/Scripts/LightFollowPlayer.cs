using UnityEngine;

public class LightFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform followPlayer;

    void Update()
    {
        if (followPlayer == null) return;

        transform.LookAt(followPlayer.position);
    }
}
