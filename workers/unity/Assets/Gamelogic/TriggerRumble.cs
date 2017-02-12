using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class TriggerRumble : MonoBehaviour
    {
        [Require] VivePlayer.Writer vivePlayerWriter;
        private PlayerInputBehaviour playerInputBehaviour;
        public bool IsLeftHand = true;
        public bool IsRightHand = true;

        private void Awake()
        {
            playerInputBehaviour = GetComponentInParent<PlayerInputBehaviour>();
            if (playerInputBehaviour == null)
            {
                Debug.LogError("ADDADAFAFAFFAA");
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsLeftHand)
            {
                playerInputBehaviour.VibrateLeft();
            }

            if (IsRightHand)
            {
                playerInputBehaviour.VibrateRight();
            }
        }
    }
}
