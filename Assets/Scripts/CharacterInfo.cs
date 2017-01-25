using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    // ID will set by CharacterController
    public int Id { get; set; }

    public SpriteRenderer Image { get { return image; } }
    [SerializeField]
    public SpriteRenderer image;
    public string Introduction { get { return introduction; } }
    [SerializeField]
    public string introduction;
}
