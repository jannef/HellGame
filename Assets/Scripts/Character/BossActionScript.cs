using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace fi.tamk.hellgame.character
{
    public class BossActionScript : MonoBehaviour
    {
        private BossBehaviourMachine _stateMachine;

        void Start()
        {
            _stateMachine = GetComponent<BossBehaviourMachine>().Initialize();
            StartCoroutine(TestBossCycle());
        }

        IEnumerator TestBossCycle()
        {
            float timer = 0;

            _stateMachine.ActivateState(AdversiaryCollectionID.AirDropper);

            while (timer < 5)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0;
            _stateMachine.ActivateState(AdversiaryCollectionID.AttachedBossTurret);

            while (timer < 5)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            timer = 0;
            _stateMachine.ReplaceStates(AdversiaryCollectionID.SideBlasters);

            while (timer < 5)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            StartCoroutine(TestBossCycle());

            yield return null;
        }
    }
}
