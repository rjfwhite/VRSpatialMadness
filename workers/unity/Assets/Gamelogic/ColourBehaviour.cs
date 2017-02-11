using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class ColourBehaviour : MonoBehaviour
    {
        [Require] private Colour.Writer colourWriter;

        private void OnCollisionEnter(Collision collision)
        {
            if (colourWriter == null)
            {
                return;
            }

            var otherColourVisualizer = collision.gameObject.GetComponent<ColourVisualizer>();
            if (otherColourVisualizer != null)
            {
                SetColour(otherColourVisualizer.colour);
            }
        }

        private void SetColour(Vector3f colour)
        {
            colourWriter.Send(new Colour.Update().SetColour(colour));
        }
    }
}