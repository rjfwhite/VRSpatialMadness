using Improbable.General;
using Improbable.Math;
using Improbable.Unity.Visualizer;
using UnityEngine;

namespace Assets.Gamelogic
{
    public class ColourVisualizer : MonoBehaviour
    {
        [Require] private Colour.Reader colourReader;

        private void OnEnable()
        {
            SetColour(colourReader.Data.colour);
            colourReader.ComponentUpdated += OnComponentUpdated;
        }

        private void OnDisable()
        {
            colourReader.ComponentUpdated -= OnComponentUpdated;
        }

        void OnComponentUpdated(Colour.Update update)
        {
            if (update.colour.HasValue)
            {
                SetColour(update.colour.Value);
            }
        }

        private void SetColour(Vector3f colour)
        {
            GetComponent<Renderer>().material.color = new Color(colour.X, colour.Y, colour.Z);
        }
    }
}