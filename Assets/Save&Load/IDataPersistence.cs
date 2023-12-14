using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data); //no ref cause we only need reading data no modify.

    void SaveData(ref GameData data); //used ref because this has to modify and write data.
 
}
