using KarioMart.Core;
using UnityEngine;
using UnityEngine.UI;

namespace KarioMart.UI
{
    public class MapSelectorButton : MonoBehaviour
    {
        //TODO: Circular dependency!!!!
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(LoadTargetMap);
        }

        public void LoadTargetMap()
        {
            GameStateManager.Instance.InstantiateTargetMap(gameObject.name);
        }
    }
}