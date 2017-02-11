using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TransformReceiver : MonoBehaviour
    {
        [Require] private Position.Reader positionReader;

        private void OnEnable()
        {
            transform.position = positionReader.Data.position.ToVector3();
            positionReader.ComponentUpdated += OnComponentUpdated;
        }

        private void OnDisable()
        {
            positionReader.ComponentUpdated -= OnComponentUpdated;
        }

        void OnComponentUpdated(Position.Update update)
        {
            if (positionReader.HasAuthority) return;
            if (update.position.HasValue)
            {
                transform.position = update.position.Value.ToVector3();
            }

        }
    }

    public static class CoordinatesExtensions
    {
        public static Vector3 ToVector3(this Coordinates coordinates)
        {
            return new Vector3((float)coordinates.X, (float)coordinates.Y, (float)coordinates.Z);
        }
    }
}