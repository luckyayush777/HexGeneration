﻿using UnityEngine;
using UnityEngine.EventSystems;

public class HexMapEditor : MonoBehaviour {

	public Color[] colors;

	public HexGrid hexGrid;

	int activeElevation;

	Color activeColor;

	private int brushSize;
	private bool applyColor;
	private bool applyElevation = true;

	public void SelectColor (int index) 
	{
		applyColor = index >= 0;
		if(applyColor)
		activeColor = colors[index];
	}

	public void SetElevation (float elevation) {
		activeElevation = (int)elevation;
	}

	void Awake () {
		SelectColor(0);
	}

	void Update () {
		if (
			Input.GetMouseButton(0) &&
			!EventSystem.current.IsPointerOverGameObject()
		) {
			HandleInput();
		}
	}

	void HandleInput () {
		Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(inputRay, out hit)) {
			EditCells(hexGrid.GetCell(hit.point));
		}
	}

	void EditCell (HexCell cell) {
		if (cell)
		{


			if (applyColor)
			{
				cell.Color = activeColor;
			}
			if (applyElevation)
			{
				cell.Elevation = activeElevation;
			}
		}
	}

	private void EditCells(HexCell center)
    {
		int CenterX = center.coordinates.X;
		int centerZ = center.coordinates.Z;
		for(int r = 0, z = centerZ - brushSize; z <= centerZ; z++, r++)
        {
			for(int x = CenterX - r; x <= CenterX + brushSize; x++)
            {
				EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
            }
        }
		for(int r = 0, z = centerZ + brushSize; z > centerZ; z--, r++)
        {
			for(int x = CenterX - brushSize; x <= CenterX + r; x++ )
            {
				EditCell(hexGrid.GetCell(new HexCoordinates(x, z)));
            }
        }
    }

	public void SetApplyElevation(bool toggle)
    {
		applyElevation = toggle;
    }

	public void SetBrushSize(float size)
    {
		brushSize = (int)size;
    }

	public void ShowUI(bool visible)
    {
		hexGrid.ShowUI((visible));
    }
}