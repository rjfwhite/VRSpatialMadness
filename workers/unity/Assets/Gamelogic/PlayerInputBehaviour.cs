using Assets.EntityTemplates;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;
using Valve.VR;

namespace Assets.Gamelogic
{
    class PlayerInputBehaviour : MonoBehaviour
    {
        [Require] VivePlayer.Writer vivePlayerWriter;
        private SteamVR_TrackedObject leftHandTrackedObject;
        private SteamVR_TrackedObject rightHandTrackedObject;
        private ColourVisualizer colourVisualizer;
        private TeamVisualizer teamVisualizer;
        private Vector3 lastLeftHandPosition;
        private float movementSensitivity = 0.2f;
        private GameObject head;
        private GameObject leftHand;
        private GameObject rightHand;

        private void Awake()
        {
            head = GameObject.Find("/[CameraRig]/Camera (eye)");
            leftHand = GameObject.Find("/[CameraRig]/Controller (left)");
            rightHand = GameObject.Find("/[CameraRig]/Controller (right)");

            colourVisualizer = GetComponent<ColourVisualizer>();
            teamVisualizer = GetComponent<TeamVisualizer>();
        }

        private void Update()
        {
            if (!head || !leftHand || !rightHand)
            {
                return;
            }
            
            var leftHandDeltaPosition = leftHand.transform.position - lastLeftHandPosition;
            var leftHandVelocity = leftHandDeltaPosition / Time.deltaTime;
            lastLeftHandPosition = leftHand.transform.position;

            leftHandTrackedObject = leftHand.GetComponent<SteamVR_TrackedObject>();
            rightHandTrackedObject = rightHand.GetComponent<SteamVR_TrackedObject>();

            if (leftHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var leftDevice = SteamVR_Controller.Input((int)leftHandTrackedObject.index);

                // process trigger press
                if (leftDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    SpawnBall(leftHandTrackedObject.transform.position, leftHandVelocity);
                    VibrateLeft();
                }

                // process touchpad
                if (leftDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    ProcessMovement(leftDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad));
                }
            }

            if (rightHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var rightDevice = SteamVR_Controller.Input((int)rightHandTrackedObject.index);

                // process trigger press
                if (rightDevice.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
                {
                    SpawnBall(rightHandTrackedObject.transform.position, Vector3.zero);
                    VibrateRight();
                }

                // process touchpad
                if (rightDevice.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    ProcessMovement(rightDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad));
                }
            }
        }

        private void SpawnBall(Vector3 position, Vector3 velocity)
        {
            SpatialOS.Commands.CreateEntity(vivePlayerWriter, "Ball", EntityTemplateFactory.Ball(new Coordinates(position.x, position.y, position.z), new Vector3f(velocity.x, velocity.y, velocity.z), colourVisualizer.Colour, Bootstrap.WorkerId, teamVisualizer.TeamId), callback => {});
        }

        private void ProcessMovement(Vector2 touchpad)
        {
            if (Mathf.Abs(touchpad.y) > movementSensitivity)
            {
                var movementdirection = Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0) * (transform.forward * Time.deltaTime * (touchpad.y * 5f));
                transform.position += movementdirection;
            }

            if (Mathf.Abs(touchpad.x) > movementSensitivity)
            {
                var movementdirection = Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0) * (transform.right * Time.deltaTime * (touchpad.x * 5f));
                transform.position += movementdirection;
            }
        }

        public void VibrateLeft()
        {
            if (leftHandTrackedObject == null)
            {
                return;
            }

            if (leftHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var leftDevice = SteamVR_Controller.Input((int)leftHandTrackedObject.index);
                leftDevice.TriggerHapticPulse(1000);
            }
        }

        public void VibrateRight()
        {
            if (rightHandTrackedObject == null)
            {
                return;
            }

            if (rightHandTrackedObject.index != SteamVR_TrackedObject.EIndex.None)
            {
                var rightDevice = SteamVR_Controller.Input((int)rightHandTrackedObject.index);
                rightDevice.TriggerHapticPulse(1000);
            }
        }
    }
}
