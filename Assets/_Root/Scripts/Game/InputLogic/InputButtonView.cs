using UnityEngine;
using JoostenProductions;

namespace Game.InputLogic
{
    internal class InputButtonView : BaseInputView
    {   
        private float axis;
        private void Start()
        {
            UpdateManager.SubscribeToUpdate(Move);
        }

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(Move);



        private void Move()
        {
            if ((axis = Input.GetAxis("Horizontal")) > 0)
                OnRightMove(Speed * Time.deltaTime * axis);
            else
                OnRightMove(Speed * Time.deltaTime * axis);
        }
    }
}