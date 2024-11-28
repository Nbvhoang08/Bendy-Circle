using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChecker : MonoBehaviour
{
    
    public LineRenderer Line1;
    public LineRenderer Line2;
    public List<LineRenderer> intersectingLines = new List<LineRenderer>();
    private bool finishedCheckingIntersections = false;

    void Update()
    {
        if (!finishedCheckingIntersections)
        {
            CheckForIntersectingLines();
        }   
  
    }

    void CheckForIntersectingLines()
    {
        //intersectingLines.Clear();
     
        // Lấy danh sách các LineRenderer trong phạm vi
        LineRenderer[] allLines = FindObjectsOfType<LineRenderer>();

        bool foundIntersectingLine = false;
        for (int i = 0; i < allLines.Length; i++)
        {
            if (allLines[i] != Line1 && allLines[i] != Line2)
            {
                // Kiểm tra nếu đường này cắt qua cả 2 đường LineRenderer của đối tượng chính
                if (IntersectsWithBothLines(allLines[i]))
                {
                    // Kiểm tra điều kiện về sorting order
                    int line1SortOrder = Line1.sortingOrder;
                    int line2SortOrder = Line2.sortingOrder;
                    int lineSortOrder = allLines[i].sortingOrder;

                    if ((line1SortOrder < lineSortOrder && lineSortOrder < line2SortOrder) || 
                        (line2SortOrder < lineSortOrder && lineSortOrder < line1SortOrder))
                    {
                        if(!intersectingLines.Contains(allLines[i])){
                            intersectingLines.Add(allLines[i]);
                            foundIntersectingLine = true;
                        }
                       
                    }
                }
            }
        }

        if (!foundIntersectingLine)
        {
            finishedCheckingIntersections = true;
            
        }
    }


    bool IntersectsWithBothLines(LineRenderer line)
    {
        Vector3[] line1Positions = new Vector3[Line1.positionCount];
        Line1.GetPositions(line1Positions);

        Vector3[] line2Positions = new Vector3[Line2.positionCount];
        Line2.GetPositions(line2Positions);

        Vector3[] linePositions = new Vector3[line.positionCount];
        line.GetPositions(linePositions);

        bool intersectsLine1 = false;
        bool intersectsLine2 = false;

        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            Vector3 lineStart = linePositions[i];
            Vector3 lineEnd = linePositions[i + 1];

            // Kiểm tra giao với Line1
            for (int j = 0; j < line1Positions.Length - 1; j++)
            {
                Vector3 line1Start = line1Positions[j];
                Vector3 line1End = line1Positions[j + 1];
                if (LinesIntersect(lineStart, lineEnd, line1Start, line1End))
                {
                    intersectsLine1 = true;
                    break;
                }
            }

            // Kiểm tra giao với Line2
            for (int j = 0; j < line2Positions.Length - 1; j++)
            {
                Vector3 line2Start = line2Positions[j];
                Vector3 line2End = line2Positions[j + 1];

            if (LinesIntersect(lineStart, lineEnd, line2Start, line2End))
                {
                    intersectsLine2 = true;
                    break;
                }
            }

            if (intersectsLine1 && intersectsLine2)
                return true;
        }

        return false;
    }


    bool LinesIntersect(Vector3 line1Start, Vector3 line1End, Vector3 line2Start, Vector3 line2End)
    {
        // Kiểm tra giao điểm giữa hai đoạn thẳng
        float denominator = ((line2End.y - line2Start.y) * (line1End.x - line1Start.x)) -
                        ((line2End.x - line2Start.x) * (line1End.y - line1Start.y));
        
        if (denominator == 0)
            return false; // Các đoạn thẳng song song

        float ua = (((line2End.x - line2Start.x) * (line1Start.y - line2Start.y)) -
               ((line2End.y - line2Start.y) * (line1Start.x - line2Start.x))) / denominator;
        float ub = (((line1End.x - line1Start.x) * (line1Start.y - line2Start.y)) -
               ((line1End.y - line1Start.y) * (line1Start.x - line2Start.x))) / denominator;

        return (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1);
    }
}
