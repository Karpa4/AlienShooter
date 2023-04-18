using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public Transform PlayerTransform;
    public static PlayerPosition instance;

    private void Awake()
    {
        instance = this;
    }
}
