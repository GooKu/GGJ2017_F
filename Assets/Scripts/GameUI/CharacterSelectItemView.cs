using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectItemView : MonoBehaviour
{
    [SerializeField]
    private Image Img;
    [SerializeField]
    private Text Introduction;

    public int SelectedId { get { return selectId; } }
    private int selectId;

    public void Refresh(CharacterInfo info)
    {
        Img.sprite = info.Image.sprite;
        Introduction.text = info.Introduction;
        selectId = info.Id;
    }
}
