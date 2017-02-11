using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TransformBehaviour : MonoBehaviour
    {
        [Require] private Position.Writer positionWriter;
        private float sendInterval = 0.1f;
        private float elapsedTimeSinceLastUpdate;

        private void OnEnable()
        {
            elapsedTimeSinceLastUpdate = Time.time;
        }

        private void Update()
        {
            if (Time.time - elapsedTimeSinceLastUpdate < sendInterval)
            {
                return;
            }
            if (Vector3.SqrMagnitude(positionWriter.Data.position.ToVector3() - transform.position) < Mathf.Epsilon)
            {
                return;
            }
            elapsedTimeSinceLastUpdate = Time.time;
            positionWriter.Send(new Position.Update().SetPosition(transform.position.ToCoordinates()));
        }
    }
}