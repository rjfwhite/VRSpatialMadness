using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VelocityVisualizer : MonoBehaviour
    {
        [Require]
        Velocity.Reader VelocityReader;

        private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            VelocityReader.ComponentUpdated += HandleUpdate;
        }

        private void OnDisable()
        {
            VelocityReader.ComponentUpdated -= HandleUpdate;
        }

        private void HandleUpdate(Velocity.Update obj)
        {
            if (VelocityReader.HasAuthority)
            {
                return;
            }
            _rigidbody.velocity = new Vector3(obj.velocity.Value.X, obj.velocity.Value.Y, obj.velocity.Value.Z);
        }
    }
}
