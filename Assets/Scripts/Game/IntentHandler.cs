using System.Collections;
using System.Collections.Generic;
using Game.Interaction;
using UnityEngine;

namespace Game
{
    public class IntentHandler : MonoBehaviour
    {
        public delegate void Accelerate(Vector3 direction, Vector3 torque);

        public bool keyboard = false;
        public bool touchScreen = false;
        public bool mouse = false;
        public bool ui = false;

        // public UIInteractionRegistry uiInteractionRegistry = null;

        public Accelerate RequestAccelerate = null;

        private List<AIntent> intents = new List<AIntent>();

        public static int AccelerateIntentions
        {
            get
            {
                return (
                    (1 << (int)Semantic.AccelerateUp) |
                    (1 << (int)Semantic.AccelerateDown) |
                    (1 << (int)Semantic.AccelerateForward) |
                    (1 << (int)Semantic.AccelerateBackward) |
                    (1 << (int)Semantic.AccelerateLeft) |
                    (1 << (int)Semantic.AccelerateRight) |

                    (1 << (int)Semantic.AccelerateRotateUp) |
                    (1 << (int)Semantic.AccelerateRotateDown) |
                    (1 << (int)Semantic.AccelerateRotateLeft) |
                    (1 << (int)Semantic.AccelerateRotateRight) |

                    (1 << (int)Semantic.AccelerateYawLeft) |
                    (1 << (int)Semantic.AccelerateYawRight)
                );
            }
            set { }
        }

        private void Awake()
        {
            if (this.keyboard)
            {
                this.intents.Add(this.gameObject.AddComponent<IntentKeyboard>());
            }
            if (this.touchScreen)
            {
                // this.interactionInterfaces.Add(this.gameObject.AddComponent<GameInteractionInterfaceTouch>());
            }
            if (this.mouse)
            {
                // this.interactionInterfaces.Add(this.gameObject.AddComponent<GameInteractionInterfaceMouse>());
            }
            if (this.ui)
            {
                /*
                GameInteractionInterfaceUI interactionMediatorUi = this.gameObject.AddComponent<GameInteractionInterfaceUI>();
                this.interactionInterfaces.Add(interactionMediatorUi);
                interactionMediatorUi.SetUIInteractionRegistry(this.uiInteractionRegistry);
                */
            }
        }

        private void FixedUpdate()
        {
            if (this.HasAnyIntention(AccelerateIntentions))
            {
                this.ClearAnyIntention(AccelerateIntentions);

                Vector3 direction = this.CompositAccelerateDirection();
                Vector3 torque = this.CompositAccelerateTorque();

                this.RequestAccelerate?.Invoke(direction, torque);
            }
        }

        private bool HasAnyIntention(Semantic semantic)
        {
            int flag = 1 << (int)semantic;
            for (int i = 0; i < this.intents.Count; i++)
            {
                if (this.intents[i].HasIntent(flag))
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasAnyIntention(int semantics)
        {
            for (int i = 0; i < this.intents.Count; i++)
            {
                if (this.intents[i].HasIntent(semantics))
                {
                    return true;
                }
            }

            return false;
        }

        private void ClearAnyIntention(Semantic semantic)
        {
            int flag = 1 << (int)semantic;
            for (int i = 0; i < this.intents.Count; i++)
            {
                this.intents[i].RemoveIntent(flag);
            }
        }
        private void ClearAnyIntention(int semantics)
        {
            for (int i = 0; i < this.intents.Count; i++)
            {
                this.intents[i].RemoveIntent(semantics);
            }
        }

        private Vector3 CompositAccelerateDirection()
        {
            Vector3 direction = Vector3.zero;

            for (int i = 0; i < this.intents.Count; i++)
            {
                direction += this.intents[i].AccelerateDirectionRate;
            }

            return direction;
        }
        private Vector3 CompositAccelerateTorque()
        {
            Vector3 torque = Vector3.zero;

            for (int i = 0; i < this.intents.Count; i++)
            {
                torque += this.intents[i].AccelerateTorqueRate;
            }

            return torque;
        }
    }
}