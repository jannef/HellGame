using fi.tamk.hellgame.input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public static class Utilities
    {
        public static LayerMask GetFiringLayer(LayerMask firerMask)
        {
            return LayerMask.NameToLayer(firerMask == LayerMask.NameToLayer("Player") ? "PlayerBullet" : "EnemyBullet");
        }

        public static Transform[] GetAllTranformsFromChildren(this GameObject go)
        {
            var transformArray = go.GetComponentsInChildren<Transform>();
            Transform[] _returnTranforms;
            if (transformArray.Length <= 1)
            {
                _returnTranforms = null;
            }
            else
            {
                _returnTranforms = new Transform[transformArray.Length-1];

                // This is used to remove the parent from available spawnPoints.
                var availableSpawnPointIndex = 0;
                foreach (var t in transformArray)
                {
                    if (t.GetInstanceID() != go.transform.GetInstanceID()) _returnTranforms[availableSpawnPointIndex++] = t;
                }
            }

            return _returnTranforms;
        }

        public static void DisplacingDamageSplash(Transform source, float radius, int howMuch)
        {
            DisplacingDamageSplash(source, radius, howMuch, LayerMask.GetMask(Constants.PlayerLayerName, Constants.PlayerDashingLayerName));
        }

        public static void DisplacingDamageSplash(Transform source, float radius, int howMuch, LayerMask mask)
        {
            var cols = Physics.OverlapSphere(source.position, radius, mask);

            if (cols.Length <= 0) return;
            var trajectory = ServiceLocator.Instance.MainCameraScript.GetRoomCenter() - source.position;
            trajectory.y = 0f;

            cols.ForEach(x => Pool.Instance.GetHealthComponent(x.gameObject).TakeDisplacingDamage(howMuch, trajectory.normalized * radius * 1.5f));
        }

        public static KeyCode ReturnKeyCodeFromButtonMap(Buttons.ButtonScheme _buttonScheme, ButtonMap map)
        {
            switch (_buttonScheme)
            {
                case Buttons.ButtonScheme.Dash:
                    return map.DashButton;
                case Buttons.ButtonScheme.Fire_1:
                    return map.FireOneButton;
                case Buttons.ButtonScheme.LimitBreak:
                    return map.LimitBreakButton;
                case Buttons.ButtonScheme.Pause:
                    return map.PauseButton;
                default:
                    return KeyCode.None;
            }
        }
    }
}
