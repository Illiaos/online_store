using Store.Web.Models;

namespace Store.Web
{
    public static class SessionExtensions
    {
        private const string key = "Cart";

        public static void Set(this ISession session, Cart value)
        {
            if (value == null) return;

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
            {
                writer.Write(value.Items.Count);
                foreach (var item in value.Items)
                {
                    writer.Write(item.Key);
                    writer.Write(item.Value);
                }

                writer.Write(value.Amount);
                session.Set(key, stream.ToArray());
            }
        }

        public static bool TryGetCart(this ISession session, out Cart value)
        {
            value = new Cart();
            if (session.TryGetValue(key, out byte[] buffer) == false) return false;
            using (var stream = new MemoryStream(buffer))
            using (var readed = new BinaryReader(stream, System.Text.Encoding.UTF8, true))
            {
                var length = readed.ReadInt32();
                for (int i = 0; i < length; i++)
                {
                    var bookId = readed.ReadInt32();
                    var amount = readed.ReadInt32();

                    value.Items.Add(bookId, amount);
                }
                value.Amount = readed.ReadDecimal();
            }
            return true;
        }
    }
}
