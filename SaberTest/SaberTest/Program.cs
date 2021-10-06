using System;
using System.IO;

namespace SaberTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            ListRand list = new ListRand();
            list.Count = 3;
            ListNode[] nodes = new ListNode[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                ListNode node = nodes[i] = new ListNode();
                node.Data = i.ToString();
                if (i == 0) continue;

                node.Prev = nodes[i - 1];
                nodes[i - 1].Next = node;
            }
            list.Head = nodes[0];
            list.Tail = nodes[list.Count - 1];

            list.Head.Rand = nodes[1];
            nodes[1].Rand = nodes[1];
            list.Tail.Rand = nodes[1];

            ListRand unpackedList = new ListRand();
            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Create, FileAccess.Write))
            {
                list.Serialize(stream);
            }

            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Open, FileAccess.Read))
            {
                unpackedList.Deserialize(stream);
            }

            Console.WriteLine($"List before:\n{list}");
            Console.WriteLine($"List after:\n{unpackedList}");
            Console.ReadKey();
        }
    }
}