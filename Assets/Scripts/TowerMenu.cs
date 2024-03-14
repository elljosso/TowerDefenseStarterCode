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

        {

            archerButton.clicked += OnArcherButtonClicked;

        }



        if (swordButton != null)

        {

            swordButton.clicked += OnSwordButtonClicked;

        }



        if (wizardButton != null)

        {

            wizardButton.clicked += OnWizardButtonClicked;

        }



        if (updateButton != null)

        {

            updateButton.clicked += OnUpdateButtonClicked;

        }



        if (destroyButton != null)

        {

            destroyButton.clicked += OnDestroyButtonClicked;

        }



        root.visible = false;

    }



    private void OnArcherButtonClicked()

    {



    }



    private void OnSwordButtonClicked()

    {



    }



    private void OnWizardButtonClicked()

    {



    }



    private void OnUpdateButtonClicked()

    {



    }



    private void OnDestroyButtonClicked()

    {



    }



    private void OnDestroy()

    {

        if (archerButton != null)

        {

            archerButton.clicked -= OnArcherButtonClicked;

        }



        if (swordButton != null)

        {

            swordButton.clicked -= OnSwordButtonClicked;

        }



        if (wizardButton != null)

        {

            wizardButton.clicked -= OnWizardButtonClicked;

        }



        if (updateButton != null)

        {

            updateButton.clicked -= OnUpdateButtonClicked;

        }



        if (destroyButton != null)

        {

            destroyButton.clicked -= OnArcherButtonClicked;

        }

    }
    public void EvaluateMenu()
    {
        // Return als selectedSite gelijk is aan null
        if (selectedSite == null)
            return;

        // Gebruik SetEnabled() functie op elke knop
        SetButtonEnabled(archerButton, false);
        SetButtonEnabled(swordButton, false);
        SetButtonEnabled(wizardButton, false);
        SetButtonEnabled(updateButton, false);
        SetButtonEnabled(destroyButton, false);

        // Bepaal welke knoppen ingeschakeld moeten zijn op basis van het niveau van de geselecteerde bouwplaats
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
        // Wijs de site toe aan de variabele selectedSite
        selectedSite = site;

        // Controleer of de geselecteerde site null is
        if (selectedSite == null)
        {
            // Verberg het menu als de geselecteerde site null is
            root.visible = false;
            return;
        }

        // Zorg ervoor dat het menu zichtbaar is
        root.visible = true;

        // Evalueer het menu op basis van de geselecteerde site
        EvaluateMenu();
    }

    private void SetButtonEnabled(Button button, bool enabled)
    {
        if (button != null)
        {
            button.SetEnabled(enabled);
        }
    }

}