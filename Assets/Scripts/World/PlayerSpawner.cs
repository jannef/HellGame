﻿using fi.tamk.hellgame.character;
using fi.tamk.hellgame.dataholders;
using fi.tamk.hellgame.utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{

    public class PlayerSpawner : MonoBehaviour
    {
        private struct PlayerSpawningSequenceData
        {
            public Vector3 _spawnStartPosition;
            public Vector3 _playerEndPosition;
            public ActorComponent _playerActor;
            public PlayerDash _playerDashComponent;
            public float _spawnLenght;
            public GameObject _player;

            public PlayerSpawningSequenceData(Vector3 startingPosition, Vector3 endPosition, ActorComponent actor, PlayerDash dash, float lenght, GameObject player)
            {
                _spawnStartPosition = startingPosition;
                _playerEndPosition = endPosition;
                _playerActor = actor;
                _playerDashComponent = dash;
                _spawnLenght = lenght;
                _player = player;
            }
        }

        [SerializeField] private float _spawnLenght;
        [SerializeField] private AnimationCurve _movementCurve;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private Transform _otherEntrancePointsParent;
        [SerializeField] private bool _waitForEncounterBegin = true;
        private int playerOriginalLayer;
        private PlayerSpawningSequenceData _sequenceData;

        public bool StartSpawning(GameObject player)
        {
            //PlayerSpawnEntrance[] otherSpawnEntrances = _otherEntrancePointsParent.GetComponentsInChildren<PlayerSpawnEntrance>();
            playerOriginalLayer = player.layer;

            var dashComponent = player.GetComponent<PlayerDash>();
            var actorComponent = player.GetComponent<ActorComponent>();

            if (actorComponent == null || dashComponent == null)
            {
                throw new UnityException("PlayerSpawner; PlayerDash or ActorComponent not found in target GameObject");
            }

            StartCoroutine(SpawningRoutine(transform.position, _startPosition.position, actorComponent, dashComponent, _spawnLenght, player));
            return true;
        }

        public void ActivateSpawning()
        {
            StartCoroutine(SpawningRoutine(_sequenceData._spawnStartPosition, _sequenceData._playerEndPosition, 
                _sequenceData._playerActor, _sequenceData._playerDashComponent, _sequenceData._spawnLenght, _sequenceData._player));
        }

        public bool StartSpawning(GameObject player, int previousRoom)
        {

            var dashComponent = player.GetComponent<PlayerDash>();
            var actorComponent = player.GetComponent<ActorComponent>();
            playerOriginalLayer = player.layer;
            player.SetLayer(Constants.EnemyLayer, false);


            if (actorComponent == null || dashComponent == null)
            {
                new UnityException("PlayerSpawner; PlayerDash or ActorComponent not found in target GameObject");
                return false;
            }

            PlayerSpawnEntrance[] otherSpawnEntrances = null;

            if (_otherEntrancePointsParent != null)
            {
                otherSpawnEntrances = _otherEntrancePointsParent.GetComponentsInChildren<PlayerSpawnEntrance>();
            }

            if (otherSpawnEntrances != null)
            {
                for (int i = 0; i < otherSpawnEntrances.Length; i++)
                {
                    if ((int)otherSpawnEntrances[i].previousScene == previousRoom)
                    {
                        StartCoroutine(SpawningRoutine(otherSpawnEntrances[i].transform.position, otherSpawnEntrances[i].StartPoint.position, 
                            actorComponent, dashComponent, _spawnLenght, player));
                        return true;
                    }
                }
            }

            player.transform.position = _startPosition.position;
            
            _sequenceData = new PlayerSpawningSequenceData(transform.position, _startPosition.position, actorComponent, dashComponent, _spawnLenght, player);
            if (_waitForEncounterBegin)
            {
                actorComponent.enabled = false;
                RoomIdentifier.EncounterBegin += ActivateSpawning;
            } else
            {
                ActivateSpawning();
            }
                
            return true;
        }

        private IEnumerator SpawningRoutine(Vector3 endPosition, Vector3 startingPosition, ActorComponent playerActor, PlayerDash dashComponent, float lenght, GameObject player)
        {
            var t = 0f;
            playerActor.enabled = false;
            playerActor.transform.forward = endPosition - startingPosition;
            dashComponent.StartDash();

            while (t <= 1)
            {
                playerActor.transform.position = Vector3.Lerp(startingPosition, endPosition, _movementCurve.Evaluate(t));
                t += Time.deltaTime / lenght;
                yield return null;
            }

            playerActor.enabled = true;
            playerActor.transform.forward = endPosition - startingPosition;
            dashComponent.StopDash();
            player.SetLayer(playerOriginalLayer, false);
            playerActor.transform.position = endPosition;
        }
    }
}
