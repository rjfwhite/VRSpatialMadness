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
            rightHandTrackedObject = GameObject.Find("/[CameraRig]/Controller (right)").GetComponent<SteamVR_TrackedObject>();
        }

        private void Update()
        {
            if (leftHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var leftDevice = SteamVR_Controller.Input((int)leftHandTrackedObject.index);
                if (leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    SpawnBall(leftHandTrackedObject.transform.position);
                }
            }

            if (rightHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var rightDevice = SteamVR_Controller.Input((int)rightHandTrackedObject.index);
                if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
                {
                    SpawnBall(rightHandTrackedObject.transform.position);
                }
            }
        }

        private void SpawnBall(Vector3 position)
        {
            Debug.Log(vivePlayerWriter + ", " + EntityTemplateFactory.Ball(new Coordinates(position.x, position.y, position.z)));
            SpatialOS.Commands.CreateEntity(vivePlayerWriter, "Ball", EntityTemplateFactory.Ball(new Coordinates(position.x, position.y, position.z)), callback => {});
        }
    }
}
