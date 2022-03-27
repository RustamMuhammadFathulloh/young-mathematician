using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Symmetry
{
    public class PosFinder 
    {


        public static GameObject GetCorrectSquare(Vector3 pos, string side, List<GameObject> list)
        {
            Vector3 correctPos;
            if (side.Equals("Horizontal"))
            {
                correctPos = new Vector3(pos.x * -1, pos.y, pos.z);
            }
            else
            {
                correctPos = new Vector3(pos.x, pos.y * -1, pos.z);
            }
            var correctObj = list
                .Where(x => x.transform.position.Equals(correctPos))
                .Select(x => x.gameObject)
                .FirstOrDefault();
            return correctObj;            
        }


        public static List<int> GetIndexGroup(int maxCoverSquares)
        {
            System.Random rand = new System.Random();
            List<int> possible = Enumerable.Range(0, 6 * 4 - 1).ToList();
            List<int> indexGroup = new List<int>();
            for (int i = 0; i < maxCoverSquares; i++)
            {
                int index = rand.Next(0, possible.Count);
                indexGroup.Add(possible[index]);
                possible.RemoveAt(index);
            }
            return indexGroup;
        }



    }

}

