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
            snapshotBuilder.Add(EntityTemplateFactory.ExampleEntity());
            snapshotBuilder.Add(EntityTemplateFactory.FloorTile(new Coordinates(0,0,0)));

            snapshotBuilder.SaveSnapshot();
        }
    }
}
