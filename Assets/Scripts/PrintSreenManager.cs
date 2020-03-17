using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintSreenManager : Singleton<PrintSreenManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                string str = GUIUtility.systemCopyBuffer;
                Debug.Log("粘贴 "+str);

            }
        }
    }
}
