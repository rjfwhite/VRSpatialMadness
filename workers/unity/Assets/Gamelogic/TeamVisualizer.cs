using Improbable.General;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class TeamVisualizer : MonoBehaviour
    {
        [Require] private Team.Reader teamReader;
        public int TeamId { get { return teamReader.Data.teamId; } }
    }
}