using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.ui
{

    public static class MouseTextureToggler
    {
        private static Vector2 menuMouseHotSpot = new Vector2(32f, 32f);
        private static Vector2 gameplayMouseHotSpot = new Vector2(32f, 32f);
        private const string menyMouseCursorResourceStringReference = "menuMouseCursor";
        private const string gameplayMouseCursorResourceStringReference = "gameplayMouseCursor";

        public static void ChangeToMenuMouseCursor()
        {
            Texture2D menuMouse = Resources.Load(menyMouseCursorResourceStringReference) as Texture2D;

            if (menuMouse != null)
            {
                Cursor.SetCursor(menuMouse, menuMouseHotSpot, CursorMode.ForceSoftware);
            }
        }

        public static void ChangeToGamePlayCursor()
        {
            Texture2D menuMouse = Resources.Load(gameplayMouseCursorResourceStringReference) as Texture2D;

            if (menuMouse != null)
            {
                Cursor.SetCursor(menuMouse, gameplayMouseHotSpot, CursorMode.ForceSoftware);
            }
        }

    }
}
