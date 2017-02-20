using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.utils;
using UnityEngine;
using fi.tamk.hellgame.dataholders;

namespace fi.tamk.hellgame.states
{
    class JumpRepeatState : StateAbstract
    {
        private enum JumpPhase
        {
            Hunting, Summon, HuntAndSummon
        }

        private JumpPhase _currentPhase;
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private SpawnerInstruction _spawnerInstance;
        int _phase = 0;
        private int jumpAmount = 3;

        public JumpRepeatState(ActorComponent controlledHero) : base(controlledHero)
        {
            var hc = ControlledActor.GetComponent<HealthComponent>();
            hc.HealthChangeEvent += OnBossHealthChange;
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();

            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _spawnerInstance = GameObject.Instantiate(externalObjects.ScriptableObjects[0]) as SpawnerInstruction;
            _currentPhase = JumpPhase.Summon;
        }

        private void OnBossHealthChange(float percentage, int health, int maxHealth)
        {
            if (_phase <= 0 && percentage < 0.66f)
            {
                 _phase++;
            } else if (_phase <= 1 && percentage < 0.33f)
            {
                _phase++;
                _currentPhase = JumpPhase.HuntAndSummon;
            }
        }

        public override InputStates StateId
        {
            get { return InputStates.JumpRepeat; }
        }

        protected override void CheckForFalling()
        {
            base.CheckForFalling();
        }

        public override void OnResumeState()
        {
            base.OnResumeState();
            switch (_currentPhase)
            {
                case JumpPhase.Hunting:
                    jumpAmount--;

                    if (jumpAmount <= 0)
                    {
                        jumpAmount = 2;
                        _currentPhase = JumpPhase.Summon;
                    }
                    break;
                case JumpPhase.Summon:

                    _mySpawner.Spawn(_spawnerInstance);

                    jumpAmount--;

                    if (jumpAmount <= 0)
                    {
                        jumpAmount = 3;
                        _currentPhase = JumpPhase.Hunting;
                    }
                    break;
                case JumpPhase.HuntAndSummon:

                    jumpAmount++;

                    if (jumpAmount % 2 == 1) _mySpawner.Spawn(_spawnerInstance);
                    break;
            }

            if (_phase > 0)
            {
                ControlledActor.FireGunByIndex(0);
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            switch (_currentPhase)
            {
                case JumpPhase.Hunting:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position)));
                    break;
                case JumpPhase.Summon:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ControlledActor.transform));
                    break;
                case JumpPhase.HuntAndSummon:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position)));
                    break;
            }
            
        }
    }
}
