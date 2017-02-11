using UnityEngine;
using UnityEngine.UI;

namespace Assets.Gamelogic
{
    public class GameUiController : MonoBehaviour
    {
        private static Text textField;
        private static string health = "n/a";
        private static string points = "n/a";
        private static string team = "n/a";

        private void Awake()
        {
            textField = GetComponent<Text>();
        }

        public static void SetText(string h, string p, string t)
        {
            health = h;
            points = p;
            team = t;
        }

        public static void SetHealth(string h)
        {
            health = h;
        }

        public static void SetPoints(string p)
        {
            points = p;
        }

        public static void SetTeam(string t)
        {
            team = t;
        }

        private void Update()
        {
            textField.text = "Health: " + health + "\nPoints: " + points + "\nTeam: " + team;
        }
    }
}