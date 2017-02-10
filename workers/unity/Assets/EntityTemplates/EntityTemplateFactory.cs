using Improbable.General;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;

namespace Assets.EntityTemplates
{
    public class EntityTemplateFactory
    {
        public static SnapshotEntity Player(Coordinates position)
        {
            var entity = new SnapshotEntity { Prefab = "Player" };

            entity.Add(new Position.Data(position));
            entity.Add(new Colour.Data(new Vector3f(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value)));
            entity.Add(new VivePlayer.Data(new ViveTransform(), new ViveTransform(), new ViveTransform()));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<VivePlayer>(CommonPredicates.SpecificClientOnly(SpatialOS.Configuration.EngineId))
                .SetWriteAccess<Colour>(CommonPredicates.PhysicsOnly);

            entity.SetAcl(acl);

            return entity;
        }

        public static SnapshotEntity Ball(Coordinates position)
        {
            var entity = new SnapshotEntity { Prefab = "Ball" };

            entity.Add(new Position.Data(position));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly);
            
            entity.SetAcl(acl);

            return entity;
        }

        public static SnapshotEntity FloorTile(Coordinates position)
        {
            var entity = new SnapshotEntity { Prefab = "FloorTile" };

            entity.Add(new Position.Data(position));
            entity.Add(new Colour.Data(new Vector3f(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value)));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<Position>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<Colour>(CommonPredicates.PhysicsOnly);

            entity.SetAcl(acl);

            return entity;
        }
    }
}
