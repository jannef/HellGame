using System;
using UnityEngine;
using System.Collections;
using fi.tamk.hellgame.dataholders;
using TMPro;

namespace fi.tamk.hellgame.ui
{
    public class HallOfFameBook : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _leftCol;
        [SerializeField] private TextMeshPro _rightCol;
        [SerializeField] private LocaleStrings.StringsEnum[] _roomsToTrack;

        private void GetLine(int forWhichIndex, out float time, out bool hasRecord)
        {
            var roomData = UserStaticData.GetRoomData(forWhichIndex);
            hasRecord = roomData != null;
            time = hasRecord ? roomData.RecordTime : 0f;
        }

        private void GetAllLines(out string labelsCol, out string fieldsCol)
        {
            labelsCol = "";
            fieldsCol = "";
            var numberOfRecords = 0;
            var total = 0f;
            foreach (var room in _roomsToTrack)
            {
                bool hasRecord;
                float time;
                GetLine(UserStaticData.NameToIndex[room], out time, out hasRecord);
                labelsCol += LocaleStrings.LocalizedStringFromEnum(room) + "\n";
                if (hasRecord)
                {
                    total += time;
                    numberOfRecords++;

                    fieldsCol += GameClock.FormatTime(TimeSpan.FromSeconds(time)) + "\n";
                }
                else
                {
                    fieldsCol += GameClock.UnsetTime + "\n";
                }
            }

            if (numberOfRecords == _roomsToTrack.Length)
            {
                labelsCol += LocaleStrings.UI_HIGHSCORE_TOTALTIME;
                fieldsCol += GameClock.FormatTime(TimeSpan.FromSeconds(total));
            }
            else
            {
                labelsCol += LocaleStrings.UI_HIGHSCORE_COMPLETIONPERCENT;
                fieldsCol += string.Format("{0}/{1}", numberOfRecords, _roomsToTrack.Length);
            }
        }

        public void UpdateRanks()
        {
            string left, right;
            GetAllLines(out left, out right);
            _leftCol.text = left;
            _rightCol.text = right;
        }

        private void Awake()
        {
            UpdateRanks();
        }
    }
}
