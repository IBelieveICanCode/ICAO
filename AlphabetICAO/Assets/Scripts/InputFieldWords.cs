using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldWords : MonoBehaviour
{
    [SerializeField]
    TMP_InputField inputTextPrefab;
    List<TMP_InputField> createdInputFields = new List<TMP_InputField>();

    public void CreateAndSetInputFields()
    {
        ClearInputFields();
        foreach (string word in GameController.Instance.ChosenWords.Values)
        {
            //GameObject _loadField = Resources.Load("Prefabs/InputText") as GameObject;
            TMP_InputField _inputField = Instantiate(inputTextPrefab);
            _inputField.transform.SetParent(this.transform);
            _inputField.text = Utility.ReplaceStringWithUnderlines(word);
            createdInputFields.Add(_inputField);
            //_inputField.onValueChanged.AddListener(delegate { ReplaceBackSlashes(_inputField); });
        }
    }

    void ClearInputFields()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
        createdInputFields.Clear();
    }

    public bool CheckCorrectAnswers()
    {
        string[] _allValues = GameController.Instance.ChosenWords.Values.ToArray();
        for (int i = 0; i < _allValues.Length; i++)
        {
            if (!createdInputFields[i].text.ToLower().Equals(_allValues[i].ToLower()))
            {
                return false;
            }
        }
        return true;
    }


}
