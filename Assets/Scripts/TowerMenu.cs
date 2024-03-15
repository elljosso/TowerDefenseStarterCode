using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private ConstructionSite selectedSite;
    private VisualElement root;

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer");
        swordButton = root.Q<Button>("sword");
        wizardButton = root.Q<Button>("wizard");
        updateButton = root.Q<Button>("upgrade");
        destroyButton = root.Q<Button>("destroy");

        if (archerButton != null)
            archerButton.clicked += OnArcherButtonClicked;

        if (swordButton != null)
            swordButton.clicked += OnSwordButtonClicked;

        if (wizardButton != null)
            wizardButton.clicked += OnWizardButtonClicked;

        if (updateButton != null)
            updateButton.clicked += OnUpdateButtonClicked;

        if (destroyButton != null)
            destroyButton.clicked += OnDestroyButtonClicked;

        root.visible = false;
    }

    private void OnDestroy()
    {
        if (archerButton != null)
            archerButton.clicked -= OnArcherButtonClicked;

        if (swordButton != null)
            swordButton.clicked -= OnSwordButtonClicked;

        if (wizardButton != null)
            wizardButton.clicked -= OnWizardButtonClicked;

        if (updateButton != null)
            updateButton.clicked -= OnUpdateButtonClicked;

        if (destroyButton != null)
            destroyButton.clicked -= OnDestroyButtonClicked;
    }

    public void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        SetButtonEnabled(archerButton, false);
        SetButtonEnabled(swordButton, false);
        SetButtonEnabled(wizardButton, false);
        SetButtonEnabled(updateButton, false);
        SetButtonEnabled(destroyButton, false);

        switch (selectedSite.Level)
        {
            case ConstructionSite.SiteLevel.Onbebouwd:
                SetButtonEnabled(archerButton, true);
                SetButtonEnabled(wizardButton, true);
                SetButtonEnabled(swordButton, true);
                break;
            case ConstructionSite.SiteLevel.Level1:
            case ConstructionSite.SiteLevel.Level2:
                SetButtonEnabled(updateButton, true);
                SetButtonEnabled(destroyButton, true);
                break;
            case ConstructionSite.SiteLevel.Level3:
                SetButtonEnabled(destroyButton, true);
                break;
        }
    }

    public void SetSite(ConstructionSite site)
    {
        selectedSite = site;

        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }

        root.visible = true;
        EvaluateMenu();
    }

    private void SetButtonEnabled(Button button, bool enabled)
    {
        if (button != null)
        {
            button.SetEnabled(enabled);
        }
    }

    private void OnArcherButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Archer, ConstructionSite.SiteLevel.Level1);
    }

    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Sword, ConstructionSite.SiteLevel.Level1);
    }

    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Wizard, ConstructionSite.SiteLevel.Level1);
    }

    private void OnUpdateButtonClicked()
    {
        if (selectedSite != null)
        {
            ConstructionSite.SiteLevel newLevel = selectedSite.Level + 1;
            GameManager.Instance.Build(selectedSite.TowerType, newLevel);
        }
    }

    private void OnDestroyButtonClicked()
    {
        if (selectedSite != null)
        {
            GameManager.Instance.Build(selectedSite.TowerType, ConstructionSite.SiteLevel.Onbebouwd);
        }
    }
}