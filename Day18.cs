using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day18
    {
        private static readonly string INPUT_FILE = "input/day18.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        private List<Node> nodes = new List<Node>();
        private Node TheRootNode = null;

        private Node StringToTree(string s)
        {
            var stack = new Stack<Node>();
            var digits = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            while (s.Length > 0)
            {
                if (s[0] == '[')
                {
                    var node = new Node();
                    nodes.Add(node);

                    if (stack.Count > 0)
                    {
                        var parent = stack.Peek();
                        parent.Children.Add(node);
                        node.Parent = parent;
                    }
                    else
                    {
                        TheRootNode = node;
                    }

                    stack.Push(node);

                    s = s.Substring(1);
                }
                else if (digits.Contains(s[0]))
                {
                    var len = s.IndexOfAny(new[] { '[', ']', ',' });
                    var num = int.Parse(s.Substring(0, len));

                    var parent = stack.Peek();
                    var child = new Node { Value = num };
                    nodes.Add(child);
                    parent.Children.Add(child);
                    child.Parent = parent;

                    s = s.Substring(len);
                }
                else if (s[0] == ',')
                {
                    // TODO: I'm not sure what I should do if I encounter a comma... nothing maybe?
                    s = s.Substring(1);
                }
                else if (s[0] == ']')
                {
                    stack.Pop();

                    if (s.Length == 1)
                    {
                        break;
                    }

                    s = s.Substring(1);
                }
            }

            return TheRootNode;
        }

        public void Part1()
        {
            Node left = null;
            Node right = null;

            foreach (var line in input)
            {
                if (left == null)
                {
                    left = StringToTree(line);
                    continue;
                }

                right = StringToTree(line);

                // do addition
                TheRootNode = Add(left, right);

                // do reduction
                Reduce();

                // set left = the result, so it can be added to the next line
                left = TheRootNode;
            }

            var answer = CalculateMagnitude();

            Console.WriteLine($"Day 18, Part 1: {answer}");
        }

        private Node Add(Node left, Node right)
        {
            var root = new Node();
            nodes.Add(root);

            root.Children.Add(left);
            root.Children.Add(right);

            left.Parent = root;
            right.Parent = root;

            return root;
        }

        private string Stringify(Node node)
        {
            var result = "";

            if (node.Children.Count == 0)
            {
                return node.Value.ToString();
            }

            if (node.Children.Count == 2)
            {
                result += "[";
            }

            result += Stringify(node.Children.First());
            result += ",";
            result += Stringify(node.Children.Last());
            result += "]";

            return result;
        }

        private long CalculateMagnitude()
        {
            return CalculateMagnitude(TheRootNode);
        }

        private long CalculateMagnitude(Node node)
        {
            if (node.Value != null)
            {
                return (long)node.Value;
            }

            var left = node.Children.First();
            var right = node.Children.Last();

            return CalculateMagnitude(left) * 3 + CalculateMagnitude(right) * 2;
        }

        private void Reduce()
        {
            while (true)
            {
                var q1 = nodes.Where(n => n.Depth() > 4);

                if (q1.Count() > 0)
                {
                    var foo = Stringify(TheRootNode);
                    var pairToExplode = q1.Take(2).ToList();

                    var a = pairToExplode[0];
                    var b = pairToExplode[1];

                    var numbers = nodes.Where(n => n.Value != null).ToList();

                    var left = numbers.ElementAtOrDefault(numbers.IndexOf(a) - 1);
                    var right = numbers.ElementAtOrDefault(numbers.IndexOf(b) + 1);

                    if (left != null)
                    {
                        left.Value += a.Value;
                    }

                    if (right != null)
                    {
                        right.Value += b.Value;
                    }

                    a.Parent.Value = 0;
                    nodes.Remove(a);
                    nodes.Remove(b);
                    a.Parent.Children.Remove(a);
                    b.Parent.Children.Remove(b);

                    var bar = Stringify(TheRootNode);

                    continue;
                }

                var q2 = nodes.Where(n => n.Value != null && n.Value > 9);

                if (q2.Count() > 0)
                {
                    var numberToSplit = q2.First();

                    // USE INSERT TO KEEP THE NUMBERS IN APPROXIMATELY THE RIGHT ORDER
                    var value = numberToSplit.Value;
                    var index = nodes.IndexOf(numberToSplit);

                    var a = new Node { Value = value / 2 };
                    var b = new Node { Value = (value + 1) / 2 };

                    nodes.Insert(index + 1, a);
                    nodes.Insert(index + 2, b);

                    numberToSplit.Children.Add(a);
                    numberToSplit.Children.Add(b);

                    a.Parent = numberToSplit;
                    b.Parent = numberToSplit;

                    numberToSplit.Value = null;

                    continue;
                }

                break;
            }

            var baz = Stringify(TheRootNode);
        }

        public void Part2()
        {
            var max = 0L;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    nodes = new List<Node>();
                    TheRootNode = null;

                    var left = StringToTree(input[i]);
                    var right = StringToTree(input[j]);

                    if ((i, j) == (0, 8))
                    {
                        var bar = "baz";
                        var one = Stringify(left);
                        var two = Stringify(right);
                    }

                    // do addition
                    TheRootNode = Add(left, right);

                    // do reduction
                    Reduce();

                    var magnitude = CalculateMagnitude();

                    if (magnitude > 3993)
                    {
                        var foo = "bar";
                    }

                    max = Math.Max(max, magnitude);
                }
            }

            Console.WriteLine($"Day 18, Part 2: {max}");
        }

        private class Node
        {
            public int? Value { get; set; }
            public List<Node> Children { get; set; } = new List<Node>();
            public Node Parent { get; set; } = null;

            public int Depth()
            {
                var depth = 0;
                var temp = this.Parent;

                while (temp != null)
                {
                    temp = temp.Parent;
                    depth++;
                }

                return depth;
            }
        }
    }
}
