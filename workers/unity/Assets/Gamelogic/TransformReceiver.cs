using Improbable.General;
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
            if (positionReader.HasAuthority)
            {
                return;
            }
            
            if (update.position.HasValue)
            {
                transform.position = update.position.Value.ToVector3();
            }
        }
    }
}
