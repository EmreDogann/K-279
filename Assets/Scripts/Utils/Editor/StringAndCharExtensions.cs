namespace Utils.Editor
{
    internal static class StringAndCharExtensions
    {
        public static char ToLowerAsciiInvariant(this char c)
        {
            if ('A' <= c && c <= 'Z')
            {
                c = (char)(c | 0x20);
            }

            return c;
        }

        public static bool EndsWithCS(this string a)
        {
            int len = a.Length;
            if (a.Length < 3)
            {
                return false;
            }

            if (a[len - 3] != '.')
            {
                return false;
            }

            char c = a[len - 2];
            if (c != 'c' && c != 'C')
            {
                return false;
            }

            c = a[len - 1];
            if (c != 's' && c != 's')
            {
                return false;
            }

            return true;
        }

        public static bool EndsWithJS(this string a)
        {
            int len = a.Length;
            if (a.Length < 3)
            {
                return false;
            }

            if (a[len - 3] != '.')
            {
                return false;
            }

            char c = a[len - 2];
            if (c != 'j' && c != 'J')
            {
                return false;
            }

            c = a[len - 1];
            if (c != 's' && c != 's')
            {
                return false;
            }

            return true;
        }

        public static bool EndsWithBoo(this string a)
        {
            int len = a.Length;
            if (a.Length < 4)
            {
                return false;
            }

            if (a[len - 4] != '.')
            {
                return false;
            }

            char c = a[len - 3];
            if (c != 'b' && c != 'B')
            {
                return false;
            }

            c = a[len - 2];
            if (c != 'o' && c != 'O')
            {
                return false;
            }

            c = a[len - 1];
            if (c != 'o' && c != 'O')
            {
                return false;
            }

            return true;
        }

        public static bool EndsWithExe(this string a)
        {
            int len = a.Length;
            if (a.Length < 4)
            {
                return false;
            }

            if (a[len - 4] != '.')
            {
                return false;
            }

            char c = a[len - 3];
            if (c != 'e' && c != 'E')
            {
                return false;
            }

            c = a[len - 2];
            if (c != 'x' && c != 'X')
            {
                return false;
            }

            c = a[len - 1];
            if (c != 'e' && c != 'E')
            {
                return false;
            }

            return true;
        }

        public static bool EndsWithDll(this string a)
        {
            int len = a.Length;
            if (a.Length < 4)
            {
                return false;
            }

            if (a[len - 4] != '.')
            {
                return false;
            }

            char c = a[len - 3];
            if (c != 'd' && c != 'D')
            {
                return false;
            }

            c = a[len - 2];
            if (c != 'l' && c != 'L')
            {
                return false;
            }

            c = a[len - 1];
            if (c != 'l' && c != 'L')
            {
                return false;
            }

            return true;
        }

        public static bool FastStartsWith(this string a, string b)
        {
            int len = b.Length;
            if (a.Length < len)
            {
                return false;
            }

            int i = 0;
            while (i < len && a[i] == b[i])
            {
                i++;
            }

            return i == len;
        }

        public static bool StartsWithIgnoreCase(this string a, string b)
        {
            int len = b.Length;
            if (a.Length < len)
            {
                return false;
            }

            int i = 0;
            while (i < len && a[i].ToLowerAsciiInvariant() == b[i])
            {
                i++;
            }

            return i == len;
        }

        public static bool FastEndsWith(this string a, string b)
        {
            int i = a.Length - 1;
            int j = b.Length - 1;
            if (i < j)
            {
                return false;
            }

            while (j >= 0 && a[i] == b[j])
            {
                i--;
                j--;
            }

            return j < 0;
        }

        public static bool EndsWithIgnoreCase(this string a, string b)
        {
            int i = a.Length - 1;
            int j = b.Length - 1;
            if (i < j)
            {
                return false;
            }

            while (j >= 0 && a[i].ToLowerAsciiInvariant() == b[j])
            {
                i--;
                j--;
            }

            return j < 0;
        }
    }
}