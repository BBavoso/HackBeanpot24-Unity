using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System;
using UnityEditor;

public class MicroplasticsDataSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dataPointPrefab;

    private List<MicroplasticData> microplastics;

    public GameControl gameControl;

    // Start is called before the first frame update
    void Start()
    {
        var file = Resources.Load<TextAsset>("Marine_Microplastics_Cleaned");
        byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(file.text);
        var memStream = new MemoryStream(byteArray);
        var reader = new StreamReader(memStream);
        
        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        
        
        var marineMicroplatics = csvReader.GetRecords<MicroplasticData>();
        microplastics = marineMicroplatics.ToList();

        RandomizeListInPlace(microplastics);

        StartCoroutine(spawnMicroplastics(microplastics));    
    }

    void RandomizeListInPlace<T>(List<T> list)
    {
        System.Random rng = new();
        int n = list.Count();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator spawnMicroplastics(List<MicroplasticData> microplastics)
    {
        Dictionary<int, float> dataSizeMapping = new();
        dataSizeMapping.Add(0, .01f);
        dataSizeMapping.Add(1, .05f);
        dataSizeMapping.Add(2, .075f);
        dataSizeMapping.Add(3, .1f);
        dataSizeMapping.Add(4, .15f);
        foreach (var microplastic in microplastics)
        {
            (Vector3 pos, float masurement) = microplastic.GetPoint();
            var pointObject = Instantiate(dataPointPrefab, pos, Quaternion.identity);
            gameControl.totalSpawned += 1;

            var scale = dataSizeMapping[microplastic.densityClass];

            pointObject.transform.localScale = new Vector3(scale, scale, scale);

            yield return new WaitForSeconds(.01f);
        }
    }
}

public class MicroplasticData
{
    static float radius = 4.2f / 2;
    public float longitude { get; set; }
    public float latitude { get; set; }
    public float measurement { get; set; }
    public int year { get; set; }
    public int densityClass { get; set; }

    // Constructor
    public MicroplasticData(float longitude, float latitude, float measurement, int year, int densityClass)
    {
        this.longitude = longitude;
        this.latitude = latitude;
        this.measurement = measurement;
        this.year = year;
        this.densityClass = densityClass;
    }

    // GetPoint method
    public (Vector3, float) GetPoint()
    {
        float latRadians = latitude * Mathf.PI / 180;
        float lngRadians = longitude * Mathf.PI / 180 + Mathf.PI / 2;

        float xPos = radius * Mathf.Cos(latRadians) * Mathf.Sin(lngRadians);
        float zPos = -radius * Mathf.Cos(latRadians) * Mathf.Cos(lngRadians);
        float yPos = radius * Mathf.Sin(latRadians);

        var position = new Vector3(xPos, yPos, zPos);

        return (position, measurement);
    }
}