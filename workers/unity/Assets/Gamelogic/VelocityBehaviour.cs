using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VelocityBehaviour : MonoBehaviour
    {
        [Require]
        Velocity.Writer VelocityWriter;

        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().velocity = new Vector3(VelocityWriter.Data.velocity.X, VelocityWriter.Data.velocity.Y, VelocityWriter.Data.velocity.Z);
        }

        private void FixedUpdate()
        {
            VelocityWriter.Send(new Velocity.Update().SetVelocity(new Vector3f(_rigidbody.velocity.x, _rigidbody.velocity.y, _rigidbody.velocity.z)));
        }

        private void OnDisable()
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
