using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Repositioner
    {
        private GameObject movingObject;
        
        private float baseSpeed;
        
        private float currentSpeed;

        private float temporalSpeed;
        private float temporalSpeedTime;

        public bool IsInstantReposition { get; set; }

        public bool IsMovable { get; set; }

        private Vector3 Position
        {
            get => movingObject.transform.position;
            set => movingObject.transform.position = value; 
        }

        public Vector3 NextPosition { get; set; }

        public void MoveUpdate()
        {
            if (!IsMovable)
            {
                return;
            }
            float time = Time.fixedDeltaTime;
            float speed = UpdateSpeed(time);
            if (IsInstantReposition)
            {
                Position = NextPosition;
                return;
            }
            if (Position != NextPosition)
            {
                Vector3 moveVec = NextPosition - Position;
                float movement = speed * time;
                if (moveVec.magnitude > movement)
                {
                    Position += moveVec.normalized * movement;
                }
                else
                {
                    Position = NextPosition;
                }
            }
        }

        public float UpdateSpeed(float time)
        {
            float returnSpeed;
            if (temporalSpeedTime > time)
            {
                temporalSpeedTime -= time;
                returnSpeed = temporalSpeed;
            }
            else
            {
                temporalSpeedTime = 0f;
                returnSpeed = currentSpeed;
            }
            return returnSpeed;
        }

        public void SetSpeed(float newSpeed) => currentSpeed = newSpeed;

        public void SetTemporalSpeed(float temporalSpeed, float time)
        {
            this.temporalSpeed = temporalSpeed;
            temporalSpeedTime = time;
        }

        public void SetNextPosition(Vector3 newPosition, float speed)
        {
            NextPosition = newPosition;
            currentSpeed = speed;
        }

        public void DiscardSpeed() => currentSpeed = baseSpeed;

        public void SetBaseParams(GameObject movingObject, float baseSpeed)
        {
            this.movingObject = movingObject;
            this.baseSpeed = baseSpeed;
            currentSpeed = baseSpeed;
            
            NextPosition = Position;
            IsInstantReposition = false;
            IsMovable = true;
        }
    }
}