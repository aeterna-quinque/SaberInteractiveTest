using System.Text;

namespace SaberTest
{
    public class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand; // произвольный элемент внутри списка
        public string Data;

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Data);
            if (Rand != null)
                builder.Append($"\tRand -> {Rand.Data}");
            return builder.ToString();
        }
    }
}