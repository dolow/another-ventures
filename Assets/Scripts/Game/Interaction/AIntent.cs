using UnityEngine;

namespace Game.Interaction
{
    public abstract class AIntent : MonoBehaviour
    {
        protected int intention = 0;

        protected Vector3 accelerateDirectionRate = Vector3.zero;
        public Vector3 AccelerateDirectionRate
        {
            get
            {
                return this.accelerateDirectionRate;
            }
            protected set
            {
                this.accelerateDirectionRate = value;
            }
        }

        protected Vector3 accelerateTorqueRate = Vector3.zero;
        public Vector3 AccelerateTorqueRate
        {
            get
            {
                return this.accelerateTorqueRate;
            }
            protected set
            {
                this.accelerateTorqueRate = value;
            }
        }

        public bool HasIntent(Semantic semantic)
        {
            return (this.intention & (int)semantic) == (int)semantic;
        }
        public bool HasIntent(int semantics)
        {
            return (this.intention & semantics) > 0;
        }

        public void RemoveIntent(Semantic semantic)
        {
            this.intention &= ~(int)semantic;
        }
        public void RemoveIntent(int semantics)
        {
            this.intention &= ~semantics;
        }

        public void ClearIntent()
        {
            this.intention = 0;
        }

        protected bool ApplySemanticRate(Semantic semantic, float rate = 1.0f)
        {
            switch (semantic)
            {
                case Semantic.AccelerateUp:
                    {
                        this.accelerateDirectionRate.y = rate;
                        return true;
                    }
                case Semantic.AccelerateDown:
                    {
                        this.accelerateDirectionRate.y = -rate;
                        return true;
                    }
                case Semantic.AccelerateLeft:
                    {
                        this.accelerateDirectionRate.x = -rate;
                        return true;
                    }
                case Semantic.AccelerateRight:
                    {
                        this.accelerateDirectionRate.x = rate;
                        return true;
                    }
                case Semantic.AccelerateForward:
                    {
                        this.accelerateDirectionRate.z = rate;
                        return true;
                    }
                case Semantic.AccelerateBackward:
                    {
                        this.accelerateDirectionRate.z = -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateUp:
                    {
                        this.accelerateTorqueRate.x = -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateDown:
                    {
                        this.accelerateTorqueRate.x = rate;
                        return true;
                    }
                case Semantic.AccelerateRotateLeft:
                    {
                        this.accelerateTorqueRate.y = -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateRight:
                    {
                        this.accelerateTorqueRate.y = rate;
                        return true;
                    }
                case Semantic.AccelerateYawLeft:
                    {
                        this.accelerateTorqueRate.z = rate;
                        return true;
                    }
                case Semantic.AccelerateYawRight:
                    {
                        this.accelerateTorqueRate.z = -rate;
                        return true;
                    }
            }

            return false;
        }

        protected bool DecreaseSemanticRate(Semantic semantic, float rate = 1.0f)
        {
            switch (semantic)
            {
                case Semantic.AccelerateUp:
                    {
                        this.accelerateDirectionRate.y -= rate;
                        return true;
                    }
                case Semantic.AccelerateDown:
                    {
                        this.accelerateDirectionRate.y -= -rate;
                        return true;
                    }
                case Semantic.AccelerateLeft:
                    {
                        this.accelerateDirectionRate.x -= -rate;
                        return true;
                    }
                case Semantic.AccelerateRight:
                    {
                        this.accelerateDirectionRate.x -= rate;
                        return true;
                    }
                case Semantic.AccelerateForward:
                    {
                        this.accelerateDirectionRate.z -= rate;
                        return true;
                    }
                case Semantic.AccelerateBackward:
                    {
                        this.accelerateDirectionRate.z -= -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateUp:
                    {
                        this.accelerateTorqueRate.x -= -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateDown:
                    {
                        this.accelerateTorqueRate.x -= rate;
                        return true;
                    }
                case Semantic.AccelerateRotateLeft:
                    {
                        this.accelerateTorqueRate.y -= -rate;
                        return true;
                    }
                case Semantic.AccelerateRotateRight:
                    {
                        this.accelerateTorqueRate.y -= rate;
                        return true;
                    }
                case Semantic.AccelerateYawLeft:
                    {
                        this.accelerateTorqueRate.z -= rate;
                        return true;
                    }
                case Semantic.AccelerateYawRight:
                    {
                        this.accelerateTorqueRate.z -= -rate;
                        return true;
                    }
            }

            return true;
        }

        protected void AddIntent(Semantic semantic)
        {
            this.intention |= (1 << (int)semantic);
        }
        protected void AddIntent(int semantics)
        {
            this.intention |= semantics;
        }
        protected void ClearMovement()
        {
            this.accelerateDirectionRate = Vector3.zero;
            this.accelerateTorqueRate = Vector3.zero;
        }
    }
}