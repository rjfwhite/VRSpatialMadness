using Improbable.Server;
using Improbable.Unity;
using Improbable.Unity.Configuration;
using Improbable.Unity.Core;
using Improbable.Worker;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public WorkerConfigurationData Configuration = new WorkerConfigurationData();

    public static string WorkerId;

    public void Start()
    {
        SpatialOS.ApplyConfiguration(Configuration);

        switch (SpatialOS.Configuration.EnginePlatform)
        {
            case EnginePlatform.FSim:
                SpatialOS.OnDisconnected += reason => Application.Quit();

                var targetFramerate = 120;
                var fixedFramerate = 20;

                Application.targetFrameRate = targetFramerate;
                Time.fixedDeltaTime = 1.0f / fixedFramerate;
                break;
            case EnginePlatform.Client:
                Application.targetFrameRate = 120;
                SpatialOS.OnConnected += OnConnected;
                break;
        }

        SpatialOS.Connect(gameObject);
    }

    public void OnConnected()
    {
        Debug.Log("Bootstrap connected to SpatialOS...");
        if(SpatialOS.Configuration.EnginePlatform == EnginePlatform.Client)
        {
            Debug.Log("Trying to spawn...");
            SpatialOS.WorkerCommands.SendCommand(GameManager.Commands.SpawnPlayer.Descriptor, new SpawnPlayerRequest(), new Improbable.EntityId(1), result => 
            {
                if (result.StatusCode == StatusCode.Failure)
                {
                    Debug.LogError("Spawning player failed. " + result.ErrorMessage);
                }
                else
                {
                    Debug.Log("Spawning Player succeeded");
                    WorkerId = result.Response.Value.workerid;
                }
            });
        }
    }
}