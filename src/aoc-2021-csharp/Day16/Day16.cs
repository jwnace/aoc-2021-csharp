using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day16;

public static class Day16
{
    private static readonly string Input = File.ReadAllText("Day16/day16.txt");
    private static readonly Dictionary<char, string> HexToBinary = new()
    {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },
        { 'A', "1010" },
        { 'B', "1011" },
        { 'C', "1100" },
        { 'D', "1101" },
        { 'E', "1110" },
        { 'F', "1111" }
    };

    public static int Part1()
    {
        var versionSum = 0;
        var binary = "";

        for (var i = 0; i < Input.Length; i++)
        {
            binary += HexToBinary[Input[i]];
        }

        while (binary.Contains('1'))
        {
            var version = Convert.ToInt32(binary.Substring(0, 3), 2);
            var typeId = Convert.ToInt32(binary.Substring(3, 3), 2);
            binary = binary.Substring(6);
            versionSum += version;

            if (typeId == 4)
            {
                // literal value packet
                var number = "";

                while (true)
                {
                    var temp = binary.Substring(0, 5);
                    number += temp.Substring(1, 4);
                    binary = binary.Substring(5);

                    if (temp[0] == '0')
                    {
                        break;
                    }
                }

                var value = Convert.ToInt64(number, 2);
            }
            else
            {
                // operator packet
                var lengthTypeId = binary.Substring(0, 1);
                binary = binary.Substring(1);

                var subpacketBitsCount = 0;
                var subpacketsCount = 0;

                if (lengthTypeId == "0")
                {
                    subpacketBitsCount = Convert.ToInt32(binary.Substring(0, 15), 2);
                    binary = binary.Substring(15);
                }
                else
                {
                    subpacketsCount = Convert.ToInt32(binary.Substring(0, 11), 2);
                    binary = binary.Substring(11);
                }
            }
        }

        return versionSum;
    }

    public static long Part2()
    {
        var transmission = new Transmission();

        Input.ToList().ForEach(x => transmission.Binary += HexToBinary[x]);

        var root = ReadPacket(transmission);
        var answer = Solve(root);

        return answer;
    }

    private static long Solve(Node node)
    {
        if (node.NodeType == NodeType.Value)
        {
            return node.Value;
        }

        var values = new List<long>();
        var result = 0L;

        foreach (var child in node.Children)
        {
            values.Add(Solve(child));
        }

        switch(node.NodeType)
        {
            case NodeType.Sum:
                result = values.Sum();
                break;

            case NodeType.Product:
                result = values.Aggregate(1L, (a, b) => a * b);
                break;

            case NodeType.Minimum:
                result = values.Min();
                break;

            case NodeType.Maximum:
                result = values.Max();
                break;

            case NodeType.GreaterThan:
                result = values[0] > values[1] ? 1 : 0;
                break;

            case NodeType.LessThan:
                result = values[0] < values[1] ? 1 : 0;
                break;

            case NodeType.EqualTo:
                result = values[0] == values[1] ? 1 : 0;
                break;
        }

        return result;
    }

    private static Node ReadPacket(Transmission transmission)
    {
        var version = Convert.ToInt32(transmission.Binary.Substring(0, 3), 2);
        var typeId = Convert.ToInt32(transmission.Binary.Substring(3, 3), 2);
        transmission.Binary = transmission.Binary.Substring(6);

        if (typeId == 4)
        {
            return ReadLiteralValuePacket(transmission);
        }
        else
        {
            return ReadOperatorPacket(transmission, (NodeType)typeId);
        }
    }

    private static Node ReadOperatorPacket(Transmission transmission, NodeType type)
    {
        var lengthTypeId = transmission.Binary.Substring(0, 1);
        transmission.Binary = transmission.Binary.Substring(1);

        var subpacketBitsCount = 0;
        var subpacketsCount = 0;

        if (lengthTypeId == "0")
        {
            subpacketBitsCount = Convert.ToInt32(transmission.Binary.Substring(0, 15), 2);
            transmission.Binary = transmission.Binary.Substring(15);
        }
        else
        {
            subpacketsCount = Convert.ToInt32(transmission.Binary.Substring(0, 11), 2);
            transmission.Binary = transmission.Binary.Substring(11);
        }

        var node = new Node { NodeType = type };

        if (subpacketsCount > 0)
        {
            for (var i = 0; i < subpacketsCount; i++)
            {
                node.Children.Add(ReadPacket(transmission));
            }
        }
        else if (subpacketBitsCount > 0)
        {
            var temp = transmission.Binary.Substring(subpacketBitsCount);

            while (transmission.Binary != temp)
            {
                node.Children.Add(ReadPacket(transmission));
            }
        }

        return node;
    }

    private static Node ReadLiteralValuePacket(Transmission transmission)
    {
        var number = "";

        while (true)
        {
            var temp = transmission.Binary.Substring(0, 5);
            number += temp.Substring(1, 4);
            transmission.Binary = transmission.Binary.Substring(5);

            if (temp[0] == '0')
            {
                break;
            }
        }

        return new Node { Value = Convert.ToInt64(number, 2), NodeType = NodeType.Value };
    }

    class Transmission
    {
        public string Binary { get; set; }
    }

    class Node
    {
        public NodeType NodeType { get; set; }
        public long Value { get; set; }
        public List<Node> Children { get; set; } = new List<Node>();
    }

    enum NodeType
    {
        Sum = 0,
        Product = 1,
        Minimum = 2,
        Maximum = 3,
        Value = 4,
        GreaterThan = 5,
        LessThan = 6,
        EqualTo = 7
    }
}
