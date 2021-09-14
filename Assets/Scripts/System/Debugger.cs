using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Debugger : MonoBehaviour
    {
        public static Debugger instance = null;

        public Text text = null;

        private void Start()
        {
            Debugger.instance = this;
        }

        public void SetText(string str)
        {
            this.text.text = str;
        }

        public void AddText(string str)
        {
            this.text.text += str;
        }
    }
}