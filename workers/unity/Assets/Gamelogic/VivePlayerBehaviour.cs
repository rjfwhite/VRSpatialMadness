using Improbable.General;
using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class VivePlayerBehaviour : MonoBehaviour
    {
        [Require]
        VivePlayer.Writer VivePlayerWriter;

        private void Update()
        {
            var leftHand = GameObject.Find("/[CameraRig]/Controller (left)");
            var rightHand = GameObject.Find("/[CameraRig]/Controller (right)");
            var head = GameObject.Find("/[CameraRig]/Camera (eye)");

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
