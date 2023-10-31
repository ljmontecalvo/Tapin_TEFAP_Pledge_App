using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;

public class PythonPassthrough : MonoBehaviour
{
    public const string SAVE_SEPARATOR = ",";

    public TMP_InputField nameInput;

    public void SaveForPassthrough()
    {
        string[] fullName = nameInput.text.Split(" ");

        string saveString = string.Join(SAVE_SEPARATOR, fullName);
        File.WriteAllText("passthrough.txt", saveString);
    }
}
