using Assets.EntityTemplates;
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
            snapshotBuilder.Add(EntityTemplateFactory.ExampleEntity());
            snapshotBuilder.SaveSnapshot();
        }
    }
}
