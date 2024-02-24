using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System;

public class MicroplasticsDataSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dataPointPrefab;
    // Start is called before the first frame update
    void Start()
    {
        var reader = new StreamReader("./assets/datasets/Marine_Microplastics.csv");
        var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
        var marineMicroplatics = csvReader.GetRecords<MicroplaticData>();

        foreach (MicroplaticData microplaticData in marineMicroplatics)
        {
            (Vector3 postion, float measuremeant) = microplaticData.GetPoint();

            if (measuremeant == 0)
            {
                continue;
            }

            var point = Instantiate(dataPointPrefab, postion, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


public class MicroplaticData
{
    static float radius = 4.2f / 2;
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float Measurement { get; set; }
    public int Year { get; set; }
    public string DensityClass { get; set; }


    public (Vector3, float) GetPoint()
    {
        float latRadians = Latitude * Mathf.PI / 180;
        float lngRadians = Longitude * Mathf.PI / 180 + Mathf.PI / 2;

        float xPos = radius * Mathf.Cos(latRadians) * Mathf.Sin(lngRadians);
        float zPos = -radius * Mathf.Cos(latRadians) * Mathf.Cos(lngRadians);
        float yPos = radius * Mathf.Sin(latRadians);

        var position = new Vector3(xPos, yPos, zPos);

        return (position, Measurement);
    }

}