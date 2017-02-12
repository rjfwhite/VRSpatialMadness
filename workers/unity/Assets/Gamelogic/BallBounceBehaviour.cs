
using Improbable.General;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class BallBounceBehaviour : MonoBehaviour
    {
        [Require]
        Velocity.Writer VelocityWriter;
        [Require]
        private Team.Reader teamReader;
        [Require]
        private Colour.Reader colourReader;

        private int bounces = 0;
        private bool collidedThisFrame = false;

        private void OnEnable()
        {
            bounces = 0;
        }

        private void FixedUpdate()
        {
            collidedThisFrame = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.IsEntityObject() && collision.gameObject.name.Contains("FloorTile") && !collidedThisFrame)
            {
                collidedThisFrame = true;
                bounces++;
                if (bounces > 5)
                {
                    SpatialOS.WorkerCommands.DeleteEntity(gameObject.EntityId(), callback => { });
                }
                SpatialOS.WorkerCommands.SendCommand(Team.Commands.SwitchTeam.Descriptor, new SwitchTeamRequest(teamReader.Data.teamId, colourReader.Data.colour), collision.gameObject.EntityId(), callback => { });
            }
        }
    }
}
