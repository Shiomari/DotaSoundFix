using System.Text.RegularExpressions;

public class NewSoundEvent
{
    public string SoundName { get; set; }
    public string Type { get; set; }
    public List<string> Vsnd_files { get; set; } = new List<string>();
    public string Volume { get; set; }
    public string Pitch { get; set; }
    public string Vsnd_duration { get; set; }

    public override string ToString()
    {
        string space = "    ";
        string text =
            $"{space}{SoundName} = \n" +
            $"{space}{{\n" +
            $"{space}{space}type = \"{Type}\"\n" +
            $"{space}{space}volume = {Volume}\n" +
            $"{space}{space}pitch = {Pitch}\n" +
            $"{space}{space}vsnd_files =\n" +
            $"{space}{space}[\n";

        foreach (var file in Vsnd_files)
        {
            text += $"{space}{space}{space}\"{file}\",\n";
        }

        text += $"{space}{space}]\n";
        text += $"{space}}}\n";

        return text;
    }
}

public class NewSoundEventParser
{
    public List<NewSoundEvent> Parse(string kv3Text)
    {
        var soundEntries = new List<NewSoundEvent>();

        var startIndex = kv3Text.IndexOf('{');
        if (startIndex == -1) return soundEntries;

        var content = kv3Text.Substring(startIndex);

        var pattern = @"(\w+)\s*=\s*\{";
        var matches = Regex.Matches(content, pattern);

        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var soundName = match.Groups[1].Value.Trim();

            var blockStart = match.Index + match.Length;
            var blockEnd = FindMatchingBrace(content, blockStart - 1);

            if (blockEnd == -1) continue;

            var blockContent = content.Substring(blockStart, blockEnd - blockStart);
            var soundEntry = ParseSoundEntry(soundName, blockContent);

            if (soundEntry != null)
            {
                soundEntries.Add(soundEntry);
            }
        }

        return soundEntries;
    }

    private int FindMatchingBrace(string text, int startIndex)
    {
        int braceCount = 1;
        for (int i = startIndex + 1; i < text.Length; i++)
        {
            if (text[i] == '{')
                braceCount++;
            else if (text[i] == '}')
                braceCount--;

            if (braceCount == 0)
                return i;
        }
        return -1;
    }

    private NewSoundEvent ParseSoundEntry(string soundName, string blockContent)
    {
        var entry = new NewSoundEvent { SoundName = soundName };

        var lines = blockContent.Split('\n');
        bool inArray = false;
        List<string> currentArray = null;

        foreach (var rawLine in lines)
        {
            var line = rawLine.Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (inArray)
            {
                if (line == "]")
                {
                    inArray = false;
                    continue;
                }

                var value = line.Trim('"', ',', ' ');
                if (!string.IsNullOrWhiteSpace(value) && currentArray != null)
                {
                    currentArray.Add(value);
                }
                continue;
            }

            if (line.StartsWith("vsnd_files", StringComparison.OrdinalIgnoreCase))
            {
                if (line.Contains("["))
                {
                    if (line.Contains("]"))
                    {
                        var arrayContent = ExtractArrayContent(line);
                        entry.Vsnd_files.AddRange(arrayContent);
                    }
                    else
                    {
                        inArray = true;
                        currentArray = entry.Vsnd_files;

                        var arrayStart = line.IndexOf('[');
                        if (arrayStart > 0)
                        {
                            var afterBracket = line.Substring(arrayStart + 1).Trim();
                            if (!string.IsNullOrWhiteSpace(afterBracket))
                            {
                                var firstValue = afterBracket.Trim('"', ',', ' ');
                                if (!string.IsNullOrWhiteSpace(firstValue))
                                {
                                    entry.Vsnd_files.Add(firstValue);
                                }
                            }
                        }
                    }
                }
                else
                {
                    var match = Regex.Match(line, @"vsnd_files\s*=\s*""([^""]+)""");
                    if (match.Success)
                    {
                        entry.Vsnd_files.Add(match.Groups[1].Value);
                    }
                }
                continue;
            }

            var propertyMatch = Regex.Match(line, @"(\w+)\s*=\s*(.+)");
            if (propertyMatch.Success)
            {
                var key = propertyMatch.Groups[1].Value.Trim();
                var value = propertyMatch.Groups[2].Value.Trim();

                value = value.Trim('"');

                switch (key.ToLower())
                {
                    case "type":
                        entry.Type = value;
                        break;
                    case "volume":
                        entry.Volume = value;
                        break;
                    case "pitch":
                        entry.Pitch = value;
                        break;
                    case "vsnd_duration":
                        entry.Vsnd_duration = value;
                        break;
                }
            }
        }

        return entry;
    }

    private List<string> ExtractArrayContent(string line)
    {
        var result = new List<string>();
        var arrayStart = line.IndexOf('[');
        var arrayEnd = line.IndexOf(']');

        if (arrayStart != -1 && arrayEnd != -1)
        {
            var arrayStr = line.Substring(arrayStart + 1, arrayEnd - arrayStart - 1);
            var items = arrayStr.Split(',');

            foreach (var item in items)
            {
                var trimmed = item.Trim('"', ' ', '\t');
                if (!string.IsNullOrWhiteSpace(trimmed))
                {
                    result.Add(trimmed);
                }
            }
        }

        return result;
    }
}