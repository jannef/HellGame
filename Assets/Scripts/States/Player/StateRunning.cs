using Assets.Scripts.States.Player;
using fi.tamk.hellgame.interfaces;
using UnityEngine;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.input;

namespace fi.tamk.hellgame.states
{
    public class StateRunning : InputTakingState
    {
        protected PlayerLimitBreak MyLimitBreak;
        private bool _dashBuffered = false;

        public override InputStates StateId
        {
            get { return InputStates.Running; }
        }

        public override TransitionType CheckTransitionLegality(InputStates toWhichState)
        {
            switch (toWhichState)
            {
                case InputStates.Dead:
                    return TransitionType.LegalOneway;
                case InputStates.Running:
                    return TransitionType.Illegal;
                default:
                    return TransitionType.LegalTwoway;
            }
        }

        public override void HandleInput(float deltaTime)
        {
            Debug.Log("HandleInput");
            base.HandleInput(deltaTime);
            RunningMovement(deltaTime);
        }

        protected virtual void RunningMovement(float deltaTime)
        {
            var movementDirection = MyInputController.PollAxisLeft();
            var movementSpeedMultiplier = (1 - MyInputController.PollLeftTrigger() * .55f); 
            var controllerLookInput = MyInputController.PollAxisRight();

            // Aim
            HeroAvatar.transform.LookAt(new Vector3(HeroAvatar.transform.position.x + controllerLookInput.x,
                HeroAvatar.transform.position.y, HeroAvatar.transform.position.z + controllerLookInput.z));
            
            // Walk
            var speed = movementDirection * ControlledActor.ActorNumericData.ActorFloatData[(int) ActorDataMap.Speed] *
                        deltaTime * movementSpeedMultiplier;
            HeroAvatar.Move(speed);
            CharacterAnimator.SetFloat("speed", speed.magnitude);

            // Limit break activation input
            if (MyInputController.PollButtonDown(Buttons.ButtonScheme.LimitBreak) && MyLimitBreak != null && MyLimitBreak.LimitAvailableOrActive)
            {
                MyLimitBreak.ActivateLimitBreak();
                return;
            }

            // Charge shot activation
            if (movementSpeedMultiplier <= .96)
            {
                ControlledActor.GoToState(new StateCharging(ControlledActor));
                return;
            }

            // Dash activation and shooting
            if (MyInputController.PollButtonDown(Buttons.ButtonScheme.Dash) || _dashBuffered)
            {
                _dashBuffered = false;
                ControlledActor.GoToState(new StateDashing(ControlledActor, movementDirection.normalized, movementSpeedMultiplier));
            }
            else if (MyInputController.PollButton(Buttons.ButtonScheme.Fire_1) || MyInputController.MyConfig.InputType == Buttons.InputType.ConsolePleb && controllerLookInput.sqrMagnitude > 0.001f)
            {
                ControlledActor.FireGunByIndex(0);
            }
        }

        public override void OnResumeState()
        {
            base.OnResumeState();
            if (ControlledActor.InputBuffer == Buttons.ButtonScheme.Dash) _dashBuffered = true;
            CharacterAnimator.speed = 1f;
        }

        public override void OnEnterState()
        {
            base.OnEnterState();
            CharacterAnimator.speed = 1f;
        }

        public StateRunning(ActorComponent hero) : base(hero)
        {
            MyLimitBreak = ControlledActor.gameObject.GetComponent<PlayerLimitBreak>();
        }
    }
}
