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


        public const string GroundRaycastLayerName = "CatchRC";

        public static int GroundRaycastlayer
        {
            get { return LayerMask.NameToLayer(GroundRaycastLayerName); }
        }

        public const float FallingDeathLenght = 2f;
    }
}
