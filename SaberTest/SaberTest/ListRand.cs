using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SaberTest
{
    public class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            Dictionary<ListNode, int> nodeIndexes = new Dictionary<ListNode, int>(Count);
            ListNode tempNode = Head;

            using (BinaryWriter writer = new BinaryWriter(s))
            {
                writer.Write(Count);

                for (int i = 0; i < Count; i++)
                {
                    writer.Write(tempNode.Data);
                    nodeIndexes.Add(tempNode, i);
                    tempNode = tempNode.Next;
                }

                foreach (KeyValuePair<ListNode, int> node in nodeIndexes)
                {
                    tempNode = node.Key.Rand;   
                    if (tempNode == null) continue;

                    writer.Write(node.Value);
                    writer.Write(nodeIndexes[tempNode]);
                }
            }
        }

        public void Deserialize(FileStream s)
        {
            using (BinaryReader reader = new BinaryReader(s))
            {
                int count = reader.ReadInt32();
                Count = count;

                ListNode[] nodesArray = new ListNode[count];
                ListNode tempNode = null;

                for (int i = 0; i < count; i++)
                {
                    nodesArray[i] = tempNode = new ListNode();
                    tempNode.Data = reader.ReadString();

                    if (i == 0)
                    {
                        Head = tempNode;
                    }
                    else
                    {
                        tempNode.Prev = nodesArray[i - 1];
                        nodesArray[i - 1].Next = tempNode;
                    }
                }

                Tail = tempNode;

                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    int originalNodeId = reader.ReadInt32();
                    int randNodeId = reader.ReadInt32();
                    nodesArray[originalNodeId].Rand = nodesArray[randNodeId];
                }
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            ListNode temp = Head;

            while (temp != null)
            {
                builder.AppendLine(temp.ToString());
                temp = temp.Next;
            }

            return builder.ToString();
        }
    }
}