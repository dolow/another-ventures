using System.Collections.Generic;
using UnityEngine;

namespace Game.Interaction
{
    public struct IntentMapKeyboard
    {
        private List<KeyCode> keyCodesCache;
        private Dictionary<KeyCode, Semantic> map;

        public Dictionary<KeyCode, Semantic> Map {
            get {
                if (this.map == null)
                {
                    this.map = new Dictionary<KeyCode, Semantic>();
                }
                return this.map;
            }
            private set { this.map = value; }
        }

        public List<KeyCode> KeyCodes
        {
            get
            {
                if (this.keyCodesCache == null)
                {
                    this.keyCodesCache = new List<KeyCode>();
                    foreach (KeyValuePair<KeyCode, Semantic> kv in this.map)
                    {
                        this.keyCodesCache.Add(kv.Key);
                    }
                }
                return this.keyCodesCache;
            }
        }
    }

    public class IntentKeyboard : AIntent
    {
        public static IntentMapKeyboard DefaultKeyboardMap
        {
            get
            {
                IntentMapKeyboard semantic = new IntentMapKeyboard();
                semantic.Map[KeyCode.I] = Semantic.AccelerateUp;
                semantic.Map[KeyCode.U] = Semantic.AccelerateDown;
                semantic.Map[KeyCode.H] = Semantic.AccelerateLeft;
                semantic.Map[KeyCode.L] = Semantic.AccelerateRight;
                semantic.Map[KeyCode.K] = Semantic.AccelerateForward;
                semantic.Map[KeyCode.J] = Semantic.AccelerateBackward;
                semantic.Map[KeyCode.W] = Semantic.AccelerateRotateUp;
                semantic.Map[KeyCode.S] = Semantic.AccelerateRotateDown;
                semantic.Map[KeyCode.A] = Semantic.AccelerateRotateLeft;
                semantic.Map[KeyCode.D] = Semantic.AccelerateRotateRight;
                semantic.Map[KeyCode.Q] = Semantic.AccelerateYawLeft;
                semantic.Map[KeyCode.E] = Semantic.AccelerateYawRight;

                return semantic;
            }
        }

        private IntentMapKeyboard keySemantic;
        private System.Interaction.Keyboard keyboardInteraction = null;

        private void Awake()
        {
            this.keyboardInteraction = this.gameObject.AddComponent<System.Interaction.Keyboard>();

            this.keySemantic = IntentKeyboard.DefaultKeyboardMap;

            this.keyboardInteraction.OnHoldBegan += this.KeyPressBegan;
            this.keyboardInteraction.OnHolding += this.KeyPressing;
            this.keyboardInteraction.OnHoldEnding += this.KeyPressEnding;

            for (int i = 0; i < this.keySemantic.KeyCodes.Count; i++)
            {
                this.keyboardInteraction.AddListeningKey(this.keySemantic.KeyCodes[i]);
            }
        }

        protected void KeyPressBegan(System.Interaction.Keyboard interaction, KeyCode key)
        {
            if (!this.keySemantic.Map.ContainsKey(key))
            {
                return;
            }

            Semantic semantic = this.keySemantic.Map[key];
            if (this.ApplySemanticRate(semantic, 1.0f))
            {
                this.AddIntent(semantic);
            }
        }

        protected void KeyPressing(System.Interaction.Keyboard interaction, KeyCode key, float duration)
        {
            if (!this.keySemantic.Map.ContainsKey(key))
            {
                return;
            }

            Semantic semantic = this.keySemantic.Map[key];
            if (this.ApplySemanticRate(semantic, 1.0f))
            {
                this.AddIntent(semantic);
            }
        }

        protected void KeyPressEnding(System.Interaction.Keyboard interaction, KeyCode key, float duration)
        {
            if (!this.keySemantic.Map.ContainsKey(key))
            {
                return;
            }

            Semantic semantic = this.keySemantic.Map[key];
            if (this.DecreaseSemanticRate(semantic, 1.0f))
            {
                this.RemoveIntent(semantic); 
            }
        }
    }
}