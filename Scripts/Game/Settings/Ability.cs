using UnityEngine;
using UnityEngine.UI;


public class Ability : MonoBehaviour
{

	public static Ability instance;
	public Sprite[] newsprite = new Sprite[10];

	private Button button;

	void Start ()
	{
		Ability.instance = this;
		button = GetComponent<Button> ();
		button.image.overrideSprite = newsprite [0];
	}

	public bool isTouchingButton (Vector2 pointTouch)
	{
		Rect buttonR = button.GetComponent<Rect> ();
		if (buttonR.Contains (pointTouch) == true) {
			return true;
		} else {
			return false;
		}
	}

	public void ChangeCostumeSprites (Abilities equippedCostume, ClickState state)
	{
		if (equippedCostume == Abilities.Hide) {
			switch (state) {
			case ClickState.Up:
				button.image.overrideSprite = newsprite [5];
				break;

			case ClickState.Down:
				button.image.overrideSprite = newsprite [6];
				break;

			case ClickState.Disabled:
				button.image.overrideSprite = newsprite [7];
				break;

			case ClickState.Highlight:
				button.image.overrideSprite = newsprite [9];
				break;
			}
				
		} else if (equippedCostume == Abilities.Dash) {
			switch (state) {
			case ClickState.Up:
				button.image.overrideSprite = newsprite [2];
				break;

			case ClickState.Down:
				button.image.overrideSprite = newsprite [3];
				break;

			case ClickState.Disabled:
				button.image.overrideSprite = newsprite [4];
				break;
			case ClickState.Highlight:
				button.image.overrideSprite = newsprite [8];
				break;
			}

		} else {
			switch (state) {
			case ClickState.Up:
				button.image.overrideSprite = newsprite [0];
				break;

			case ClickState.Down:
				button.image.overrideSprite = newsprite [1];
				break;

			case ClickState.Disabled:
				break;
			}

		}


	}
}

public enum ClickState
{
	Up,
	Down,
	Disabled,
	Highlight
}