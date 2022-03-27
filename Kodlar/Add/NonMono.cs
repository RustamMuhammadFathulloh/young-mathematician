using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using System.Linq;


namespace Add
{
    public class NonMono 
    {


        public static List<int> RandomList(int m, int n)
        {
            int[] arr = new int[m];
            for (int i = 0; i < n; i++)
            {
                arr[Random.Range(0, n) % m]++;
            }
            return arr.ToList();
        }

        public static List<int> MakeRandomIndexGroup(int unitNumber, int maxSquare)
        {
            System.Random rand = new System.Random();
            List<int> possible = Enumerable.Range(0, maxSquare).ToList();
            List<int> indexGroup = new List<int>();
            for (int i = 0; i < unitNumber; i++)
            {
                int index = rand.Next(0, possible.Count);
                indexGroup.Add(possible[index]);
                possible.RemoveAt(index);
            }
            return indexGroup;
        }

        public static void SetBorder(int level, ref float xBorder, ref float yBorder, List<GameObject> squares)
        {
            switch (level)
            {
                case 1:
                    xBorder = squares[1].transform.position.x;
                    yBorder = squares[2].transform.position.y;
                    break;
                case 2:
                    xBorder = squares[1].transform.position.x;
                    yBorder = squares[2].transform.position.y;
                    break;
                case 3:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[3].transform.position.y;
                    break;
                case 4:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[3].transform.position.y;
                    break;
                case 5:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[6].transform.position.y + 0.5f;
                    break;
                case 6:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[6].transform.position.y + 0.5f;
                    break;
                case 7:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[6].transform.position.y + 0.5f;
                    break;
                case 8:
                    xBorder = squares[2].transform.position.x;
                    yBorder = squares[6].transform.position.y + 0.5f;                    
                    break;
            }
        }


    }

}

