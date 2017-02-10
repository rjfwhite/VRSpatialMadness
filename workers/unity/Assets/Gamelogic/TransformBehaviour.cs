using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TransformBehaviour : MonoBehaviour
    {
        [Require] private Position.Writer positionWriter;

        private void Update()
        {
            positionWriter.Send(new Position.Update().SetPosition(new Coordinates(transform.position.x, transform.position.y, transform.position.z)));
        }
    }
}