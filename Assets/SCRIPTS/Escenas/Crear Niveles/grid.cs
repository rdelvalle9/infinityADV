using UnityEngine.UI;
using System.Collections;
using UnityEngine;

// la disposicion de los botones de crear el nivel
public class grid : MonoBehaviour
{

    public int col, row;

    private void Start()
    {
        RectTransform parent = gameObject.GetComponent<RectTransform>();
        GridLayoutGroup grid = gameObject.GetComponent<GridLayoutGroup>();

        grid.cellSize = new Vector2(parent.rect.width / col, parent.rect.height / row);
    }


}
