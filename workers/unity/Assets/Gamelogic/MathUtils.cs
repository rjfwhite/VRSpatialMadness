using Improbable.Math;
using UnityEngine;

namespace Assets.Gamelogic
{
    public static class MathUtils
    {
        public static Vector3 ToVector3(this Coordinates coordinates)
        {
            return new Vector3((float)coordinates.X, (float)coordinates.Y, (float)coordinates.Z);
        }

        public static Coordinates ToCoordinates(this Vector3 vector)
        {
            return new Coordinates(vector.x, vector.y, vector.z);
        }
    }
}
