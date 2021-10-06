using Microsoft.VisualStudio.TestTools.UnitTesting;
using SaberTest;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void PackUnpack_ListRand_WithNoRand_GetTheSameData()
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

            ListRand unpackedList = new ListRand();
            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Create, FileAccess.Write))
            {
                list.Serialize(stream);
            }

            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Open, FileAccess.Read))
            {
                unpackedList.Deserialize(stream);
            }

            Assert.AreEqual(list.Count, unpackedList.Count);
            ListNode nodeBefore = list.Head;
            ListNode nodeAfter = unpackedList.Head;
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(nodeBefore.Data, nodeAfter.Data);
                if (nodeBefore.Prev != null || nodeAfter.Prev != null)
                    Assert.AreEqual(nodeBefore.Prev.Data, nodeAfter.Prev.Data);
                if (nodeBefore.Next != null || nodeAfter.Next != null)
                    Assert.AreEqual(nodeBefore.Next.Data, nodeAfter.Next.Data);
                if (nodeBefore.Rand != null || nodeAfter.Rand != null)
                    Assert.AreEqual(nodeBefore.Rand.Data, nodeAfter.Rand.Data);
            }
        }
        
        [TestMethod]
        public void PackUnpack_ListRand_Empty_GetTheSameData()
        {
            ListRand list = new ListRand();
            list.Count = 0;

            ListRand unpackedList = new ListRand();
            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Create, FileAccess.Write))
            {
                list.Serialize(stream);
            }

            using (FileStream stream = new FileStream(@"..\..\..\data.dat", FileMode.Open, FileAccess.Read))
            {
                unpackedList.Deserialize(stream);
            }

            Assert.AreEqual(list.Count, unpackedList.Count);
        }

        [TestMethod]
        public void PackUnpack_ListRand_WithRandElements_GetTheSameData()
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

            Assert.AreEqual(list.Count, unpackedList.Count);
            ListNode nodeBefore = list.Head;
            ListNode nodeAfter = unpackedList.Head;
            for (int i = 0; i < list.Count; i++)
            {
                Assert.AreEqual(nodeBefore.Data, nodeAfter.Data);
                if (nodeBefore.Prev != null || nodeAfter.Prev != null)
                    Assert.AreEqual(nodeBefore.Prev.Data, nodeAfter.Prev.Data);
                if (nodeBefore.Next != null || nodeAfter.Next != null)
                    Assert.AreEqual(nodeBefore.Next.Data, nodeAfter.Next.Data);
                if (nodeBefore.Rand != null || nodeAfter.Rand != null)
                    Assert.AreEqual(nodeBefore.Rand.Data, nodeAfter.Rand.Data);
            }
        }
    }
}