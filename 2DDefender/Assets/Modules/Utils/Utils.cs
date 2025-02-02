using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Fill the given array by its indexes placed in randomized sequence
        /// </summary>
        /// <param name="arrIndex">the given array</param>
        /// <param name="seed">Use <see langword="null"/> for Random seed</param>
        public static void FillArrayByShuffledIndexes(int[] arrIndex, int? seed = null)
        {
            int numElements = arrIndex.Length;
            List<int> listIndexes = new List<int>(numElements);
            for (int i = 0; i < numElements; i++)
            {
                listIndexes.Add(i);
            }
            System.Random rnd;
            if (seed == null)
                rnd = new System.Random();
            else
                rnd = new System.Random(seed.Value);
            for (int i = numElements; i >= 2; i--)
            {
                int positioninList = rnd.Next(0, i);
                arrIndex[i - 1] = listIndexes[positioninList];
                listIndexes.RemoveAt(positioninList);
            }
            arrIndex[0] = listIndexes[0];
        }
        /// <summary>
        /// Create array and Fill it by its indexes placed in randomized sequence
        /// </summary>
        /// <param name="sizeArr">demanded array size</param>
        /// <param name="seed">Use <see langword="null"/> for Random seed</param>
        /// <returns>final array</returns>
        public static int[] FillArrayByShuffledIndexes(int sizeArr, int? seed = null)
        {
            if (sizeArr > 0)
            {
                int[] newArray = new int[sizeArr];
                FillArrayByShuffledIndexes(newArray, seed);
                return newArray; 
            }
            return null;
        }
        /// <summary>
        /// The Underlaying type of enum may not be int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newInt"></param>
        /// <returns></returns>
        private static bool ConvertAnyEnumUnderlayingTypeToInt32(object obj, out int newInt)
        {
            newInt = 0;
            bool result;
            try
            {
                newInt = Convert.ToInt32(obj);
                result = true;
            }
            catch (Exception)
            {
                Debug.LogError("ConvertAnyEnumUnderlayingTypeToInt32() : Cann't convert");
                result = false;
            }
            return result;
        }

    }
}