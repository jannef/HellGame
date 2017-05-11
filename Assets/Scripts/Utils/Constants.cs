using fi.tamk.hellgame.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace fi.tamk.hellgame.utils
{
    public static class Constants
    {
        public const string PlayerLayerName = "Player";
        public static int PlayerLayer
        {
            get { return LayerMask.NameToLayer(PlayerLayerName); }
        }

        public const string EnemyLayerName = "Enemy";
        public static int EnemyLayer
        {
            get { return LayerMask.NameToLayer(EnemyLayerName); }
        }

        public const string PlayerDashingLayerName = "PlayerIgnoreBullets";
        public static int PlayerDashingLayer
        {
            get { return LayerMask.NameToLayer(PlayerDashingLayerName); }
        }

        public const string ObstacleLayerName = "Obstacle";
        public static int ObstacleLayer
        {
            get { return LayerMask.NameToLayer(ObstacleLayerName); }
        }


        public const string GroundRaycastLayerName = "Ground";

        public static int GroundRaycastlayer
        {
            get { return LayerMask.NameToLayer(GroundRaycastLayerName); }
        }

        public const string WorldPointRaycastLayerName = "CatchRC";

        public static int WorldPointRaycastLayer
        {
            get { return LayerMask.NameToLayer(WorldPointRaycastLayerName); }
        }

        public const float FallingDeathLenght = 2f;

        public static float SmootherstepDerivateEasing(float x)
        {
            return -140 * Mathf.Pow(x - 1, 3) * Mathf.Pow(x, 3);
        }

        public const string SlimeJumpAnimationStateStringName = "SlimeBoss_Jump";
        public const string SlimeStartJumpAnimationTrigger = "StartJump";
        public const string SlimeLandAnimationTrigger = "Land";
        public const string MusicVCAReference = "vca:/music";
        public const string SfxVCAReference = "vca:/sfx";
        public const string UiVCAReference = "vca:/ui";
        public const string EnglishLocaleString = "en_EN";
        public const string FinnishLocaleString = "fi_FI";
        public const string SpanishLocaleString = "es_ES";
        public const string GermanLocaleString = "de_DE";
        public const string PortuegeseLocaleString = "pt_PT";
        public const string FrenchLocaleString = "fr_FR";
        public const string ItalianLocaleString = "it_IT";
        public const float PenaltyTimePerHit = 5f;
        public const float PlayerRunningSpeed = 16f;
        public const float PuddleSlowDownRate = .5f;

        public static MenuActionType[] ActionsToAlwaysSetOnBasicButtons = new MenuActionType[5]
        {
            MenuActionType.Up, MenuActionType.Down, MenuActionType.Left, MenuActionType.Right, MenuActionType.Submit
        };
    }
}
