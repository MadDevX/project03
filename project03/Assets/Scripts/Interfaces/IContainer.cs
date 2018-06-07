using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IContainer
{
    bool Add(ItemDetails item);
    void Remove(ItemDetails item);
    List<ItemDetails> GetItemList();
}
