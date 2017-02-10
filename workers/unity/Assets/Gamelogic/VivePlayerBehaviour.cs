using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VivePlayerBehaviour : MonoBehaviour
    {
        [Require]
        VivePlayer.Writer VivePlayerWriter;

        public Transform head;
        public Transform rightHand;
        public Transform leftHand;

        private void OnEnable()
        {
            Debug.Log("IT BEGINS");
            leftHand = GameObject.Find("/[CameraRig]/Controller (left)").transform;
            rightHand = GameObject.Find("/[CameraRig]/Controller (right)").transform;
            head = GameObject.Find("/[CameraRig]/Camera (eye)").transform;
        }

        private void Update()
        {
            VivePlayerWriter.Send(new VivePlayer.Update().SetHead(createViveTransform(head)).SetLeftHand(createViveTransform(leftHand)).SetRightHand(createViveTransform(rightHand)));
        }

        private ViveTransform createViveTransform(Transform unityTransform)
        {
            return new ViveTransform(new Improbable.Math.Coordinates(unityTransform.position.x, unityTransform.position.y, unityTransform.position.z), new Improbable.Player.Quaternion(unityTransform.rotation.x, unityTransform.rotation.y, unityTransform.rotation.z, unityTransform.rotation.w));
        }
    }
}
