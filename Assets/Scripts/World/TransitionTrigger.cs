using fi.tamk.hellgame.input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class TransitionTrigger : MonoBehaviour
    {
        [SerializeField] protected LegalScenes TargetScene;
        [SerializeField] protected bool InvisibleTransitionTrigger = false;
        [SerializeField] protected bool AutomaticTransitionTrigger = false;
        [SerializeField] protected Color HilightColor;

        private int PlayersInside
        {
            get
            {
                return _playersInside;
            }

            set
            {
                _playersInside = value;
                _active = _playersInside > 0;
            }
        }
        private int _playersInside = 0;

        private bool _active = false;

        private InputController _playerInput;

        private Material _material;

        private Color _originalColor;

        public void Awake()
        {
            if (!InvisibleTransitionTrigger)
            {
                var rend = gameObject.GetComponent<Renderer>() ?? new UnityException("No renderer attached to TransitionTrigger object!").Throw<Renderer>();
                _material = rend.material;
                _originalColor = _material.color;
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (!_active)
            {
                _playerInput = other.transform.parent.gameObject.GetComponent<InputController>() ?? new UnityException("TransitionTrigger triggered by malformed hero: other.transform.parent.gameObject.GetComponent<InputController>() returned null!").Throw<InputController>();
                if (!InvisibleTransitionTrigger) _material.color = HilightColor;
            }

            PlayersInside++;
        }

        public void OnTriggerExit(Collider other)
        {
            PlayersInside--;
            if (!_active)
            {
                _playerInput = null;
                if (!InvisibleTransitionTrigger) _material.color = _originalColor;
            }
        }

        public void Update()
        {
            if (_active && _playerInput != null && (_playerInput.PollButtonDown(Buttons.ButtonScheme.Dash) || AutomaticTransitionTrigger))
            {
                if (TargetScene != LegalScenes.ErrorOrNone)
                    RoomManager.LoadRoom(TargetScene);
                else
                    throw new UnityException("TransitionTrigger with incorrectly set target was activated!");
            }
        }
    }
}
