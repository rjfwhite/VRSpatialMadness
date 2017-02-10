using Improbable.General;
using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VivePlayerBehaviour : MonoBehaviour
    {
        [Require] VivePlayer.Writer VivePlayerWriter;

        private GameObject leftHand;
        private GameObject rightHand;
        private GameObject head;

        private void Awake()
        {
            leftHand = GameObject.Find("/[CameraRig]/Controller (left)");
            rightHand = GameObject.Find("/[CameraRig]/Controller (right)");
            head = GameObject.Find("/[CameraRig]/Camera (eye)");
        }

        private void Update()
        {
            var update = new VivePlayer.Update();

            if(leftHand)
            {
                update.SetLeftHand(createViveTransform(leftHand.transform));
            }

            if(rightHand)
            {
                update.SetRightHand(createViveTransform(rightHand.transform));
            }

            if(head)
            {
                update.SetHead(createViveTransform(head.transform));
            }

            VivePlayerWriter.Send(update);
        }

        private ViveTransform createViveTransform(Transform unityTransform)
        {
            return new ViveTransform(new Improbable.Math.Coordinates(unityTransform.position.x, unityTransform.position.y, unityTransform.position.z), new ImprobableQuaternion(unityTransform.rotation.x, unityTransform.rotation.y, unityTransform.rotation.z, unityTransform.rotation.w));
        }
    }
}
