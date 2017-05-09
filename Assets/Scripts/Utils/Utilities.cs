using fi.tamk.hellgame.input;
using fi.tamk.hellgame.world;
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

        public static void PlayOneShotSound(String Event, Vector3 position)
        {
            if (String.IsNullOrEmpty(Event)) return;

            FMODUnity.RuntimeManager.PlayOneShot(Event, position);
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

        public static string ReturnLocaleStringBasedOnLanguageEnum(SystemLanguage language)
        {
            switch (language)
            {
                case SystemLanguage.English:
                    return Constants.EnglishLocaleString;
                case SystemLanguage.Finnish:
                    return Constants.FinnishLocaleString;
                case SystemLanguage.Spanish:
                    return Constants.SpanishLocaleString;
                default:
                    return Constants.EnglishLocaleString;
                
            }
        }

        public static LegalScenes ReturnLevelSelectScreenBasedOnRoom(LegalScenes scene)
        {
            switch (scene)
            {
                case LegalScenes.Cellar_1:
                case LegalScenes.Cellar_2:
                case LegalScenes.Cellar_3:
                case LegalScenes.SlimeBoss:
                    return LegalScenes.LevelSelect_Wing_1;
                case LegalScenes.Kitchen_1:
                case LegalScenes.Kitchen_2:
                case LegalScenes.Kitchen_3:
                case LegalScenes.WallBoss:
                    return LegalScenes.Kitchen_LevelSelect;
                case LegalScenes.Library_1:
                case LegalScenes.Library_2:
                case LegalScenes.Library_3:
                case LegalScenes.Library_Boss:
                    return LegalScenes.Library_LevelSelect;
                case LegalScenes.Chambers_1:
                case LegalScenes.Chambers_2:
                case LegalScenes.Chambers_3:
                case LegalScenes.Chambers_Boss:
                    return LegalScenes.Chambers_LevelSelect;
                default:
                    return LegalScenes.LevelSelectHub;
            }
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
