// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using CsvHelper;
// using System.IO;
// using System.Globalization;
// using System.Linq;
// using System;

// public class MicroplasticsDataSpawner : MonoBehaviour
// {
//     [SerializeField] private GameObject dataPointPrefab;
//     public GameControl gameControl;

//     void Start()
//     {
//         var reader = new StreamReader("./assets/datasets/Marine_Microplastics.csv");
//         var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
//         var marineMicroplatics = csvReader.GetRecords<MicroplaticData>();

//         foreach (MicroplaticData microplaticData in marineMicroplatics)
//         {
//             (Vector3 postion, float measuremeant) = microplaticData.GetPoint();

//             if (measuremeant == 0)
//             {
//                 continue;
//             }

//             var point = Instantiate(dataPointPrefab, postion, Quaternion.identity);
//             gameControl.plasticsOnScreen += 1;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }
// }


// public class MicroplaticData
// {
//     static float radius = 4.2f / 2;
//     public float Longitude { get; set; }
//     public float Latitude { get; set; }
//     public float Measurement { get; set; }
//     public int Year { get; set; }
//     public string DensityClass { get; set; }


//     public (Vector3, float) GetPoint()
//     {
//         float latRadians = Latitude * Mathf.PI / 180;
//         float lngRadians = Longitude * Mathf.PI / 180 + Mathf.PI / 2;

//         float xPos = radius * Mathf.Cos(latRadians) * Mathf.Sin(lngRadians);
//         float zPos = -radius * Mathf.Cos(latRadians) * Mathf.Cos(lngRadians);
//         float yPos = radius * Mathf.Sin(latRadians);

//         var position = new Vector3(xPos, yPos, zPos);

//         return (position, Measurement);
//     }

// }

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
        var reader = new StreamReader("./assets/datasets/Marine_Microplastics_Cleaned.csv");
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

            var scale = dataSizeMapping[microplastic.DensityClass];

            pointObject.transform.localScale = new Vector3(scale, scale, scale);

            yield return new WaitForSeconds(.01f);
        }
    }


}


public class MicroplasticData
{
    static float radius = 4.2f / 2;
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float Measurement { get; set; }
    public int Year { get; set; }
    public int DensityClass { get; set; }


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