using Improbable.General;
using Improbable.Worker;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using UnityEngine;
using Improbable.Unity.Core;

namespace Assets.EntityTemplates
{
    public static class ExampleEntityTemplate
    {
        // Template definition for a Example entity
        public static SnapshotEntity GenerateExampleSnapshotEntityTemplate()
        {
            // Set name of Unity prefab associated with this entity
            var exampleEntity = new SnapshotEntity { Prefab = "ExampleEntity" };

            // Define components attached to snapshot entity
            exampleEntity.Add(new WorldTransform.Data(new WorldTransformData(new Coordinates(0, 0, 0))));
            exampleEntity.Add(new Name.Data(new NameData("your_example_entity")));

            var acl = Acl.Build()
                // Both FSim (server) workers and client workers granted read access over all states
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                // Only FSim workers granted write access over WorldTransform component
                .SetWriteAccess<WorldTransform>(CommonPredicates.PhysicsOnly)
                // Only client workers granted write access over Name component
                .SetWriteAccess<Name>(CommonPredicates.VisualOnly);

            exampleEntity.SetAcl(acl);

            return exampleEntity;
        }

        public static SnapshotEntity GenerateMyPlayer()
        {
            // Set name of Unity prefab associated with this entity
            var exampleEntity = new SnapshotEntity { Prefab = "Player" };

            // Define components attached to snapshot entity
            exampleEntity.Add(new WorldTransform.Data(new WorldTransformData(new Coordinates(Random.Range(-20, 20), 0, Random.Range(-20, 20)))));
            exampleEntity.Add(new VivePlayer.Data(new ViveTransform(), new ViveTransform(), new ViveTransform()));

            var acl = Acl.Build()
                // Both FSim (server) workers and client workers granted read access over all states
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                // Only FSim workers granted write access over WorldTransform component
                .SetWriteAccess<WorldTransform>(CommonPredicates.PhysicsOnly)
                // Only client workers granted write access over Name component
                .SetWriteAccess<VivePlayer>(CommonPredicates.SpecificClientOnly(SpatialOS.Configuration.EngineId));

            exampleEntity.SetAcl(acl);

            return exampleEntity;
        }
    }
}