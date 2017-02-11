using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TeamSwitcher : MonoBehaviour
    {
        [Require] private Team.Writer teamWriter;
        [Require] private Colour.Writer colourWriter;

        private void OnCollisionEnter(Collision collision)
        {
            if (teamWriter == null || colourWriter == null)
            {
                return;
            }

            var otherTeamVisualizer = collision.gameObject.GetComponent<TeamVisualizer>();
            if (otherTeamVisualizer != null)
            {
                teamWriter.Send(new Team.Update().SetTeamId(otherTeamVisualizer.TeamId));
            }

            var otherColourVisualizer = collision.gameObject.GetComponent<ColourVisualizer>();
            if (otherColourVisualizer != null)
            {
                colourWriter.Send(new Colour.Update().SetColour(otherColourVisualizer.Colour));
            }
        }

        
    }
}