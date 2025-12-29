using System;
using System.Collections.Generic;

public class Range
{
    public int MinRow, MinCol, MaxRow, MaxCol;

    public Range(int minRow, int minCol)
    {
        MinRow = minRow;
        MinCol = minCol;
        MaxRow = minRow;
        MaxCol = minCol;
    }

    public void Expand(int row, int col)
    {
        MinRow = Math.Min(MinRow, row);
        MinCol = Math.Min(MinCol, col);
        MaxRow = Math.Max(MaxRow, row);
        MaxCol = Math.Max(MaxCol, col);
    }

    public override string ToString()
    {
        return $"[{MinRow}, {MinCol}] to [{MaxRow}, {MaxCol}]";
    }
}

public class MatrixRangeFinder
{
    private int[,] matrix;
    private bool[,] visited;
    private int n, m;
    private List<Range> ranges;

    public MatrixRangeFinder(int[,] matrix)
    {
        this.matrix = matrix;
        n = matrix.GetLength(0);
        m = matrix.GetLength(1);
        visited = new bool[n, m];
        ranges = new List<Range>();
    }

    public List<Range> FindRanges()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i, j] == 1 && !visited[i, j])
                {
                    var range = new Range(i, j);
                    ExploreRange(i, j, range);
                    ranges.Add(range);
                }
            }
        }
        return ranges;
    }

    private void ExploreRange(int row, int col, Range range)
    {
        if (row < 0 || col < 0 || row >= n || col >= m || visited[row, col] || matrix[row, col] == 0)
            return;

        visited[row, col] = true;
        range.Expand(row, col);

        // Explore in all four directions
        ExploreRange(row - 1, col, range); // up
        ExploreRange(row + 1, col, range); // down
        ExploreRange(row, col - 1, range); // left
        ExploreRange(row, col + 1, range); // right
    }
}

class Program
{
    static void Main()
    {
        var url = "https://learn.acloud.guru:8080/course/introduction-to-azure/learn/3d7274b3-eeca-48c8-b6a9-d4351df1a09a/4004f1c7-2209-450a-b6e4-a7bec62f439c/watch#fragment";
        //var url = "example.com:8080/path/to/resource?query=123#fragment";

        string scheme = string.Empty;
        string host = string.Empty;
        string port = string.Empty;
        string path = string.Empty;
        string query = string.Empty;
        string fragment = string.Empty;

        string remainingUrl = url;

        var schemeSplit = remainingUrl.Split("://");
        if(schemeSplit.Length > 1)
        {
            scheme = schemeSplit[0];
            remainingUrl = schemeSplit[1];
        }

        var hostSplit = remainingUrl.Split(':', '/', '?', '#');
        
        if(hostSplit.Length > 1) 
        {
            host = hostSplit[0];
            remainingUrl = remainingUrl.Substring(host.Length);
        }
        else
        {
            host = remainingUrl;
            return;
        }
 
        if (remainingUrl.StartsWith(":"))
        {
            var portSplit = remainingUrl.Split('/', '?', '#');

            if (portSplit.Length > 1)
            {
                port = portSplit[0].Substring(1);
                remainingUrl = remainingUrl.Substring(port.Length + 1);
            }
            else
            {
                port = remainingUrl;
                return;
            }
        }

        var pathSplit = remainingUrl.Split('?', '#');
        if (pathSplit.Length > 1)
        {
            path = pathSplit[0];
            remainingUrl = remainingUrl.Substring(path.Length);
        }
        else
        {
            path = remainingUrl;
            return;
        }

        if(remainingUrl.StartsWith('?'))
        {
            var fragmentSplit = remainingUrl.Split("#");
            query = fragmentSplit[0].Replace("?", "");
            if (fragmentSplit.Length > 1)
            {
                fragment = fragmentSplit[1];
            }
        }
        else
        {
            fragment = remainingUrl.Replace("#", ""); ;
        }

        Console.WriteLine(scheme);
        Console.WriteLine(host);
        Console.WriteLine(port);
        Console.WriteLine(path);
        Console.WriteLine(query);
        Console.WriteLine(fragment);
    }
}
