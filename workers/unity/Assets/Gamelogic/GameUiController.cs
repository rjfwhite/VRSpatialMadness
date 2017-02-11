using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic
{
    public class GameUiController : MonoBehaviour
    {
        private static Text textField;

        private void Awake()
        {
            textField = GetComponent<Text>();
        }

        public static void SetText(string text)
        {
            textField.text = text;
        }
    }
}