using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    Button button;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        button = root.Q<Button>("Play");

        if (button != null)
            button.clicked += OnButtonClicked;
        else
            Debug.LogError("Button 'Play' not found in UXML!");
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene("Kirri Dev Scene");
    }
}
