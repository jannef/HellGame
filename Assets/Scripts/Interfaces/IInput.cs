using fi.tamk.hellgame.input;
using UnityEngine;

namespace fi.tamk.hellgame.interfaces
{
    public interface IInput
    {
        Vector3 PollAxisLeft();
        Vector3 PollAxisRight();

        bool PollButton(Buttons.ButtonScheme whichButton);
        bool PollButtonDown(Buttons.ButtonScheme whichButton);
        bool PollButtonUp(Buttons.ButtonScheme whichButton);
    }
}