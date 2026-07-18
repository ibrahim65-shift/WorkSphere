using System;
using System.Security.Cryptography;
using System.Text;

namespace WorkSphere.Global_Classes
{
    public static class clsSecurity
    {
        // توليد salt آمن (16 بايت افتراضي)
        public static string GenerateSalt(int size = 16)
        {
            var bytes = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }

        // SHA256(salt + password)
        public static string HashPassword(string password, string salt)
        {
            using (var sha = SHA256.Create())
            {
                var data = Encoding.UTF8.GetBytes(salt + password);
                var hash = sha.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }

        // صيغة تخزين قياسية: salt:hash
        public static string Pack(string salt, string hash) => $"{salt}:{hash}";

        // فك التعبئة مع قدرات اكتشاف الترتيب (salt:hash أو hash:salt)
        public static bool TryUnpack(string packed, out string salt, out string hash)
        {
            salt = null;
            hash = null;

            if (string.IsNullOrWhiteSpace(packed))
                return false;

            // قطع النص عند أول ':' فقط — لتجنّب مشاكل وجود ':' داخل المحتوى (نادر للـ Base64 لكنه آمن)
            int idx = packed.IndexOf(':');
            if (idx <= 0 || idx == packed.Length - 1)
                return false;

            string part1 = packed.Substring(0, idx).Trim();
            string part2 = packed.Substring(idx + 1).Trim();

            // طول Base64 المتوقع: salt (16 bytes) -> 24 chars, SHA256 hash (32 bytes) -> 44 chars
            // نستخدم هذا كقاعدة لاكتشاف الترتيب؛ إذا لم تتطابق الأطوال، نجرّب فك الـ Base64 للتأكد.
            bool part1LooksLikeHash = LooksLikeSha256Base64(part1);
            bool part2LooksLikeHash = LooksLikeSha256Base64(part2);

            if (part1LooksLikeHash && !part2LooksLikeHash)
            {
                // مخزون بصيغة hash:salt
                hash = part1;
                salt = part2;
                return true;
            }
            else if (!part1LooksLikeHash && part2LooksLikeHash)
            {
                // مخزون بصيغة salt:hash (المرغوب)
                salt = part1;
                hash = part2;
                return true;
            }
            else
            {
                // حالة غير واضحة: نحاول فك Base64 للتأكد
                // إذا كلاهما يفككان لكن أحدهما يطابق طول SHA256 بعد الفك، نقرّر بناءً عليه
                try
                {
                    byte[] b1 = Convert.FromBase64String(part1);
                    byte[] b2 = Convert.FromBase64String(part2);

                    if (b1.Length == 32 && b2.Length != 32)
                    {
                        hash = part1; salt = part2; return true;
                    }
                    if (b2.Length == 32 && b1.Length != 32)
                    {
                        salt = part1; hash = part2; return true;
                    }
                }
                catch
                {
                    // إذا فكّ الـ Base64 فشل، نفشل بهدوء
                }

                return false;
            }
        }

        // تحقق: يحاول فك packed ثم يحتسب الهاش ويقارن بشكل صارم
        public static bool Verify(string password, string packed)
        {
            if (!TryUnpack(packed, out var salt, out var storedHash))
                return false;

            var computed = HashPassword(password, salt);
            return string.Equals(computed, storedHash, StringComparison.Ordinal);
        }

        // مساعد بسيط لتوقع شكل hash (طول Base64 = 44 عادةً للـ SHA256)
        private static bool LooksLikeSha256Base64(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            if (s.Length == 44) return true; // القاعدة العامة
            // إذا طوله غير 44، نحاول فك القاعدة ونرى طول البايت
            try
            {
                var b = Convert.FromBase64String(s);
                return b.Length == 32;
            }
            catch
            {
                return false;
            }
        }
    }
}