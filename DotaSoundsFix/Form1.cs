using System.Diagnostics;
using ValveResourceFormat;
using ValveResourceFormat.ResourceTypes;
using NAudio.Wave;
using System.Text;

namespace DotaSoundsFix
{
    public partial class Form1 : Form
    {

        string _baseFolder = null;


        public Form1()
        {
            InitializeComponent();

            if (File.Exists("lastPath.txt"))
            {
                _baseFolder = File.ReadAllText("lastPath.txt");
                selectFolderLabel.Text = _baseFolder;
            }
        }



        public void SelectFolder()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select folder, thats contains 'sounds' and 'soundsevent' folders.";
                dialog.RootFolder = Environment.SpecialFolder.Desktop;
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var subdirs = Directory.GetDirectories(dialog.SelectedPath);
                    bool isHaveSounds = false;
                    bool isHaveSoundEvents = false;
                    foreach (var dir in subdirs)
                    {
                        switch (Path.GetRelativePath(dialog.SelectedPath, dir))
                        {
                            case "soundevents":
                                isHaveSoundEvents = true;
                                break;

                            case "sounds":
                                isHaveSounds = true;
                                break;
                        }
                    }

                    if (!isHaveSoundEvents || !isHaveSounds)
                    {
                        MessageBox.Show("Select folder, thats contains 'sounds' and 'soundsevent' folders.");
                        return;
                    }

