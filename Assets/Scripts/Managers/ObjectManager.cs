using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    private static ObjectManager instance;
    public static ObjectManager Instance { get => instance; set => instance = value; }

    public DynamicJoystick dynamicJoystick;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

}
