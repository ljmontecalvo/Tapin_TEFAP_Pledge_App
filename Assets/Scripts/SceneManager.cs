using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using TMPro;
using System;
using UnityEditor.Scripting;

public class SceneManager : MonoBehaviour
{
    public GameObject acceptButton;
    public GameObject clearButton;
    public GameObject viewButton;

    public GameObject logo;
    public GameObject formOne;

    public GameObject screenOne;
    public GameObject screenTwo;
    public GameObject screenThree;

    public TMP_Text name;
    public TMP_Text street;
    public TMP_Text city;
    public TMP_Text state;
    public TMP_Text zip;
    public TMP_Text mobilePhone;
    public TMP_Text homePhone;

    public TMP_InputField nameInput;
    public TMP_InputField streetInput;
    public TMP_InputField cityInput;
    public TMP_InputField stateInput;
    public TMP_InputField zipInput;
    public TMP_InputField mobilePhoneInput;
    public TMP_InputField homePhoneInput;

    public ContactSearcher contactSearcher;

    public GameObject edits;
    public GameObject table;

    public void ClearSignaturePad()
    {
        GameObject[] lines = GameObject.FindGameObjectsWithTag("Line");

        foreach (GameObject line in lines)
        {
            Destroy(line);
        }
    }

    public void AcceptSignature()
    {
        StartCoroutine(SaveScreenPNG());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            formOne.SetActive(false);
            logo.SetActive(true);
            ClearSignaturePad();
        } else if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            formOne.SetActive(true);
            logo.SetActive(false);
        }
    }

    IEnumerator SaveScreenPNG()
    {
        acceptButton.SetActive(false);
        clearButton.SetActive(false);
        viewButton.SetActive(false);

        // Read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        // Create a texture in RGB24 format the size of the screen
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read the screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode the bytes in PNG format
        byte[] bytes = ImageConversion.EncodeArrayToPNG(tex.GetRawTextureData(), tex.graphicsFormat, (uint)width, (uint)height);
        Destroy(tex);

        // Write the returned byte array to a file in the project folder
        File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);

        acceptButton.SetActive(true);
        clearButton.SetActive(true);
        viewButton.SetActive(true);

        formOne.SetActive(false);
        ClearSignaturePad();
        logo.SetActive(true);
    }

    public void SearchButton()
    {
        screenOne.SetActive(false);
        screenTwo.SetActive(true);
        screenThree.SetActive(false);

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.5f);

        name.text = contactSearcher.firstName + " " + contactSearcher.lastName;
        nameInput.text = contactSearcher.firstName + " " + contactSearcher.lastName;

        street.text = contactSearcher.address;
        streetInput.text = contactSearcher.address;

        city.text = contactSearcher.city;
        cityInput.text = contactSearcher.city;

        state.text = contactSearcher.state;
        stateInput.text = contactSearcher.state;

        zip.text = contactSearcher.zip;
        zipInput.text = contactSearcher.zip;

        mobilePhone.text = contactSearcher.phone1;
        mobilePhoneInput.text = contactSearcher.phone1;

        homePhone.text = contactSearcher.phone2;
        homePhoneInput.text = contactSearcher.phone2;
    }

    public void EditButton()
    {
        edits.SetActive(true);
    }

    public void SubmitButton()
    {
        string[] fullName = nameInput.text.Split(" ");

        contactSearcher.firstName = fullName[0];
        contactSearcher.lastName = fullName[1];
        contactSearcher.address = streetInput.text;
        contactSearcher.city = cityInput.text;
        contactSearcher.state = stateInput.text;
        contactSearcher.zip = zipInput.text;
        contactSearcher.phone1 = mobilePhoneInput.text;
        contactSearcher.phone2 = homePhoneInput.text;

        screenOne.SetActive(false);
        screenTwo.SetActive(false);
        screenThree.SetActive(true);
    }

    public void TableButton()
    {
        if (table.activeInHierarchy)
        {
            table.SetActive(false);
        } else
        {
            table.SetActive(true);
        }
    }
}
