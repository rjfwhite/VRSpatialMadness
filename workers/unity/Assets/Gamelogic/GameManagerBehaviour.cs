using Assets.EntityTemplates;
using Improbable.Server;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using Improbable.Worker;
using UnityEngine;

namespace Assets.Gamelogic
{
    class GameManagerBehaviour : MonoBehaviour
    {
        [Require] private GameManager.Writer GameManagerWriter;

        private void OnEnable()
        {
            GameManagerWriter.CommandReceiver.OnSpawnPlayer += HandleSpawnPlayer;
        }

        private void HandleSpawnPlayer(Improbable.Entity.Component.ResponseHandle<GameManager.Commands.SpawnPlayer, SpawnPlayerRequest, SpawnPlayerResponse> request)
        {
            Debug.Log("GOT REQUEST TO SPAWN PLAYER");
            int newTeamId = GameManagerWriter.Data.currentTeamId + 1;
            GameManagerWriter.Send(new GameManager.Update().SetCurrentTeamId(newTeamId));

            SpatialOS.WorkerCommands.CreateEntity("Player", EntityTemplateFactory.Player(new Improbable.Math.Coordinates(Random.Range(-20, 15), 0, Random.Range(-20, 20)), request.CallerInfo.CallerWorkerId, newTeamId), callback =>

            {
                if (callback.StatusCode != StatusCode.Success)
                {
                    Debug.LogError("Spawning player failed on fsim " + callback.ErrorMessage);
                }
                else
                {
                    Debug.Log("SUCCESSFULLY SPAWNED PLAYER FOR " + request.CallerInfo.CallerWorkerId);
                    request.Respond(new SpawnPlayerResponse(request.CallerInfo.CallerWorkerId));
                }
            });
        }

        private void OnDisable()
        {
            GameManagerWriter.CommandReceiver.OnSpawnPlayer -= HandleSpawnPlayer;
        }
    }
}
