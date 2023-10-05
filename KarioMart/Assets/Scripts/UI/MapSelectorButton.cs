using KarioMart.Core;
using UnityEngine;

namespace KarioMart.UI
{
    public class MapSelectorButton : MonoBehaviour
    {
        public void LoadTargetMap()
        {
            GameStateManager.Instance.SetTargetMapName(gameObject.name);
            GameStateManager.Instance.SetGameState(GameStateManager.GameState.InGame);
            Destroy(transform.root.gameObject);
        }
    }
}