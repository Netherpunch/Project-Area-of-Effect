using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
	public Texture2D image;
	public Rect position;

	public const int SLOTCOLUMNSIZE = 10;
	public const int SLOTROWSIZE = 4;

	public  int SLOTPOSX = 17;
	public  int SLOTPOSY = 255;

	public  int SLOTWIDTH = 29;
	public  int SLOTHEIGHT = 29;

	public Slot[,] slots = new Slot[SLOTCOLUMNSIZE,SLOTROWSIZE];

	public List<Item> items = new List<Item>();

	private Item temp;

	private bool test;

	private Vector2 selection;
	private Vector2 preSelection;

	// Use this for initialization
	void Start () 
	{
		test = false;
		SetSlots ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!test) 
		{
			Test();
		}
	}

	void Test()
	{
		AddItem (0, 0, Items.GetArmor(0));
		test = true;
	}

	void OnGUI()
	{
		DrawInventory ();
		DrawSlots ();
		DrawItem ();
		DetectGUIAction ();
		DrawTempItem ();
	}

	void SetSlots()
	{
		for(int x = 0;x < SLOTCOLUMNSIZE; x++)
		{
			for(int y = 0;y < SLOTROWSIZE; y++)
			{
				slots[x,y] = new Slot(new Rect(SLOTPOSX + SLOTWIDTH * x, SLOTPOSY + SLOTHEIGHT * y,
				                              	 SLOTWIDTH, SLOTHEIGHT));

			}
		}
	}

	bool AddItem(int x, int y, Item item)
	{
		for (int sX = 0; sX < item.width; sX++) 
		{
			for(int sY = 0; sY < item.height; sY++)
			{
				if(slots[x,y].occupied)
				{
					Debug.Log("Occupied");
					return false;
				}
			}
		}

		if (x + item.width > SLOTCOLUMNSIZE) 
		{
			Debug.LogWarning("Out of bounds - To Wide");
			return false;
		}
		else if (y + item.height > SLOTROWSIZE) 
		{
			Debug.LogWarning("Out of bounds - To High");
			return false;
		}

		item.x = x;
		item.y = y;

		items.Add (item);

		for (int sX = x; sX < item.width + x; sX++) 
		{
			for(int sY = y; sY < item.height + y; sY++)
			{
				slots[sX,sY].occupied = true;
			}
		}
		return true;
	}

	void RemoveItem(Item item)
	{
		for(int x = item.x; x < item.x + item.width; x++)
		{
			for(int y = item.y; y < item.y + item.height; y++)
			{
				slots[x,y].occupied = false;
			}
		}
		items.Remove (item);
	}

	void DrawItem()
	{
		for (int count = 0; count < items.Count; count++) 
		{
			GUI.DrawTexture(new Rect(SLOTPOSX + position.x + items[count].x * SLOTWIDTH, SLOTPOSY + position.y + items[count].y * SLOTHEIGHT,
			                         items[count].width * SLOTWIDTH, items[count].height * SLOTHEIGHT), items[count].image);
		}
	}

	void DrawTempItem()
	{
		if (temp != null) 
		{
			GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y,
			                         temp.width *  SLOTWIDTH,temp.height * SLOTHEIGHT), temp.image);
		}
	}

	void DrawSlots()
	{
		for(int x = 0;x < SLOTCOLUMNSIZE; x++)
		{
			for(int y = 0;y < SLOTROWSIZE; y++)
			{
				slots[x,y].Draw(position.x, position.y);
				
			}
		}
	}

	void DrawInventory()
	{
		position.x = Screen.width - position.width;
		position.y = Screen.height - position.height;
		GUI.DrawTexture (position, image);
	}

	void DetectGUIAction()
	{
		if (Input.mousePosition.x > position.x && Input.mousePosition.x < position.x + position.width) 
		{
			if(Screen.height - Input.mousePosition.y > position.y && Input.mousePosition.y < position.y + position.height)
			{
				//ClickToMove.busy = true;
				DetectMouseAction();
				return;
			}
		}
		//ClickToMove.busy = false;
	}

	void DetectMouseAction()
	{
		for(int x = 0;x < SLOTCOLUMNSIZE; x++)
		{
			for(int y = 0;y < SLOTROWSIZE; y++)
			{
				Rect slot = new Rect(position.x + slots[x,y].position.x, position.y + slots[x,y].position.y, SLOTWIDTH, SLOTHEIGHT);
				if(slot.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
				{
					//Debug.Log(x + " , " + y);
					if(Event.current.isMouse && Input.GetMouseButtonDown(0))
					{
						selection.x = x;
						selection.y = y;

						for(int i = 0; i < items.Count; i++)
						{
							for(int countX = items[i].x; countX < items[i].x + items[i].width; countX++)
							{
								for(int countY = items[i].y; countY < items[i].y + items[i].height; countY++)
								{
									if(countX == x && countY == y)
									{
										temp = items[i];
										RemoveItem(temp);
										return;
									}
								}
							}
						}
					}
					else if(Event.current.isMouse && Input.GetMouseButtonUp(0))
					{
						preSelection.x = x;
						preSelection.y = y;

						if(preSelection.x != selection.x || preSelection.y != selection.y)
						{
							if(temp != null)
							{
								if(AddItem((int)preSelection.x, (int)preSelection.y, temp))
								{

								}
								else
								{
									AddItem(temp.x, temp.y, temp);
								}
								temp = null;
							}
						}
						else
						{
							AddItem(temp.x, temp.y, temp);
							temp = null;
						}
					}
					return;
				}
			}
		}
	}
}
