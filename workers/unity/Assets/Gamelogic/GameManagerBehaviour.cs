﻿using Assets.EntityTemplates;
using Improbable.Server;
using Improbable.Unity.Core;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    class GameManagerBehaviour : MonoBehaviour
    {
        [Require]
        private GameManager.Writer GameManagerWriter;

        private void OnEnable()
        {
            GameManagerWriter.CommandReceiver.OnSpawnPlayer += HandleSpawnPlayer;
        }

        private void HandleSpawnPlayer(Improbable.Entity.Component.ResponseHandle<GameManager.Commands.SpawnPlayer, SpawnPlayerRequest, SpawnPlayerResponse> request)
        {
            Debug.Log("GOT REQUEST TO SPAWN PLAYER");
            SpatialOS.WorkerCommands.CreateEntity("Player", EntityTemplateFactory.Player(new Improbable.Math.Coordinates(Random.RandomRange(-30, 30), 0, Random.RandomRange(-30, 30)), request.CallerInfo.CallerWorkerId), callback =>
            {
                Debug.Log("SUCCESSFULLY SPAWNED PLAYER FOR " + request.CallerInfo.CallerWorkerId);
                request.Respond(new SpawnPlayerResponse());
            });
        }

        private void OnDisable()
        {
            GameManagerWriter.CommandReceiver.OnSpawnPlayer -= HandleSpawnPlayer;
        }
    }
}
