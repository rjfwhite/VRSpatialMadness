using Improbable.General;
using Improbable.Worker;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;
using UnityEngine;
using Improbable.Unity.Core;

namespace Assets.EntityTemplates
{
    public class EntityTemplateFactory
    {
        public static SnapshotEntity ExampleEntity()
        {
            var entity = new SnapshotEntity { Prefab = "ExampleEntity" };

            entity.Add(new Position.Data(new Coordinates(0, 0, 0)));
            entity.Add(new Name.Data("your_example_entity"));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<Name>(CommonPredicates.VisualOnly);

            entity.SetAcl(acl);

            return entity;
        }

        public static SnapshotEntity Ball(Coordinates position)
        {
            var entity = new SnapshotEntity { Prefab = "Ball" };

            entity.Add(new Position.Data(position));
            entity.Add(new Name.Data(new NameData("ball")));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<Name>(CommonPredicates.VisualOnly);

            entity.SetAcl(acl);

            return entity;
        }

        public static SnapshotEntity GenerateMyPlayer()
        {
            // Set name of Unity prefab associated with this entity
            var entity = new SnapshotEntity { Prefab = "Player" };

            // Define components attached to snapshot entity
            entity.Add(new Position.Data(new Coordinates(Random.Range(-20, 20), 0, Random.Range(-20, 20))));
            entity.Add(new VivePlayer.Data(new ViveTransform(), new ViveTransform(), new ViveTransform()));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<VivePlayer>(CommonPredicates.SpecificClientOnly(SpatialOS.Configuration.EngineId));

            entity.SetAcl(acl);

            return entity;
        }
    }
}
