using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day18
    {
        private static readonly string INPUT_FILE = "input/day18.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            Tree left = null;
            Tree right = null;

            foreach (var line in input)
            {
                if (left == null)
                {
                    left = Tree.FromString(line);
                    continue;
                }

                right = Tree.FromString(line);

                var sum = left.Add(right);
                
                left = sum;
            }

            var answer = left.Magnitude();

            Console.WriteLine($"Day 18, Part 1: {answer}");
        }

        public void Part2()
        {
            var max = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    var left = Tree.FromString(input[i]);
                    var right = Tree.FromString(input[j]);
                    var sum = left.Add(right);

                    max = Math.Max(max, sum.Magnitude());
                }
            }

            Console.WriteLine($"Day 18, Part 2: {max}");
        }

        private class Tree
        {
            public Node Root { get; set; }

            public List<Node> Numbers { get; set; } = new List<Node>();

            public static Tree FromString(string s)
            {
                var tree = new Tree();
                var stack = new Stack<Node>();
                var digits = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                while (s.Length > 0)
                {
                    if (s[0] == '[')
                    {
                        var node = new Node();

                        if (stack.Count > 0)
                        {
                            var parent = stack.Peek();
                            parent.Children.Add(node);
                            node.Parent = parent;
                        }
                        else
                        {
                            tree.Root = node;
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

                        parent.Children.Add(child);
                        child.Parent = parent;

                        tree.Numbers.Add(child);

                        s = s.Substring(len);
                    }
                    else if (s[0] == ',')
                    {
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

                return tree;
            }

            public Tree Add(Tree right)
            {
                var left = this;
                var sum = new Tree { Root = new Node() };

                sum.Root.Children.Add(left.Root);
                sum.Root.Children.Add(right.Root);

                left.Root.Parent = sum.Root;
                right.Root.Parent = sum.Root;

                sum.Numbers = left.Numbers.Concat(right.Numbers).ToList();
                sum.Reduce();
                
                return sum;
            }

            private void Reduce()
            {
                while (true)
                {
                    var q1 = Numbers.Where(n => n.Depth() > 4).ToList();

                    if (q1.Count() > 0)
                    {
                        var pairToExplode = q1.Take(2).ToList();

                        var a = pairToExplode[0];
                        var b = pairToExplode[1];

                        var left = Numbers.ElementAtOrDefault(Numbers.IndexOf(a) - 1);
                        var right = Numbers.ElementAtOrDefault(Numbers.IndexOf(b) + 1);

                        if (left != null)
                        {
                            left.Value += a.Value;
                        }

                        if (right != null)
                        {
                            right.Value += b.Value;
                        }

                        a.Parent.Value = 0;
                        Numbers.Insert(Numbers.IndexOf(a), a.Parent);
                        Numbers.Remove(a);
                        Numbers.Remove(b);
                        a.Parent.Children.Remove(a);
                        b.Parent.Children.Remove(b);

                        continue;
                    }

                    var q2 = Numbers.Where(n => n.Value != null && n.Value > 9);

                    if (q2.Count() > 0)
                    {
                        var numberToSplit = q2.First();

                        var value = numberToSplit.Value.Value;
                        var index = Numbers.IndexOf(numberToSplit);

                        var a = new Node { Value = value / 2 };
                        var b = new Node { Value = (value + 1) / 2 };

                        Numbers.Insert(index + 1, a);
                        Numbers.Insert(index + 2, b);

                        numberToSplit.Children.Add(a);
                        numberToSplit.Children.Add(b);

                        a.Parent = numberToSplit;
                        b.Parent = numberToSplit;

                        numberToSplit.Value = null;
                        Numbers.RemoveAt(index);

                        continue;
                    }

                    break;
                }
            }

            public int Magnitude()
            {
                return CalculateMagnitude(Root);
            }

            private int CalculateMagnitude(Node node)
            {
                if (node.Value != null)
                {
                    return node.Value.Value;
                }

                var left = node.Children.First();
                var right = node.Children.Last();

                return CalculateMagnitude(left) * 3 + CalculateMagnitude(right) * 2;
            }

            public override string ToString()
            {
                return Root.ToString();
            }
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

            public override string ToString()
            {
                var result = "";

                if (Children.Count == 0)
                {
                    return Value.ToString();
                }

                if (Children.Count == 2)
                {
                    result += "[";
                }

                result += Children.First().ToString();
                result += ",";
                result += Children.Last().ToString();
                result += "]";

                return result;
            }
        }
    }
}
