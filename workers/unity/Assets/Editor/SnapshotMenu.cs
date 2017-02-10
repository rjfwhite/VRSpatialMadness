using Assets.EntityTemplates;
using Improbable.Math;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class SnapshotMenu
    {
        [MenuItem("Improbable/Snapshots/Generate Snapshot %#&w")]
        private static void GenerateSnapshot()
        {
            var path = Application.dataPath + "/../../../snapshots/initial_world.snapshot";
            var snapshotBuilder = new SnapshotBuilder(path);

            AddFloorTiles(snapshotBuilder);
            
            snapshotBuilder.SaveSnapshot();
        }

        private static void AddFloorTiles(SnapshotBuilder snapshotBuilder)
        {
            var width = 100;
            var height = 100;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    snapshotBuilder.Add(EntityTemplateFactory.FloorTile(new Coordinates(-0.5 * width + i, 0, -0.5 * height + j)));
                }
            }
                
        }
    }
}
