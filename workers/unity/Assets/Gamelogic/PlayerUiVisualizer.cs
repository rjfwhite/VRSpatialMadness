using Improbable.General;
using Improbable.Player;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class PlayerUiVisualizer : MonoBehaviour
    {
        [Require] private PlayerInfo.Writer playerInfoWriter;
        [Require] private Team.Writer teamWriter;
        

        private void Update()
        {
            UpdateGameUi();
        }

        private void UpdateGameUi()
        {
            GameUiController.SetText(playerInfoWriter.Data.health.ToString(), playerInfoWriter.Data.points.ToString(), teamWriter.Data.teamId.ToString());
        }
    }
}