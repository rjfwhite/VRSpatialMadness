using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VivePlayerVisualizer : MonoBehaviour
    {
        [Require]
        VivePlayer.Reader VivePlayerReader;

        public Transform head;
        public Transform rightHand;
        public Transform leftHand;

        private void OnEnable()
        {
            SetLimbs();
            VivePlayerReader.ComponentUpdated += VivePlayerReader_ComponentUpdated;
        }

        private void VivePlayerReader_ComponentUpdated(VivePlayer.Update update)
        {
            SetLimbs();
        }

        private void OnDisable()
        {
            VivePlayerReader.ComponentUpdated -= VivePlayerReader_ComponentUpdated;
        }

        private void SetLimbs()
        {
            SetViveTransform(VivePlayerReader.Data.head, head);
            SetViveTransform(VivePlayerReader.Data.rightHand, rightHand);
            SetViveTransform(VivePlayerReader.Data.leftHand, leftHand);
        }

        private void SetViveTransform(ViveTransform spatialTransform, Transform unityTransform)
        {
            unityTransform.position = new Vector3((float)spatialTransform.position.X, (float)spatialTransform.position.Y, (float)spatialTransform.position.Z);
            unityTransform.rotation = new UnityEngine.Quaternion(spatialTransform.rotation.x, spatialTransform.rotation.y, spatialTransform.rotation.z, spatialTransform.rotation.w);
        }
    }
}
