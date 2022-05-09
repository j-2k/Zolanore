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
        mapIcon.transform.SetParent(contentRectTransform);
        mapIcon.SetIcon(worldObject.icon);
        mapIcon.SetIconScale(worldObject.scaleIcon);
        mapIcon.SetColor(worldObject.col);
        mapIcon.SetText(worldObject.text);
        mapIcon.SetTextSize(worldObject.textSize);
        MM_WorldObjectLookup[worldObject] = mapIcon;
    }

    public void CalculateTransformations()
    {
        Vector2 mmDimension = contentRectTransform.rect.size;
        Vector2 terrainDimension = new Vector2(terrain.terrainData.size.x * 2, terrain.terrainData.size.z);


        Vector2 scaleRatioOnMM = mmDimension / terrainDimension;
        Vector2 IconTranslations = new Vector2(0, (-mmDimension.y/2));// (-mmDimension.y / 3) - 20);

        transformationMatrixMap = Matrix4x4.TRS(IconTranslations, Quaternion.identity, scaleRatioOnMM);
        //  |scaleratio.x,         0,          0,          icoTranslation.x,   |
        //  |0,                scaleratio.y,   0,          icoTranslation.y,   |
        //  |0,                    0,          0,                  0,          |
        //  |0,                    0,          0,                  0,          |
    }

    private void Start()
    {
        CalculateTransformations();
    }

    private void Update()
    {
        UpdateAllIcons();
    }

    void UpdateAllIcons()
    {
        foreach (var kvp in MM_WorldObjectLookup)
        {
            MM_WorldObject mmWO = kvp.Key;
            MM_Icon mmI = kvp.Value;

            //translation
            Vector2 mapIconPos = WorldToMapPosition(mmWO.transform.position);
            mmI.rectTransform.anchoredPosition = mapIconPos;

            //rotation
            Vector3 iconRot = mmWO.transform.rotation.eulerAngles;
            mmI.iconRectTransform.localRotation = Quaternion.AngleAxis(-iconRot.y, Vector3.forward);

            //contentRectTransform.anchoredPosition = new Vector2(-mapIconPos.x, -mapIconPos.y);
        }
    }

    Vector2 WorldToMapPosition(Vector3 worldPos)
    {
        var pos = new Vector2(worldPos.x, worldPos.z);
        return transformationMatrixMap.MultiplyPoint3x4(pos);
    }

}
