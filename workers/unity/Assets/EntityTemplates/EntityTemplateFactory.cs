using Improbable.General;
using Improbable.Worker;
using Improbable.Math;
using Improbable.Player;
using Improbable.Unity.Core.Acls;

namespace Assets.EntityTemplates
{
    public class EntityTemplateFactory
    {
        public static SnapshotEntity ExampleEntity()
        {
            var exampleEntity = new SnapshotEntity { Prefab = "ExampleEntity" };

            exampleEntity.Add(new WorldTransform.Data(new WorldTransformData(new Coordinates(0, 0, 0))));
            exampleEntity.Add(new Name.Data(new NameData("your_example_entity")));

            var acl = Acl.Build()
                .SetReadAccess(CommonPredicates.PhysicsOrVisual)
                .SetWriteAccess<WorldTransform>(CommonPredicates.PhysicsOnly)
                .SetWriteAccess<Name>(CommonPredicates.VisualOnly);

            exampleEntity.SetAcl(acl);

            return exampleEntity;
        }
    }
}