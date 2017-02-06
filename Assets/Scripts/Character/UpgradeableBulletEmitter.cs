using fi.tamk.hellgame.dataholders;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace fi.tamk.hellgame.character
{

    public class UpgradeableBulletEmitter : BulletEmitterWithEffects
    {
        private int _collectedUpgradetokens;
        private int _currentWeaponLevel = 0;
        private int _nextWeaponLevelBreakpoint = 0;
        [SerializeField] protected UnityEvent _upgradeEffect;
        public BulletEmitterUpgradeData[] UpgradeData;

        void Start()
        {
            AddToUpgradeBreakPoint();
        }

        public virtual void AddUpgradepoints(int howMany)
        {
            _collectedUpgradetokens += howMany;

            if (_collectedUpgradetokens >= _nextWeaponLevelBreakpoint && UpgradeData.Length > _currentWeaponLevel)
            {
                Cooldown += UpgradeData[_currentWeaponLevel].AddedEmitterCooldown;
                Spread += UpgradeData[_currentWeaponLevel].AddedSpread;
                NumberOfBullets += UpgradeData[_currentWeaponLevel].AddedBulletNumber;
                Dispersion += UpgradeData[_currentWeaponLevel].AddedDispersion;

                BulletSystem.AddToDamageAndSpeed(UpgradeData[_currentWeaponLevel].AddedDamage, UpgradeData[_currentWeaponLevel].AddedSpeed);

                AddToUpgradeBreakPoint();
                _currentWeaponLevel++;

                if (_upgradeEffect == null) return;
                _upgradeEffect.Invoke();
            }
        }

        private void AddToUpgradeBreakPoint()
        {
            if (UpgradeData.Length > _currentWeaponLevel)
            {
                _nextWeaponLevelBreakpoint += UpgradeData[_currentWeaponLevel].UpgradePointLimitIncrease;
            } else
            {
                _nextWeaponLevelBreakpoint = int.MaxValue;
            }           
        }
    }
}
