using System.Text;
using System.Text.RegularExpressions;

public class OldSoundEvent
{
    public string SoundName { get; set; }
    public string Type { get; set; }
    public List<string> Vsnd_files { get; set; }
    public string Volume { get; set; }
    public string Pitch { get; set; }
}

public class OldSoundEventParser
{
    public List<OldSoundEvent> Parse(string input)
    {
        var soundEvents = new List<OldSoundEvent>();

        var soundBlocks = ExtractSoundBlocks(input);

        foreach (var block in soundBlocks)
        {
            var soundEvent = ParseSoundBlock(block);
            if (soundEvent != null)
            {
                soundEvents.Add(soundEvent);
            }
        }

        return soundEvents;
    }

    private List<string> ExtractSoundBlocks(string input)
    {
        var blocks = new List<string>();

        int startIdx = input.IndexOf("[");
        if (startIdx == -1) return blocks;

        int braceCount = 0;
        bool inBlock = false;
        StringBuilder currentBlock = new StringBuilder();

        for (int i = startIdx; i < input.Length; i++)
        {
            char c = input[i];

            if (c == '{')
            {
                braceCount++;
                inBlock = true;
            }

            if (inBlock)
            {
                currentBlock.Append(c);
            }

            if (c == '}')
            {
                braceCount--;
                if (braceCount == 0 && inBlock)
                {
                    blocks.Add(currentBlock.ToString());
                    currentBlock.Clear();
                    inBlock = false;
                }
            }
        }

        return blocks;
    }

    private OldSoundEvent ParseSoundBlock(string block)
    {
        var soundEvent = new OldSoundEvent();

        var soundNameMatch = Regex.Match(block, @"m_SoundName\s*=\s*resource:""([^""]+)""");
        if (soundNameMatch.Success)
        {
            soundEvent.SoundName = soundNameMatch.Groups[1].Value;
        }

        var kvBlockMatch = Regex.Match(block, @"m_OperatorsKV\s*=\s*resource:""""""([\s\S]*?)""""""", RegexOptions.Multiline);
        if (kvBlockMatch.Success)
        {
            var kvContent = kvBlockMatch.Groups[1].Value;
            ParseKvContent(kvContent, soundEvent);
        }

        return soundEvent;
    }

    private void ParseKvContent(string kvContent, OldSoundEvent soundEvent)
    {
        var typeMatch = Regex.Match(kvContent, @"type\s*=\s*""([^""]+)""");
        if (typeMatch.Success)
        {
            soundEvent.Type = typeMatch.Groups[1].Value;
        }

        var volumeMatch = Regex.Match(kvContent, @"volume\s*=\s*""([^""]+)""");
        if (volumeMatch.Success)
        {
            soundEvent.Volume = volumeMatch.Groups[1].Value;
        }

        var pitchMatch = Regex.Match(kvContent, @"pitch\s*=\s*""([^""]+)""");
        if (pitchMatch.Success)
        {
            soundEvent.Pitch = pitchMatch.Groups[1].Value;
        }

        soundEvent.Vsnd_files = ExtractVsndFiles(kvContent);
    }

    private List<string> ExtractVsndFiles(string kvContent)
    {
        var files = new List<string>();

        int vsndStart = kvContent.IndexOf("vsnd_files");
        if (vsndStart == -1) return files;

        int arrayStart = kvContent.IndexOf('[', vsndStart);
        if (arrayStart == -1) return files;

        int bracketCount = 0;
        bool inArray = false;
        StringBuilder currentValue = new StringBuilder();

        for (int i = arrayStart; i < kvContent.Length; i++)
        {
            char c = kvContent[i];

            if (c == '[' && !inArray)
            {
                bracketCount++;
                inArray = true;
            }
            else if (c == ']')
            {
                bracketCount--;
                if (bracketCount == 0)
                {
                    if (currentValue.Length > 0)
                    {
                        var file = currentValue.ToString().Trim().Trim('"', ',', ' ');
                        if (!string.IsNullOrEmpty(file))
                            files.Add(file);
                    }
                    break;
                }
            }
            else if (c == '"' && inArray)
            {
                int stringStart = i + 1;
                int stringEnd = kvContent.IndexOf('"', stringStart);
                if (stringEnd != -1)
                {
                    string file = kvContent.Substring(stringStart, stringEnd - stringStart);
                    files.Add(file);
                    i = stringEnd;
                }
            }
        }

        return files;
    }
}