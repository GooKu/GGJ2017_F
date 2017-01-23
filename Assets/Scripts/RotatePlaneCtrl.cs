using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlaneCtrl : MonoBehaviour {

    private float speed = 100;
    private int dir = -1;
    private int Rotate = 1;
    private float r = 0;
    
    [SerializeField]
    private RotationDir initDir;

    private string dataKey;

    enum RotationDir
    {
        Clockwise = 0,
        CounterClockwise,
        Random,
    }

    void Awake()
    {
        this.dataKey = this.transform.position.ToString() + this.name;

        switch (this.initDir)
        {
            case RotationDir.Clockwise: this.dir = -1; break;
            case RotationDir.CounterClockwise: this.dir = +1; break;
            case RotationDir.Random: this.dir = Random.Range(0, 1) * 2 - 1; break;
        }

        this.LoadGameData();
    }

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //keep rotate
        if (Rotate == 1)
        {
            //if(t > 1)
            //{
            //    dir *= -1;
            //    t = 0;
            //}
            r = gameObject.transform.rotation.z;
            if (r > 0.5 || r < -0.5) { dir *= -1; }
            gameObject.transform.Rotate(new Vector3(0, 0, 1 * dir * speed * Time.deltaTime));
        }
        //t += Time.deltaTime;
    }
    void OnMouseDown()
    {
        if (Rotate == 0) Rotate = 1;
        else if (Rotate == 1) Rotate = 0;

        this.SaveGameData();
    }

    void SaveGameData() {
        var data = new Data()
        {
            Rotate = this.Rotate,
            Rotation = this.transform.localRotation,
        };

        GameDataManager.Instance.SaveLevelTempData(this.dataKey, data);
    }

    void LoadGameData()
    {
        Data data;
        if (GameDataManager.Instance.LoadLevelTempData(this.dataKey, out data))
        {
            this.Rotate = data.Rotate;
            this.transform.localRotation = data.Rotation;
        }
    }

    struct Data
    {
        public int Rotate;
        public Quaternion Rotation;
    }
}
