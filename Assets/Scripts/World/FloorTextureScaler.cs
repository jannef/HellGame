using UnityEngine;

namespace fi.tamk.hellgame.world
{
    public class FloorTextureScaler : MonoBehaviour
    {
        [Range(0.1f, 10f), SerializeField] private float _tileMultiplierX = 1f;
        [Range(0.1f, 10f), SerializeField] private float _tileMultiplierZ = 1f;
        [SerializeField] private bool _useXForBoth = false;

        private void Awake()
        {
            var material = gameObject.GetComponent<Renderer>().material;
            material.mainTextureScale = new Vector2(gameObject.transform.localScale.x * _tileMultiplierX, gameObject.transform.localScale.z * (_useXForBoth ? _tileMultiplierX :_tileMultiplierZ));
        }
    }
}
