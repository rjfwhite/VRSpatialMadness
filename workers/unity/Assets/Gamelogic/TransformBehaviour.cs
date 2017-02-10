using UnityEngine;
using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;

namespace Assets.Gamelogic.Behaviours
{
    // This MonoBehaviour will be enabled on both client and server-side workers
    public class TransformBehaviour : MonoBehaviour
    {
        // Inject access to the entity's WorldTransform component
        [Require]
        private Position.Writer WorldTransformReader;

        private void Update()
        {
            WorldTransformReader.Send(new Position.Update().SetPosition(new Coordinates(transform.position.x, transform.position.y, transform.position.z)));
        }
    }
}