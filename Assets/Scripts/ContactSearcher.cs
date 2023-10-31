using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Scripting.Python;
using System.IO;
using System;

public class ContactSearcher : MonoBehaviour
{
    public const string SAVE_SEPARATOR = ",";

    public string firstName;
    public string lastName;
    public string address;
    public string city;
    public string state;
    public string zip;
    public string phone1;
    public string phone2;


    public void RunSearchScript()
    {
        string scriptPath = Path.Combine(Application.dataPath, "Scripts\\ContactSearcher.py");
        PythonRunner.RunFile(scriptPath);

        StartCoroutine(RetrieveInfo());
    }

    private IEnumerator RetrieveInfo()
    {
        yield return new WaitForSeconds(1f);

        string savedString = File.ReadAllText(Path.Combine(Application.dataPath, "..\\passthrough.txt"));
        string[] contents = savedString.Split(new[] { SAVE_SEPARATOR }, System.StringSplitOptions.None);

        firstName = contents[0];
        lastName = contents[1];
        address = contents[2];
        city = contents[3];
        state = contents[4];
        zip = contents[5];
        phone1 = contents[6];
        phone2 = contents[7];
    }
}
