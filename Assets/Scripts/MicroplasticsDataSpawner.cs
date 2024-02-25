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
        // Load the CSV file from Resources
        var file = Resources.Load<TextAsset>("Marine_Microplastics_Cleaned");

        if (file != null)
        {
            // Split the CSV content into lines
            string[] lines = file.text.Split('\n');

            // Create a list to store MicroplasticData objects
            List<MicroplasticData> microplastics = new List<MicroplasticData>();

            // Iterate through each line (skipping the header)
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                // Skip empty lines
                if (string.IsNullOrEmpty(line))
                    continue;

                // Split the line into individual CSV fields
                string[] fields = line.Split(',');

                // Ensure the line has the expected number of fields
                if (fields.Length != 5)
                {
                    Debug.LogWarning("Skipping invalid line: " + line);
                    continue;
                }

                // Parse fields and create MicroplasticData object
                MicroplasticData data = new MicroplasticData
                {
                    latitude = float.Parse(fields[0]),
                    longitude = float.Parse(fields[1]),
                    measurement = float.Parse(fields[2]),
                    year = int.Parse(fields[3]),
                    densityClass = int.Parse(fields[4])
                };

                // Add MicroplasticData object to the list
                microplastics.Add(data);
            }
            RandomizeListInPlace(microplastics);

            StartCoroutine(spawnMicroplastics(microplastics));  
        }
        else
        {
            Debug.LogError("CSV file not found or failed to load.");
        }
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
    
    public MicroplasticData() {
        
    }

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