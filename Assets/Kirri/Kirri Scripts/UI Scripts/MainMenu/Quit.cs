using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour
{
    Button button;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        button = root.Q<Button>("Quit");

        if (button != null)
            button.clicked += OnButtonClicked;
        else
            Debug.LogError("Button 'Quit' not found in UXML!");
    }

    private void OnButtonClicked()
    {
        Application.Quit();
    }
}
