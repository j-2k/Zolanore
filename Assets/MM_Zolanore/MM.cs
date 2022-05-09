using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MM : MonoBehaviour
{
    public static MM instance;

    private void Awake()
    {
      if (instance != null)
      {
          Destroy(this.gameObject);
      }
      else
      {
          instance = this;
          DontDestroyOnLoad(this.gameObject);
      }
    }

    [SerializeField]
    Terrain terrain;

    [SerializeField]
    RectTransform sViewRectTransform;

    [SerializeField]
    RectTransform contentRectTransform;

    [SerializeField]
    MM_Icon mmIconPrefab;

    Matrix4x4 transformationMatrixMap;

    Dictionary<MM_WorldObject, MM_Icon> MM_WorldObjectLookup = new Dictionary<MM_WorldObject, MM_Icon>();

    public void CreateMMWorldObject(MM_WorldObject worldObject)
    {
        MM_Icon mapIcon = Instantiate(mmIconPrefab);
        mapIcon.SetIcon(worldObject.icon);
        mapIcon.SetColor(worldObject.col);
        mapIcon.SetText(worldObject.text);
        mapIcon.SetTextSize(worldObject.textSize);
        MM_WorldObjectLookup[worldObject] = mapIcon;
    }

    public void CalculateTransformations()
    {
        Vector2 mmDimension = contentRectTransform.rect.size;
        Vector2 terrainDimension = new Vector2(terrain.terrainData.size.x, terrain.terrainData.size.z);


        Vector2 scaleRatioOnMM = mmDimension / terrainDimension;
        Vector2 IconTranslations = new Vector2(0, -mmDimension.y / 3);

        transformationMatrixMap = Matrix4x4.TRS(IconTranslations, Quaternion.identity, scaleRatioOnMM);
    }
}
