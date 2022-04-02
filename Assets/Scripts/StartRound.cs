using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartRound : MonoBehaviour
{
    public GameObject squarePrefab;

    private PoleDrawer drawer;

    /// <summary>
    /// ����, ���������� ���������� �� ����
    /// </summary>
    private bool poleState = false;
    
    // Start is called before the first frame update
    void Start()
    {
        drawer = new PoleDrawer(squarePrefab);
    }

    private void IsMouseOverUI()
    {

    }

    /// <summary>
    /// ������ ����, �������������� ������ ������
    /// </summary>
    public void Draw()
    {
        if(poleState)
            drawer.ClearPole();

        drawer.DrawPole(10, 20);

        poleState = true;
    }

    

}
