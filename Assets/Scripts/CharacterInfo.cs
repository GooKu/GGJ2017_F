using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public int Id { get { return id; } }
    [SerializeField]
    public int id;
    public SpriteRenderer Image { get { return image; } }
    [SerializeField]
    public SpriteRenderer image;
    public string Introduction { get { return introduction; } }
    [SerializeField]
    public string introduction;
}