                    _baseFolder = dialog.SelectedPath;
                    File.WriteAllText("lastPath.txt", _baseFolder);
                    return;
                }
            }
            _baseFolder = null;
        }

        public void FixEmptySounds()
        {
            if (_baseFolder == null)
            {
                MessageBox.Show("Select folder, thats contains 'sounds' and 'soundsevent' folders.");
                return;
            }

            string[] files = Directory.GetFiles(_baseFolder, "*.vsnd_c", SearchOption.AllDirectories);

            int count = 0;
            foreach (var file in files)
            {
                if (File.ReadAllBytes(file).Length > 0)
                {
                    continue;
                }
                File.Copy(@"null.vsnd_c", file, true);
                count++;
            }

            MessageBox.Show($"Replaced {count} sounds.");
        }

        public void CheckNotIncludingSounds()
        {
            if (_baseFolder == null)
            {
                MessageBox.Show("Folder not selected.");
                return;
            }

            string[] sounds = Directory.GetFiles(_baseFolder, "*.vsnd_c", SearchOption.AllDirectories);
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i] = Path.GetRelativePath(_baseFolder, sounds[i]);
            }


            string[] soundevents = Directory.GetFiles(_baseFolder, "*.vsndevts_c", SearchOption.AllDirectories);

            List<string> usingSounds = new List<string>();
            foreach (var soundevent in soundevents)
            {
                using var se = new Resource();
                se.Read(soundevent);

                var writer = new IndentedTextWriter();
                se.DataBlock.WriteText(writer);

                var text = writer.ToString();
                string[] lines = text.Split('\n').Where(str => str.Contains(".vsnd")).ToArray();
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    line = line.Remove(line.IndexOf(".vsnd") + 5);
                    line = line.Substring(line.LastIndexOf('\"') + 1);
                    line = line.Replace("/", "\\");
                    line = line + "_c";

                    lines[i] = line;
                }

                usingSounds.AddRange(lines);
            }

            HashSet<string> uniqueSet = new HashSet<string>(usingSounds);
            usingSounds = uniqueSet.ToList();

            for (int i = 0; i < sounds.Length; i++)
            {
                for (int j = 0; j < usingSounds.Count; j++)
                {
                    if (usingSounds[j].ToLowerInvariant().Contains(sounds[i].ToLowerInvariant()))
                    {
                        usingSounds.RemoveAt(j);
                        continue;
                    }
                }
            }

            usingSounds.Insert(0, "MISSING SOUND FILES:");
            File.WriteAllLines("MissingSounds.txt", usingSounds);
            Process.Start(new ProcessStartInfo
            {
                FileName = "MissingSounds.txt",
                UseShellExecute = true
            });
        }

        public void FixNotIncludingSounds()
        {
            if (_baseFolder == null)
            {
                MessageBox.Show("Folder not selected.");
                return;
            }

            string[] sounds = Directory.GetFiles(_baseFolder, "*.vsnd_c", SearchOption.AllDirectories);
            for (int i = 0; i < sounds.Length; i++)
            {
                sounds[i] = Path.GetRelativePath(_baseFolder, sounds[i]);
            }


            string[] soundevents = Directory.GetFiles(_baseFolder, "*.vsndevts_c", SearchOption.AllDirectories);

            List<string> usingSounds = new List<string>();
            foreach (var soundevent in soundevents)
            {
                using var se = new Resource();
                se.Read(soundevent);

                var writer = new IndentedTextWriter();
                se.DataBlock.WriteText(writer);

                var text = writer.ToString();
                string[] lines = text.Split('\n').Where(str => str.Contains(".vsnd")).ToArray();
                for (int i = 0; i < lines.Length; i++)
                {
                    var line = lines[i].Trim();
                    line = line.Remove(line.IndexOf(".vsnd") + 5);
                    line = line.Substring(line.LastIndexOf('\"') + 1);
                    line = line.Replace("/", "\\");
                    line = line + "_c";

                    lines[i] = line;
                }

                usingSounds.AddRange(lines);
            }

            HashSet<string> uniqueSet = new HashSet<string>(usingSounds);
            usingSounds = uniqueSet.ToList();

            for (int i = 0; i < sounds.Length; i++)
            {
                for (int j = 0; j < usingSounds.Count; j++)
                {
                    if (usingSounds[j].ToLowerInvariant().Contains(sounds[i].ToLowerInvariant()))
                    {
                        usingSounds.RemoveAt(j);
                        continue;
                    }
                }
            }

            int count = 0;
            foreach (var us in usingSounds)
            {
                var file = Path.Combine(_baseFolder, us);
                var dir = Path.GetDirectoryName(file);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                if (Path.GetFileName(file).Contains("?"))
                    continue;

                File.Copy(@"null.vsnd_c", file, true);
                count++;
            }

            MessageBox.Show($"Added {usingSounds.Count} empty sounds.");
        }

        public void FixSoundsCodec()
        {
            string[] sounds = Directory.GetFiles(_baseFolder, "*.vsnd_c", SearchOption.AllDirectories);

            Directory.CreateDirectory("Sounds");

            foreach (var sound in sounds)
            {
                string name = Path.GetFileNameWithoutExtension(sound);

                using var sd = new Resource();
                sd.Read(sound);

                Sound soundBlock = ((Sound)sd.DataBlock);
                var bytes = soundBlock.GetSound();

                if (soundBlock.SoundType == Sound.AudioFileType.MP3)
                {
                    var file = Path.Combine("Sounds", name + ".mp3");
                    File.WriteAllBytes(file, bytes);
                    Mp3ToWavConverter.ConvertMp3ToWav(file, Path.Combine("Sounds", name + ".wav"));
                }
                else
                {
                    var file = Path.Combine("Sounds", name + ".wav");
                    File.WriteAllBytes(file, bytes);
                }
            }

            string[] wavFiles = Directory.GetFiles("Sounds", "*.wav");
            foreach (string wavFile in wavFiles)
            {
                ConvertAdpcmToPcm(wavFile, Path.Combine("FixedSounds", Path.GetFileName(wavFile)));
            }
            Directory.Delete("Sounds", true);

            if (Directory.Exists("FixedSounds"))
            {
                wavFiles = Directory.GetFiles("FixedSounds", "*.wav");
                DragDropSimulator.SimulateDragDropToApp("cs2_sound_converter.exe", wavFiles);

                var fixedAudios = new List<string>(Directory.GetFiles("FixedSounds", "*.vsnd_c"));

                foreach (var sound in sounds)
                {
                    foreach (var fixedAudio in fixedAudios)
                    {
                        if (Path.GetFileName(sound) == Path.GetFileName(fixedAudio))
                        {
                            File.Copy(fixedAudio, sound, true);
                        }
                    }
                }

                MessageBox.Show($"Fixed {fixedAudios.Count} sounds.");
                Directory.Delete("FixedSounds", true);
            }
            else
            {
                MessageBox.Show("All sounds codec correct.");
            }
        }

        public void GetFixedWavFiles()
        {
            string[] sounds = Directory.GetFiles(_baseFolder, "*.vsnd_c", SearchOption.AllDirectories);

            Directory.CreateDirectory("Sounds");

            foreach (var sound in sounds)
            {
                string name = Path.GetFileNameWithoutExtension(sound);

                using var sd = new Resource();
                sd.Read(sound);

                Sound soundBlock = ((Sound)sd.DataBlock);
                var bytes = soundBlock.GetSound();

                if (soundBlock.SoundType == Sound.AudioFileType.MP3)
                {
                    var file = Path.Combine("Sounds", name + ".mp3");
                    File.WriteAllBytes(file, bytes);
                    Mp3ToWavConverter.ConvertMp3ToWav(file, Path.Combine("Sounds", name + ".wav"));
                }
                else
                {
                    var file = Path.Combine("Sounds", name + ".wav");
                    File.WriteAllBytes(file, bytes);
                }
            }

            string[] wavFiles = Directory.GetFiles("Sounds", "*.wav");
            foreach (string wavFile in wavFiles)
            {
                ConvertAdpcmToPcm(wavFile, Path.Combine("FixedSounds", Path.GetFileName(wavFile)));
            }
            Directory.Delete("Sounds", true);

            if (Directory.Exists("FixedSounds"))
            {
                wavFiles = Directory.GetFiles("FixedSounds", "*.wav");
                DragDropSimulator.OpenExplorerWithFile(wavFiles[0]);

                MessageBox.Show($"Created {wavFiles.Length} .WAV files.");
            }
            else
            {
                MessageBox.Show("All sounds codec correct.");
            }
        }

        public void CheckOldVoSoundScripts()
        {
            string[] soundevents = Directory.GetFiles(_baseFolder, "game_sounds_vo*.vsndevts_c", SearchOption.AllDirectories);

            Directory.CreateDirectory("soundevents");

            List<string> old = new List<string>();
            foreach (var soundevent in soundevents)
            {
                string path = Path.GetFileName(soundevent);

                using var se = new Resource();
                se.Read(soundevent);

                if (se.DataBlock is NTRO ntro)
                {
                    old.Add(Path.GetFileName(soundevent));
                }
            }

            if (old.Count > 0)
            {
                MessageBox.Show($"Old soundevents:\n" + string.Join("\n", old));
            }
            else
            {
                MessageBox.Show($"Old soundevents not found.");
            }
        }

        public void CreateFixedVsndevts()
        {
            string[] soundevents = Directory.GetFiles(_baseFolder, "game_sounds_vo*.vsndevts_c", SearchOption.AllDirectories);

            Directory.CreateDirectory("soundevents");

            List<string> old = new List<string>();
            int count = 0;
            foreach (var soundevent in soundevents)
            {
                using var se = new Resource();
                se.Read(soundevent);

                if (se.DataBlock is NTRO ntro)
                {
                    string name = Path.GetFileNameWithoutExtension(soundevent);
                    string path = soundevent.Replace(name + ".vsndevts_c", name + ".vsndevts");
                    old.Add(Path.GetFileName(path));

                    var writer = new IndentedTextWriter();
                    ntro.WriteText(writer);
                    var brokenText = writer.ToString();

                    var parser1 = new OldSoundEventParser();
                    var brokenEvents = parser1.Parse(brokenText);

                    var dotaEventPath = Directory.GetFiles("DotaSoundScripts", name + ".vsndevts_c", SearchOption.AllDirectories).FirstOrDefault();
                    if (string.IsNullOrEmpty(dotaEventPath))
                    {
                        MessageBox.Show("Unknown dota soundevent script: " + name + ".vsndevts_c");
                        return;
                    }
                    using var dotaSe = new Resource();
                    dotaSe.Read(dotaEventPath);

                    var dotaText = dotaSe.DataBlock.ToString();

                    var parser2 = new NewSoundEventParser();
                    var soundEntries = parser2.Parse(dotaText);

                    foreach (var soundEntry in soundEntries)
                    {
                        foreach (var brokenEvent in brokenEvents)
                        {
                            if (soundEntry.SoundName == brokenEvent.SoundName)
                            {
                                soundEntry.Vsnd_files = brokenEvent.Vsnd_files;
                                break;
                            }
                        }
                    }

                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(dotaText.Remove(dotaText.IndexOf("\n")));
                    stringBuilder.AppendLine("{");
                    foreach (var soundEntry in soundEntries)
                    {
                        stringBuilder.Append(soundEntry.ToString());
                    }
                    stringBuilder.AppendLine("}");

                    File.WriteAllText(path, stringBuilder.ToString());
                }
            }

            if (old.Count > 0)
            {
                MessageBox.Show($"Created fixed soundevents:\n" + string.Join("\n", old));
            }
            else
            {
                MessageBox.Show($"Old soundevents not found.");
            }
        }



        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            SelectFolder();
            selectFolderLabel.Text = _baseFolder;
        }

        private void fixEmptySoundsButton_Click(object sender, EventArgs e)
        {
            FixEmptySounds();
        }

        private void checkNotIncludingSoundsButton_Click(object sender, EventArgs e)
        {
            CheckNotIncludingSounds();
        }

        private void fixNotIncludingSoundsButton_Click(object sender, EventArgs e)
        {
            FixNotIncludingSounds();
        }

        private void setCs2PathButton_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cs2_sound_converter.exe",
                UseShellExecute = true
            });
        }

        private void fixSoundsCodecButton_Click(object sender, EventArgs e)
        {
            FixSoundsCodec();
        }

        private void getFixedWavFilesButton_Click(object sender, EventArgs e)
        {
            GetFixedWavFiles();
        }

        private void checkOldVoSoundsScriptButton_Click(object sender, EventArgs e)
        {
            CheckOldVoSoundScripts();
        }

        private void createFixedVsndevts_Click(object sender, EventArgs e)
        {
            CreateFixedVsndevts();
        }



        void ConvertAdpcmToPcm(string inputPath, string outputPath)
        {
            try
            {
                // Чтение ADPCM файла
                using (var reader = new WaveFileReader(inputPath))
                {
                    // Проверяем формат
                    if (reader.WaveFormat.Encoding != WaveFormatEncoding.Adpcm)
                    {
                        Console.WriteLine("Файл не в формате ADPCM");
                        return;
                    }

                    Console.WriteLine($"Исходный формат: {reader.WaveFormat}");

                    // Конвертируем в PCM
                    using (var conversionStream = new WaveFormatConversionStream(
                        new WaveFormat(reader.WaveFormat.SampleRate, 16, reader.WaveFormat.Channels),
                        reader))
                    {
                        var dir = Path.GetDirectoryName(outputPath);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }

                        // Сохраняем как PCM WAV
                        WaveFileWriter.CreateWaveFile(outputPath, conversionStream);
                    }

                    Console.WriteLine($"Конвертация завершена: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
