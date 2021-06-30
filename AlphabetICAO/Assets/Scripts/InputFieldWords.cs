using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UtilityScripts;
using Zenject;

public class InputFieldWords : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputTextPrefab;

    private List<TMP_InputField> createdInputFields = new List<TMP_InputField>();


    public void CreateAndSetInputFields()
    {
        ClearInputFields();
        foreach (string word in GameController.ChosenWords.Values)
        {
            //GameObject _loadField = Resources.Load("Prefabs/InputText") as GameObject;
            TMP_InputField _inputField = Instantiate(inputTextPrefab);
            _inputField.transform.SetParent(this.transform);
            _inputField.text = Utility.ReplaceStringWithUnderlines(word);
            _inputField.onValidateInput += delegate(string text, int index, char addedChar)
            {
                ReplaceBackSlashes(_inputField);
                return addedChar;
            };
            createdInputFields.Add(_inputField);
        }
        createdInputFields[0].Select();
    }

    private void ReplaceBackSlashes(TMP_InputField inputField)
    {
        if (!inputField.text.Contains("_")) return;
        string _newText = inputField.text.Remove(inputField.text.Length - 1);
        inputField.text = _newText;
    }

    private void ClearInputFields()
    {
        foreach (Transform _child in transform)
            Destroy(_child.gameObject);
        createdInputFields.Clear();
    }

    public bool CheckIfAnswersCorrect()
    {
        string[] _allValues = GameController.ChosenWords.Values.ToArray();
        return !_allValues.Where((t, i) => !createdInputFields[i].text.ToLower().Equals(t.ToLower())).Any();
    }


}
