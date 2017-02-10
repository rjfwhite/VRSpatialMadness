using Assets.EntityTemplates;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class PlayerInputBehaviour : MonoBehaviour
    {
        [Require] VivePlayer.Writer vivePlayerWriter;
        public SteamVR_TrackedObject leftHandTrackedObject;
        public SteamVR_TrackedObject rightHandTrackedObject;

        private void Awake()
        {
            leftHandTrackedObject = GameObject.Find("/[CameraRig]/Controller (left)").GetComponent<SteamVR_TrackedObject>();
            leftHandTrackedObject = GameObject.Find("/[CameraRig]/Controller (right)").GetComponent<SteamVR_TrackedObject>();
        }

        private void Update()
        {
            var device = SteamVR_Controller.Input((int)leftHandTrackedObject.index);
            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                Debug.Log("Press Down");
                SpawnBall(leftHand.transform.position);
            }
        }

        private void SpawnBall(Vector3 position)
        {
            Debug.Log(vivePlayerWriter + ", " + EntityTemplateFactory.Ball(new Coordinates(position.x, position.y, position.z)));
            SpatialOS.Commands.CreateEntity(vivePlayerWriter, "Ball", EntityTemplateFactory.Ball(new Coordinates(position.x, position.y, position.z)), callback => {});
        }
    }
}
