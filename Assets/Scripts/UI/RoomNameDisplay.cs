using System.Collections;
using TMPro;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{
    public class RoomNameDisplay : MonoBehaviour
    {
        [SerializeField] private float Duration = 5f;
        private GameObject _roomNameBar;
        private TextMeshProUGUI _roomNameText;

        public void Init(string roomName)
        {
            var references = FindObjectOfType<GUIReferences>() ?? new UnityException("GUIReference MonoBehaviour component could not be found in the scene!").Throw<GUIReferences>();
            _roomNameBar = references.RoomNameBar;
            _roomNameText = references.RoomNameText;
            _roomNameText.text = roomName;
        }

        public void DisplayRoomName()
        {
            StartCoroutine(FlashMessage(Duration));
        }

        private IEnumerator FlashMessage(float duration)
        {
            _roomNameBar.SetActive(true);
            yield return new WaitForSeconds(duration);
            _roomNameBar.SetActive(false);
        }
    }
}