string filePath = "./data/pa_A.txt";
Graph graph = ReadGraphFromFile(filePath);

int gardenIndex = 3; // Chỉ mục của đỉnh sân vườn

int phuonganA = CheckFeasibilityA(graph, gardenIndex, out List<int> pathA);
int phuonganB = CheckFeasibilityB(graph, gardenIndex, out List<int> pathB);

Console.WriteLine("Ket qua(PA-A): " + phuonganA);
Console.WriteLine("Lo trinh: " + string.Join(" -> ", pathA));

Console.WriteLine("Ket qua(PA-B): " + phuonganB);
Console.WriteLine("Lo trinh: " + string.Join(" -> ", pathB));

// Hàm đọc đồ thị từ file
static Graph ReadGraphFromFile(string filePath)
{
    using (var reader = new System.IO.StreamReader(filePath))
    {
        string? firstLine = reader.ReadLine();
        if (firstLine == null) throw new System.Exception("File is empty or has no valid first line.");

        int n = int.Parse(firstLine);
        Graph graph = new Graph(n);

        for (int i = 0; i < n; i++)
        {
            string? line = reader.ReadLine();
            if (line == null) throw new System.Exception($"Line {i + 2} is missing.");
            string[] parts = line.Split(' ');
            int count = int.Parse(parts[0]);

            if (count + 1 != parts.Length)
            {
                throw new System.Exception($"Invalid format in line {i + 2}: expected {count + 1} parts but got {parts.Length}.");
            }

            for (int j = 1; j <= count; j++)
            {
                int neighbor = int.Parse(parts[j]);
                graph.AddEdge(i, neighbor);
            }
        }

        return graph;
    }
}

// Hàm kiểm tra tính khả thi của phương án A
// Hàm này nhận vào một đối tượng đồ thị và chỉ mục của đỉnh sân vườn.
// Ta đếm số lượng đỉnh có bậc lẻ.
// Dựa trên số lượng đỉnh có bậc lẻ, hàm trả về:
// 2: Nếu có thể quay lại sân vườn.
// 1: Nếu chỉ có thể kết thúc tại một phòng.
// 0: Nếu không tồn tại lộ trình.
static int CheckFeasibilityA(Graph graph, int gardenIndex, out List<int> path)
{
    int oddCount = 0;
    path = new List<int>();

    for (int i = 0; i < graph.AdjacencyList.Length; i++)
    {
        int degree = graph.AdjacencyList[i].Count;
        if (degree % 2 != 0)
        {
            oddCount++;
        }
    }
    // Tìm lộ trình tường ứng 

    if (oddCount == 0)
    {
        path.Add(gardenIndex);
        FindPathA(graph, gardenIndex, path);
        return 2;
    }
    else if (oddCount == 2)
    {
        path.Add(gardenIndex);
        FindPathA(graph, gardenIndex, path);
        return 1;
    }

    return 0;
}

// Hàm kiểm tra tính khả thi của phương án B
// Hàm này nhận vào một đối tượng đồ thị và chỉ mục của đỉnh sân vườn.
// Ta đếm số lượng đỉnh có bậc lẻ.
// Dựa trên số lượng đỉnh có bậc lẻ, hàm trả về:
// 2: Nếu có thể quay lại sân vườn.
// 1: Nếu chỉ có thể kết thúc tại một phòng.
// 0: Nếu không tồn tại lộ trình.
static int CheckFeasibilityB(Graph graph, int gardenIndex, out List<int> path)
{
    int oddCount = 0;
    path = new List<int>();

    for (int i = 0; i < graph.AdjacencyList.Length; i++)
    {
        int degree = graph.AdjacencyList[i].Count;

        if (degree % 2 != 0)
        {
            oddCount++;
        }
    }

    if (oddCount == 0)
    {
        path.Add(gardenIndex);
        FindPathB(graph, gardenIndex, path);
        return 2;
    }
    else if (oddCount == 2)
    {
        path.Add(gardenIndex);
        FindPathB(graph, gardenIndex, path);
        return 1;
    }

    return 0;
}

// dùng DFS để tìm lộ trình
static void FindPathA(Graph graph, int node, List<int> path)
{
    List<int> neighbors = new List<int>(graph.AdjacencyList[node]);
    neighbors.Sort();

    foreach (int neighbor in neighbors)
    {
        if (graph.AdjacencyList[node].Contains(neighbor))
        {
            path.Add(neighbor);

            graph.RemoveEdge(node, neighbor);
            graph.RemoveEdge(neighbor, node);

            FindPathA(graph, neighbor, path);
        }
    }
}

static void FindPathB(Graph graph, int node, List<int> path)
{
    List<int> neighbors = new List<int>(graph.AdjacencyList[node]);
    neighbors.Sort();

    foreach (int neighbor in neighbors)
    {
        if (graph.AdjacencyList[node].Contains(neighbor))
        {
            path.Add(neighbor);

            graph.RemoveEdge(node, neighbor);
            graph.RemoveEdge(neighbor, node);

            FindPathB(graph, neighbor, path);
        }
    }
}


class Graph
{
    public List<int>[] AdjacencyList;

    public Graph(int n)
    {
        AdjacencyList = new List<int>[n];
        for (int i = 0; i < n; i++)
        {
            AdjacencyList[i] = new List<int>();
        }
    }

    public void AddEdge(int u, int v)
    {
        AdjacencyList[u].Add(v);
        AdjacencyList[v].Add(u);
    }

    public void RemoveEdge(int u, int v)
    {
        AdjacencyList[u].Remove(v);
        AdjacencyList[v].Remove(u);
    }
}