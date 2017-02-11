using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VelocityBehaviour : MonoBehaviour
    {
        [Require]
        Velocity.Writer VelocityWriter;

        private void OnEnable()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().velocity = new Vector3(VelocityWriter.Data.velocity.X, VelocityWriter.Data.velocity.Y, VelocityWriter.Data.velocity.Z);
        }

        private void OnDisable()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
