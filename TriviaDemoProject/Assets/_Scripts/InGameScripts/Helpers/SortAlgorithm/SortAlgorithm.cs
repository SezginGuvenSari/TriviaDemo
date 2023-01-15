using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SortAlgorithm
{

    public static void SortAllData(List<Data> allData, int beginIndex, int endIndex)
    {
        if (beginIndex >= endIndex) return;

        var pivot = allData[endIndex].score;
        int j = beginIndex - 1;

        for (int i = beginIndex; i < endIndex; i++)
        {
            if (allData[i].score > pivot)
            {
                j++;
                SwapData(allData, i, j);
            }
        }
        j++;
        SwapData(allData, endIndex, j);

        SortAllData(allData, beginIndex, j - 1);
        SortAllData(allData, j + 1, endIndex);
    }
    private static void SwapData(List<Data> data, int i, int j) => (data[i], data[j]) = (data[j], data[i]);
}
