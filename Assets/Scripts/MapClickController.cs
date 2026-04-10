using UnityEngine;
using UnityEngine.EventSystems;

public class MapClickController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Node.CurrentNode != null)
            {
                UIManager.Instance.SetTowerMenu(false);
                Node.CurrentNode = null;
            }

            if (Tower.CurrentTower != null)
            {
                Tower.CurrentTower.SetTowerInfo(false);
                Tower.CurrentTower = null;
            }
        }

    }
}
