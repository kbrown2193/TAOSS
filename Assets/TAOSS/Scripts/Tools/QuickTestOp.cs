#if (UNITY_EDITOR) 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class QuickTestOp : MonoBehaviour
{
    [MenuItem("Tools/Testing/Quick Test Op")]
    private static void CreateQuickTestWindow()
    {
        // Create a new instance of QuickTestData with the specified input fields
        QuickTestData testData = new QuickTestData("NewTest"); // "NewTest" is the default save file name

        // Open the Save File dialog for setting values
        EditorWindow.CreateInstance<QuickTestOPFileWindow>().ShowDialog(testData);
    }

    public static QuickTestData RunTestOperation(QuickTestData quickTestData)
    {
        Debug.Log("Running Test Operation");
        quickTestData.outputXBoolValue = quickTestData.xBoolValue;
        quickTestData.outputYBoolValue = quickTestData.yBoolValue;
        quickTestData.outputXIntValue = quickTestData.xIntValue;
        quickTestData.outputYIntValue = quickTestData.yIntValue;
        quickTestData.outputXFloatValue = quickTestData.xFloatValue;
        quickTestData.outputYFloatValue = quickTestData.yFloatValue;

        quickTestData.outputXIntValue = quickTestData.xIntValue + quickTestData.yIntValue;
        quickTestData.outputYIntValue = quickTestData.xIntValue * quickTestData.yIntValue;

        Debug.Log("outputXIntValue Test Operation output"  + quickTestData.outputXIntValue);
        Debug.Log("outputYIntValue Test Operation output" + quickTestData.outputYIntValue);

        return quickTestData;
    }


    public static void DebugTestOperation(QuickTestData quickTestData)
    {
        Debug.Log("QuickTestOp.TestOperation() bool BASECASE Begin at " + System.DateTime.Now.ToString());
        bool x = false;
        bool y = true;
        Debug.Log("Initial {x{" + x + "} y{" + y + "}}");

        //x ^= y;
        x ^= true;
        Debug.Log("Test_00_00 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_01 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_02 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_03 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_04 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_05 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_06 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_07 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_08 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_09 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_10 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_11 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_12 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_13 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_14 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_15 x ^= false {x{" + x + "} y{" + y + "}}");
        Debug.Log("QuickTestOp.TestOperation() End at " + System.DateTime.Now.ToString());


        Debug.Log("QuickTestOp.TestOperation() Begin at " + System.DateTime.Now.ToString());

        x = quickTestData.xBoolValue;
        y = quickTestData.yBoolValue;

        Debug.Log("Initial {x{" + x + "} y{" + y + "}}");

        //x ^= y;
        x ^= true;
        Debug.Log("Test_00_00 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_01 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_02 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_03 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_04 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_05 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_06 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_07 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_08 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_09 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_10 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_11 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_12 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_13 x ^= false {x{" + x + "} y{" + y + "}}");
        x ^= true;
        Debug.Log("Test_00_14 x ^= true {x{" + x + "} y{" + y + "}}");
        x ^= false;
        Debug.Log("Test_00_15 x ^= false {x{" + x + "} y{" + y + "}}");
        Debug.Log("QuickTestOp.TestOperation() End at " + System.DateTime.Now.ToString());

        // integer testing
        // Test 01 Basecase
        int xInt = 0;
        int yInt = 1;
        int resultInt = 0;
        Debug.Log("QuickTestOp.TestOperation() int BASECASE Begin at " + System.DateTime.Now.ToString());
        Debug.Log("Initial {x{" + xInt + "} y{" + yInt + "}}");


        resultInt ^= yInt;
        Debug.Log("Test_01_00 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_01 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_02 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_03 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_04 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        // Test 01
        xInt = quickTestData.xIntValue;
        yInt = quickTestData.yIntValue;
        resultInt = xInt;
        Debug.Log("QuickTestOp.TestOperation() Int Begin at " + System.DateTime.Now.ToString());
        Debug.Log("Initial {x{" + xInt + "} y{" + yInt + "}}");
        
        //x ^= y;
        resultInt ^= yInt;
        Debug.Log("Test_01_00 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_01 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_02 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_03 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        resultInt ^= yInt;
        Debug.Log("Test_01_04 x ^= y {x{" + xInt + "} y{" + yInt + "}} = " + resultInt);

        Debug.Log("QuickTestOp.TestOperation() Int End at " + System.DateTime.Now.ToString());


        Debug.Log("|= false   does what ???"); // oh is just performs OR (0,1,1,1)
        x = false;
        y = true;
        //x |= y;
        x |= false;
        y |= false;
        Debug.Log("(false |= false) = " + x);
        Debug.Log("(true |= false) = " + y);

        x = false;
        y = true;

        x |= true;
        y |= true;

        Debug.Log("(false |= true) = " + x);
        Debug.Log("(true |= true) = " + y);

        Debug.Log("performs OR (0,1,1,1)");



        Debug.Log("&= false   does what ???"); // oh is just performs AND (0,0,0,1)
        x = false;
        y = true;
        //x |= y;
        x &= false;
        y &= false;
        Debug.Log("(false &= false) = " + x);
        Debug.Log("(true &= false) = " + y);

        x = false;
        y = true;

        x &= true;
        y &= true;

        Debug.Log("(false &= true) = " + x);
        Debug.Log("(true &= true) = " + y);

        Debug.Log("performs AND (0,0,0,1)");
    }

    // Custom Editor Window to set values for QuickTestData
    public class QuickTestOPFileWindow : EditorWindow
    {
        private string testName;
        private bool xBoolValue;
        private bool yBoolValue;
        private int xIntValue;
        private int yIntValue;
        private float xFloatValue;
        private float yFloatValue;

        public QuickTestData ShowDialog(QuickTestData initialData)
        {
            testName = initialData.testName;
            xBoolValue = initialData.xBoolValue;
            yBoolValue = initialData.yBoolValue;
            xIntValue = initialData.xIntValue;
            yIntValue = initialData.yIntValue;
            xFloatValue = initialData.xFloatValue;
            yFloatValue = initialData.yFloatValue;
            ShowUtility();
            return initialData;
        }

        private void OnGUI()
        {
            GUILayout.Label("Test File Settings", EditorStyles.boldLabel);

            // Allow setting values for gameData
            testName = EditorGUILayout.TextField("Test File Name:", testName);
            xBoolValue = EditorGUILayout.Toggle("X Bool:", xBoolValue);
            yBoolValue = EditorGUILayout.Toggle("Y Bool:", yBoolValue);
            xIntValue = EditorGUILayout.IntField("X Int:", xIntValue);
            yIntValue = EditorGUILayout.IntField("Y Int:", yIntValue);
            xFloatValue = EditorGUILayout.FloatField("X Float:", xFloatValue);
            yFloatValue = EditorGUILayout.FloatField("Y Float:", yFloatValue);

            EditorGUILayout.Space();

            if(GUILayout.Button("Debug Run Test"))
            {
                // Create a new instance of GameData with the specified input fields
                QuickTestData newQuickTestData = new QuickTestData(testName);

                newQuickTestData.testName = testName;
                newQuickTestData.xBoolValue = xBoolValue;
                newQuickTestData.yBoolValue = yBoolValue;
                newQuickTestData.xIntValue = xIntValue;
                newQuickTestData.yIntValue = yIntValue;
                newQuickTestData.xFloatValue = xFloatValue;
                newQuickTestData.yFloatValue = yFloatValue;
                DebugTestOperation(newQuickTestData);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Run Test"))
            {
                // Create a new instance of QuickTestData with the specified input fields
                QuickTestData newQuickTestData = new QuickTestData(testName);

                newQuickTestData.testName = testName;
                newQuickTestData.xBoolValue = xBoolValue;
                newQuickTestData.yBoolValue = yBoolValue;
                newQuickTestData.xIntValue = xIntValue;
                newQuickTestData.yIntValue = yIntValue;
                newQuickTestData.xFloatValue = xFloatValue;
                newQuickTestData.yFloatValue = yFloatValue;
                RunTestOperation(newQuickTestData);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button("Run and Save Test"))
            {
                // Create a new instance of QuickTestData with the specified input fields
                QuickTestData newQuickTestData = new QuickTestData(testName);

                newQuickTestData.testName = testName;
                newQuickTestData.xBoolValue = xBoolValue;
                newQuickTestData.yBoolValue = yBoolValue;
                newQuickTestData.xIntValue = xIntValue;
                newQuickTestData.yIntValue = yIntValue;
                newQuickTestData.xFloatValue = xFloatValue;
                newQuickTestData.yFloatValue = yFloatValue;

                newQuickTestData = RunTestOperation(newQuickTestData);

                // Serialize the newQuickTestData and save it to a file using the test name as the save file name
                string json = JsonUtility.ToJson(newQuickTestData);
                string saveFileName = testName + ".json"; // Use testName as the save file name

                // Define the path where the save file will be stored in the same location as Tests
                string saveFolderPath = Path.Combine(Application.persistentDataPath, "Tests");
                string saveFilePath = Path.Combine(saveFolderPath, saveFileName);

                // Create the Tests folder if it doesn't exist
                if (!Directory.Exists(saveFolderPath))
                {
                    Directory.CreateDirectory(saveFolderPath);
                }

                // Write the JSON data to the file
                System.IO.File.WriteAllText(saveFilePath, json);

                // Refresh the Asset Database to make the new save file visible in the Unity Editor
                AssetDatabase.Refresh();

                Debug.Log("Test save file created: " + saveFilePath);

                // Close the editor window
                Close();
            }
        }
    }
}

[System.Serializable]
public class QuickTestData
{
    public string testName;
    public bool xBoolValue;
    public bool yBoolValue;
    public int xIntValue;
    public int yIntValue;
    public float xFloatValue;
    public float yFloatValue;

    public bool outputXBoolValue;
    public bool outputYBoolValue;
    public int outputXIntValue;
    public int outputYIntValue;
    public float outputXFloatValue;
    public float outputYFloatValue;

    // Constructor for QuickTestData
    public QuickTestData(string newtestName)
    {
        testName = newtestName; // this is the name of the save file
        xBoolValue = false;
        yBoolValue = true;
        xIntValue = 0;
        yIntValue = 1;
        xFloatValue = 0f;
        yFloatValue = 1f;
        outputXBoolValue = false;
        outputYBoolValue = false; // set output to all 0s, to see if has ran, or all ones?
        outputXIntValue = 0;
        outputYIntValue = 0;
        outputXFloatValue = 0f;
        outputYFloatValue = 0f;
    }
}
#endif