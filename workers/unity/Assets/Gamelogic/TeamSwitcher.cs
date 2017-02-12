using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TeamSwitcher : MonoBehaviour
    {
        [Require] private Team.Writer teamWriter;
        [Require] private Colour.Writer colourWriter;

        private void OnEnable()
        {
            teamWriter.CommandReceiver.OnSwitchTeam += HandleSwitchTeam;
        }

        private void OnDisable()
        {
            teamWriter.CommandReceiver.OnSwitchTeam -= HandleSwitchTeam;
        }

        private void HandleSwitchTeam(Improbable.Entity.Component.ResponseHandle<Team.Commands.SwitchTeam, SwitchTeamRequest, SwitchTeamResponse> obj)
        {
            teamWriter.Send(new Team.Update().SetTeamId(obj.Request.teamId));
            colourWriter.Send(new Colour.Update().SetColour(obj.Request.color));
            obj.Respond(new SwitchTeamResponse());
        }
    }
}