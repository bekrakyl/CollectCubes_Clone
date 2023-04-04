using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementSettings movementSettings;

    [SerializeField] private Transform collectorModel;

    private GameManager gameManager;
    private ObjectManager objectManager;

    private DynamicJoystick dynamicJoystick;

    private Rigidbody body;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();   
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        objectManager = ObjectManager.Instance;

        dynamicJoystick = objectManager.dynamicJoystick;

        GameManager.Instance.GameWin += GameEnd;
    }

    private void GameEnd()
    {
        body.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (!gameManager.ExecuteGame) return;

        body.velocity = new Vector3(dynamicJoystick.Horizontal, 
            body.velocity.y, 
            dynamicJoystick.Vertical) * movementSettings.movementSpeed;
        CollectorLookAt();
    }

    private void CollectorLookAt()
    {
        if (body.velocity.magnitude > 1)
        {
            var lookPos = body.velocity + Vector3.forward * .5f;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            collectorModel.rotation = Quaternion.Slerp(collectorModel.rotation, rotation, movementSettings.sensivity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!gameManager.ExecuteGame) return;
        other.gameObject.GetComponent<ICollisionCube>()?.OnInteractEnter("Player");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!gameManager.ExecuteGame) return;
        other.gameObject.GetComponent<ICollisionCube>()?.OnInteractExit();
    }
}
