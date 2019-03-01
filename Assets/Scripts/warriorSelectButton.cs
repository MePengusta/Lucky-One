using UnityEngine;

public class warriorSelectButton : MonoBehaviour {

    public static GameObject selectedWarrior;
    public GameObject warriorPrefab;
    private static warriorSelectButton[] buttonArray;

	void Start () {
        buttonArray = GameObject.FindObjectsOfType<warriorSelectButton>();
	}
	
	private void OnMouseDown()
	{
        foreach (warriorSelectButton thisButton in buttonArray)
        {
            thisButton.GetComponent<SpriteRenderer>().color = Color.black;
        }
        GetComponent<SpriteRenderer>().color = Color.white;
        selectedWarrior = warriorPrefab;
        print(selectedWarrior);

       // Instantiate(selectedWarrior, new Vector2(0, -2), Quaternion.identity);
	}
    public static void makeAllWhite()
    {
        foreach (warriorSelectButton thisButton in buttonArray)
        {
            thisButton.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
