using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Adminka;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UtilityScripts;

public class InputFieldWords : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputTextPrefab;

    private List<TMP_InputField> createdInputFields = new List<TMP_InputField>();

    public void CreateAndSetInputFields()
    {
        ClearInputFields();
        foreach (string word in GameController.Instance.ChosenWords.Values)
        {
            //GameObject _loadField = Resources.Load("Prefabs/InputText") as GameObject;
            TMP_InputField _inputField = Instantiate(inputTextPrefab);
            _inputField.transform.SetParent(this.transform);
            _inputField.text = Utility.ReplaceStringWithUnderlines(word);
            //EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
            //_inputField.onSelect.AddListener((x) => _inputField.caretPosition = 1);
            //inputField.OnSelect((x) => _inputField.caretPosition = 1);
            //_inputField.onValueChanged.AddListener(delegate { ReplaceBackSlashes(_inputField); });
            _inputField.onValidateInput += delegate(string text, int index, char addedChar)
            {
                ReplaceBackSlashes(_inputField);
                return addedChar;
            };
            createdInputFields.Add(_inputField);
        }

        //EventSystem.current.SetSelectedGameObject(createdInputFields[0].gameObject,); 
        //createdInputFields[0].onSelect.AddListener((x) => createdInputFields[0].caretPosition = 1);
        createdInputFields[0].Select();
        //createdInputFields[0].caretPosition = 1;
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

    public bool CheckCorrectAnswers()
    {
        string[] _allValues = GameController.Instance.ChosenWords.Values.ToArray();
        return !_allValues.Where((t, i) => !createdInputFields[i].text.ToLower().Equals(t.ToLower())).Any();
    }


}
