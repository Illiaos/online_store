using Store.Web.Models;

#pragma warning disable
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
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);

                session.Set(key, stream.ToArray());
            }
        }

        public static bool TryGetCart(this ISession session, out Cart value)
        {
            if (session.TryGetValue(key, out byte[] buffer) == false)
            {
                value = new Cart(-1);
                return false;
            }

            using (var stream = new MemoryStream(buffer))
            using (var readed = new BinaryReader(stream, System.Text.Encoding.UTF8, true))
            {
                var orderId = readed.ReadInt32();
                value = new Cart(orderId);
                value.TotalCount = readed.ReadInt32();
                value.TotalPrice = readed.ReadDecimal();
            }
            return true;
        }
    }
}
