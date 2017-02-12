using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class BallSpeedCap : MonoBehaviour
    {
        [Require] Position.Writer positionWriter;
        private Rigidbody objectRigidBody;
        public float MaxSpeed = 10f;

        private void Awake()
        {
            objectRigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (objectRigidBody.velocity.magnitude > MaxSpeed)
            {
                objectRigidBody.velocity = objectRigidBody.velocity.normalized*MaxSpeed;
            }
        }
    }
}
