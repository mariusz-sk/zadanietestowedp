using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace AgentSimulator.UI
{
    public class TextWindow : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textContent;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            PrintNumbers();
        }

        public void PrintNumbers()
        {
            if (_textContent == null)
                return;

            string message = "";
            for (int i=1; i<=100; i++)
            {
                message += string.Format("{0} {1}{2}\n", i, (i % 3) == 0 ? "Marko " : "", (i % 5) == 0 ? "Polo" : "");
            }

            _textContent.text = message;
        }
    }
}
