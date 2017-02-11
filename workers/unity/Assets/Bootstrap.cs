using Assets.EntityTemplates;
using Improbable.Server;
using Improbable.Unity;
using Improbable.Unity.Configuration;
using Improbable.Unity.Core;
using UnityEngine;

// Placed on a gameobject in client scene to execute connection logic on client startup
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
            SpatialOS.WorkerCommands.SendCommand(GameManager.Commands.SpawnPlayer.Descriptor, new SpawnPlayerRequest(), new Improbable.EntityId(1), result => {
                WorkerId = result.Response.Value.workerid;
            });
        }
    }
}