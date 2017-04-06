using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using fi.tamk.hellgame.character;
using fi.tamk.hellgame.interfaces;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.world;
using UnityEngine;
using UnityEngine.SceneManagement;
using fi.tamk.hellgame.utils;

namespace fi.tamk.hellgame.states
{
    class TutorialRoom : StateAbstract
    {
        private SpawnerInstruction _singleBarrelSpawn;
        private SpawnerInstruction _limitBreakSpawn;
        private PlayerLimitBreak _playerLimitBreak;
        private AirSpawnerWithSetSpawnPoints _mySpawner;

        private int _activeMinions = 0;
        private int _phase = 0;
        private float _spawnTimer = 0;
        private float _spawnInterval = 1f;
        private int _spawnCap = 6;


        public TutorialRoom(ActorComponent controlledHero) : base(controlledHero)
        {
            var externalObjects = ControlledActor.gameObject.GetComponent<BossExternalObjects>();
           
            _mySpawner = externalObjects.ExistingGameObjects[0].GetComponent<AirSpawnerWithSetSpawnPoints>();

            SceneManager.sceneLoaded += Initialize;

            _singleBarrelSpawn = externalObjects.ScriptableObjects[0] as SpawnerInstruction;
            _limitBreakSpawn = externalObjects.ScriptableObjects[1] as SpawnerInstruction;

            NextWave();
        }

        private void Initialize(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= Initialize;
            var player = ServiceLocator.Instance.GetNearestPlayer(Vector3.zero);
            if (player == null) return;
            _playerLimitBreak = player.GetComponent<PlayerLimitBreak>();
            _playerLimitBreak.LimitBreakActivation.AddListener(PlayerActivatesLimitBreak);
            _playerLimitBreak.PowerUpGained += LimitBreakGained;
        }

        private void LimitBreakGained(float percentage, int collected)
        {
            if (percentage >= 100)
            {
                Debug.Log(percentage);
            }
        }

        private void MinionHasDied()
        {
            _activeMinions--;
            
            if (_activeMinions <= 0)
            {
                NextWave();
            }
        }

        private void EndEncounter()
        {
            RoomIdentifier.OnRoomCompleted();
        }

        private void PlayerActivatesLimitBreak()
        {
            _mySpawner.Spawn(_limitBreakSpawn);
        }

        private void NextWave()
        {
            var array = _mySpawner.Spawn(_singleBarrelSpawn);
            if (array.Length > 0)
            {
                Debug.Log(_activeMinions);
                _activeMinions++;
                array[0].DeathEffect.AddListener(MinionHasDied);
            }
        }

        public override void HandleInput(float deltaTime)
        {
            base.HandleInput(deltaTime);
            if (_spawnTimer >= _spawnInterval && _activeMinions <= _spawnCap)
            {
                _spawnTimer = 0;
                //NextWave();
            }  else
            {
                _spawnTimer += Time.deltaTime;
            }
        }

        protected override void CheckForFalling()
        {
        }

        public override InputStates StateId
        {
            get
            {
                return InputStates.TutorialRoom;
            }
        }
    }
}
