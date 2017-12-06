using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour {
    public List<string> Controllers;
    int len;
    public int numPlayers = 4;
    public int playersMapped = 0;
    public int[] joys = { -1, -1, -1, -1 };
	void Start () {
         len = Input.GetJoystickNames().Length;


        int j = 0;
        string[] controllers = Input.GetJoystickNames();
        for (int i =0;i <controllers.Length; i++)
        {
            if (!String.IsNullOrEmpty(controllers[i]))
            {
                joys[j] = i;
                j++;
                playersMapped++;
            }
        }
        for(;j<joys.Length;j++)
            joys[j] = -1;

        StartCoroutine(checkControllers());

    }


    public void detectPressedKeyOrButton()
    {
        for (int i = 1;i < 10; i++) {
            if(Input.GetKeyDown("joystick "+i+" button 0")){
                print("joystick "+i);
            }
        }
    }

    // Update is called once per frame
    //void Update () {
    //    Controllers = new List<string>();

    //   foreach( string name in Input.GetJoystickNames())
    //    {
    //        Controllers.Add(name);
    //    }

    //    detectPressedKeyOrButton();

    //}

    private IEnumerator checkControllers()
    {
        while (true)
        {
            bool check = true;
            string[] controllers = Input.GetJoystickNames();
            for (int i = 0; i < joys.Length; i++)
            {
                if(joys[i]!=-1)
                if (String.IsNullOrEmpty(controllers[joys[i]])){
                    check = false;
                    break;
                }
            }

            if (!check || playersMapped < numPlayers)
            {
                Debug.Log("Remapping");
                List<int> availableControllers = new List<int>();
                for (int i = 0; i < controllers.Length; i++)
                {
                    if (!String.IsNullOrEmpty(controllers[i]))
                        availableControllers.Add(i);
                }

                for (int i = 0; i < joys.Length; i++)
                {
                    int index = availableControllers.IndexOf(joys[i]);
                    if (index != -1)
                        availableControllers.RemoveAt(index);
                }

                for (int i = 0; i < joys.Length; i++)
                {
                    if (availableControllers.Count == 0)
                        break;

                    if(joys[i] == -1)
                    {
                        joys[i] = availableControllers[0];
                        availableControllers.RemoveAt(0);
                        playersMapped++;
                    }
                    else if (String.IsNullOrEmpty(controllers[joys[i]]))
                    {
                        joys[i] = availableControllers[0];
                        availableControllers.RemoveAt(0);
                    }
                }

            }
            yield return new WaitForSeconds(1f);

        }

    }
}
