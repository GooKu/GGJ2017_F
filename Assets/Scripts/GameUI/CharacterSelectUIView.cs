using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUIView : MonoBehaviour
{
    [SerializeField]
    private RectTransform  scroll;
    [SerializeField]
    private CharacterSelectItemView sampleItem;

    private List<CharacterSelectItemView> itemList = new List<CharacterSelectItemView>();
    private float itemWidth;
    private float itemHeigth;

    private void Awake()
    {
        itemWidth = sampleItem.GetComponent<RectTransform>().sizeDelta.x;
        itemHeigth = sampleItem.GetComponent<RectTransform>().sizeDelta.y;

        gameObject.SetActive(false);
    }

    public void Open(CharacterInfo[] characterList)
    {
        gameObject.SetActive(true);
        sampleItem.gameObject.SetActive(false);
        float buttonPosition = 0;

        var contentRoot = sampleItem.transform.parent;
        for (int i = 0; i < characterList.Length; i++)
        {
            CharacterSelectItemView item;
            if (itemList.Count == i)
            {
                GameObject itemObj = Instantiate<GameObject>(sampleItem.gameObject);
                itemObj.transform.SetParent(contentRoot);

                var pos = itemObj.transform.localPosition;
                pos.z = 0;
                itemObj.transform.localPosition = pos;

                item = itemObj.GetComponent<CharacterSelectItemView>();
                itemList.Add(item);

                // 交給 HorizontalLayoutGroup 排位置
                // RectTransform rect = item.GetComponent<RectTransform>();
                // rect.localScale = Vector3.one;
                // buttonPosition += itemWidth * i;
                // rect.anchoredPosition3D = new Vector3(buttonPosition, 0, 0);
            }
            else
            {
                item = itemList[i];
            }

            // var layout = contentRoot.GetComponent<HorizontalLayoutGroup>();
            // layout.enabled = false;

            item.gameObject.SetActive(true);
            item.Refresh(characterList[i]);
        }

        scroll.sizeDelta = new Vector2(buttonPosition+ itemWidth, itemHeigth);
        scroll.anchoredPosition = Vector2.zero;
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
