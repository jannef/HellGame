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
            Hunting, Summon, HuntAndSummon, QuickLeap
        }

        private JumpPhase _currentPhase;
        private AirSpawnerWithSetSpawnPoints _mySpawner;
        private SpawnerInstruction _spawnerInstance;
        int _phase = 0;
        private int jumpAmount = 3;
        private int spawnedWaveAmount = 0;
        private int totalHuntJumps = 0;
        private float originalShortJumpDelay;

        private SlimeJumpData _longJumpData;
        private SlimeJumpData _shortJumpData;
        private SlimeJumpData _quickLongJump;

        public JumpRepeatState(ActorComponent controlledHero) : base(controlledHero)
        {
            var hc = ControlledActor.GetComponent<HealthComponent>();
            hc.HealthChangeEvent += OnBossHealthChange;
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();

            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();
            _spawnerInstance = GameObject.Instantiate(externalObjects.ScriptableObjects[0]) as SpawnerInstruction;
            _longJumpData = GameObject.Instantiate(externalObjects.ScriptableObjects[1]) as SlimeJumpData;
            _shortJumpData = GameObject.Instantiate(externalObjects.ScriptableObjects[2]) as SlimeJumpData;
            _quickLongJump = GameObject.Instantiate(externalObjects.ScriptableObjects[3]) as SlimeJumpData;
            originalShortJumpDelay = _shortJumpData.JumpDelay;
            _currentPhase = JumpPhase.Hunting;
        }

        private void OnBossHealthChange(float percentage, int health, int maxHealth)
        {
            if (_phase <= 0 && percentage < 0.8f)
            {
                _spawnerInstance.numberOfSpawns++;
                _phase++;
            } else if (_phase <= 1 && percentage < 0.5f) {
                _phase++;

            } else if (_phase <= 2 && percentage < 0.30f)
            {
                _phase++;
                _currentPhase = JumpPhase.HuntAndSummon;
            }
            else if (_phase <= 3 && percentage < 0.1f)
            {
                _phase++;
            }
        }

        public override InputStates StateId
        {
            get { return InputStates.JumpRepeat; }
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
                        jumpAmount = 3;
                        _currentPhase = JumpPhase.Summon;
                        _shortJumpData.JumpDelay = originalShortJumpDelay * 4;
                    }

                    totalHuntJumps++;

                    if (totalHuntJumps == 6)
                    {
                        _longJumpData.JumpDelay = _longJumpData.JumpDelay * 0.8f;
                    }

                    break;
                case JumpPhase.Summon:

                    _mySpawner.Spawn(_spawnerInstance);
                    spawnedWaveAmount++;
                    if (spawnedWaveAmount == 3)
                    {
                        _spawnerInstance.numberOfSpawns++;
                    }

                    _shortJumpData.JumpDelay = originalShortJumpDelay;

                    jumpAmount--;

                    if (jumpAmount <= 0)
                    {
                        jumpAmount = 3;

                        if (_phase > 1)
                        {
                            _currentPhase = JumpPhase.QuickLeap;
                        } else
                        {
                            _currentPhase = JumpPhase.Hunting;
                        }
                        
                    }


                    break;
                case JumpPhase.HuntAndSummon:

                    jumpAmount--;

                    if (jumpAmount % 2 == 1) _mySpawner.Spawn(_spawnerInstance);

                    if (jumpAmount <= 0)
                    {
                        jumpAmount = 3;
                        _currentPhase = JumpPhase.Summon;
                    }

                    break;
                case JumpPhase.QuickLeap:

                    if (_phase < 3)
                    {
                        _currentPhase = JumpPhase.Hunting;
                    } else
                    {
                        _currentPhase = JumpPhase.HuntAndSummon;
                    }

                    
                    break;
            }

            if (_phase > 0)
            {
                ControlledActor.FireGunByIndex(0);
                if (_phase > 3)
                {
                    ControlledActor.FireGunByIndex(1);
                }
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);

            switch (_currentPhase)
            {
                case JumpPhase.Hunting:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position), _longJumpData));
                    break;
                case JumpPhase.Summon:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ControlledActor.transform, _shortJumpData));
                    break;
                case JumpPhase.HuntAndSummon:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position), _longJumpData));
                    break;
                case JumpPhase.QuickLeap:
                    ControlledActor.GoToState(new SlimeJumpingState(ControlledActor, ServiceLocator.Instance.GetNearestPlayer(ControlledActor.transform.position), _quickLongJump));
                    break;
            }
            
        }
    }
}
