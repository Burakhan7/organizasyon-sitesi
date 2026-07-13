namespace OrganizasyonSitesi.Services;

public static class SlugYardimcisi
{
    public static string Uret(string metin)
    {
        var harita = new Dictionary<char, char>
        {
            ['ç'] = 'c',
            ['ğ'] = 'g',
            ['ı'] = 'i',
            ['ö'] = 'o',
            ['ş'] = 's',
            ['ü'] = 'u',
            ['Ç'] = 'c',
            ['Ğ'] = 'g',
            ['İ'] = 'i',
            ['Ö'] = 'o',
            ['Ş'] = 's',
            ['Ü'] = 'u'
        };

        var temiz = new System.Text.StringBuilder();
        foreach (var ch in metin.Trim())
        {
            var c = harita.TryGetValue(ch, out var eslenik) ? eslenik : char.ToLowerInvariant(ch);
            if (char.IsLetterOrDigit(c)) temiz.Append(c);
            else if (c == ' ' || c == '-') temiz.Append('-');
        }

        var sonuc = System.Text.RegularExpressions.Regex.Replace(temiz.ToString(), "-{2,}", "-").Trim('-');
        return string.IsNullOrEmpty(sonuc) ? "icerik" : sonuc;
    }
}