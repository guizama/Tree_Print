using System;
using System.Collections.Generic;
using System.Linq;

class TreeNode
{
    public char Data { get; }
    public List<TreeNode> Children { get; } = new List<TreeNode>();

    public TreeNode(char data)
    {
        Data = data;
    }

    public void AddChild(TreeNode child)
    {
        Children.Add(child);
    }
}

class TreeBuilder
{
    private readonly Dictionary<char, TreeNode> nodes = new Dictionary<char, TreeNode>();
    private TreeNode root = null;
    private HashSet<char> children = new HashSet<char>();

    public TreeNode BuildTree(List<(char, char)> pairArray)
    {
        nodes.Clear();
        root = null;
        children.Clear();

        foreach (var (parent, child) in pairArray)
        {
            TreeNode parentNode = nodes.ContainsKey(parent) ? nodes[parent] : new TreeNode(parent);
            TreeNode childNode = nodes.ContainsKey(child) ? nodes[child] : new TreeNode(child);

            if (parentNode.Children.Count >= 2)
            {
                throw new Exception("E1: Mais de 2 filhos");
            }

            if (HasCycle(childNode))
            {
                throw new Exception("E2: Ciclo presente");
            }

            parentNode.AddChild(childNode);
            nodes[parent] = parentNode;
            nodes[child] = childNode;

            if (parent == 'A')
            {
                root = parentNode; // Assume 'A' as the root node
                children.Add(child);
            }
            else
            {
                if (children.Contains(child))
                {
                    throw new Exception("E3: Raízes múltiplas");
                }
                children.Add(child);
            }
        }

        if (root == null)
        {
            throw new Exception("E3: Raízes múltiplas");
        }

        return root;
    }

    private bool HasCycle(TreeNode node, HashSet<char> visited = null)
    {
        if (visited == null)
        {
            visited = new HashSet<char>();
        }

        if (visited.Contains(node.Data))
        {
            return true;
        }

        visited.Add(node.Data);

        foreach (var child in node.Children)
        {
            if (HasCycle(child, visited))
            {
                return true;
            }
        }

        visited.Remove(node.Data);
        return false;
    }
}

class Program
{
    static void Main()
    {
        try
        {
            List<(char, char)> pairArray2 = new List<(char, char)>
            {
                ('A', 'B'), ('A', 'C'), ('B', 'G'), ('C', 'H'), ('E', 'F'), ('B', 'D'), ('C', 'E')
            };

            TreeBuilder treeBuilder = new TreeBuilder();
            TreeNode tree2 = treeBuilder.BuildTree(pairArray2);
            PrintTree(tree2);
            Console.WriteLine("");
            Console.WriteLine("---");
            Console.WriteLine("");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Resultado: Exceção {e.Message}");
        }

        try
        {
            List<(char, char)> pairArray2 = new List<(char, char)>
            {
                ('B', 'D'), ('D', 'E'), ('A', 'B'), ('C', 'F'), ('E', 'G'), ('A', 'C')
            };

            TreeBuilder treeBuilder = new TreeBuilder();
            TreeNode tree2 = treeBuilder.BuildTree(pairArray2);
            PrintTree(tree2);
            Console.WriteLine("");
            Console.WriteLine("---");
            Console.WriteLine("");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Resultado: Exceção {e.Message}");
        }

        try
        {
            List<(char, char)> pairArray2 = new List<(char, char)>
            {
                ('A', 'B'), ('A', 'C'), ('B', 'D'), ('D', 'C')
            };

            TreeBuilder treeBuilder = new TreeBuilder();
            TreeNode tree2 = treeBuilder.BuildTree(pairArray2);
            PrintTree(tree2);
            Console.WriteLine("");
            Console.WriteLine("---");
            Console.WriteLine("");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Resultado: Exceção {e.Message}");
        }
    }

    static void PrintTree(TreeNode root, string indent = "")
    {
        if (root == null)
        {
            return;
        }

        Console.WriteLine(indent + root.Data);

        foreach (var child in root.Children)
        {
            PrintTree(child, indent + "   ");
        }
    }
}
