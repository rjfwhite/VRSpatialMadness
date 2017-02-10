using Improbable;
using Improbable.Worker;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.Editor
{
    public class SnapshotBuilder
    {
        private readonly string snapshotPath;
        private int currentEntityId;
        private readonly IDictionary<EntityId, SnapshotEntity> snapshotEntities = new Dictionary<EntityId, SnapshotEntity>();

        public SnapshotBuilder(string path)
        {
            snapshotPath = path;
            currentEntityId = 1;
        }

        public void Add(SnapshotEntity entity)
        {
            snapshotEntities.Add(new EntityId(currentEntityId++), entity);
        }

        public void SaveSnapshot()
        {
            File.Delete(snapshotPath);
            var result = Snapshot.Save(snapshotPath, snapshotEntities);

            if (result.HasValue)
            {
                Debug.LogErrorFormat("Failed to generate initial world snapshot: {0}", result.Value);
            }
            else
            {
                Debug.LogFormat("Successfully generated initial world snapshot at {0} with {1} entities", snapshotPath, currentEntityId);
            }
        }
    }
}
