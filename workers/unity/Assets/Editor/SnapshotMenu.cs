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

            snapshotBuilder.Add(EntityTemplateFactory.GameManager());
            AddFloorTiles(snapshotBuilder);
            
            snapshotBuilder.SaveSnapshot();
        }

        private static void AddFloorTiles(SnapshotBuilder snapshotBuilder)
        {
            var tileWidth = 10f;
            var width = 10;
            var height = 10;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    snapshotBuilder.Add(EntityTemplateFactory.FloorTile(new Coordinates((-0.5 * width + i) * tileWidth, 0, (-0.5 * height + j) * tileWidth)));
                }
            }
                
        }
    }
}
