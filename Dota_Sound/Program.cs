




//string[] files = Directory.GetFiles(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\VPK CREATOR\pak01_dir\sounds\vo\bhchan");
//List<string> se = new List<string>(File.ReadAllLines(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\SE.txt")).Where(str => str.Trim().StartsWith("\"")).ToList();

//for (int i = 0; i < files.Length; i++)
//{
//    string file = Path.GetFileName(files[i]);
//    file = file.Remove(file.Length - 2);

//    for (int j = 0; j < se.Count; j++)
//    {
//        if (se[j].Contains(file))
//        {
//            se.RemoveAt(j);
//            continue;
//        }
//    }
//}

//Console.WriteLine(se);




//List<string> se = new List<string>(File.ReadAllLines(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\SE.txt")).Where(str => !str.Trim().StartsWith("vsnd_duration")).ToList();
//File.WriteAllLines(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\SE.txt", se);



//string[] files = Directory.GetFiles(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\ConvertedSounds", "*.wav", SearchOption.AllDirectories);
//foreach (string file in files)
//{
//    File.Delete(file);
//}


//using NAudio.Wave;

//string basePath = @"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\SOUNDS";
//string[] files = Directory.GetFiles(basePath, "*.wav", SearchOption.AllDirectories);

//foreach (string file in files)
//{
//    ConvertAdpcmToPcm(file, Path.Combine(@"C:\Users\Shiro\Desktop\Programs\Mod Organizer\FIX\FixSounds", file.Substring(basePath.Length + 1)));
//}



//static void ConvertAdpcmToPcm(string inputPath, string outputPath)
//{
//    try
//    {
//        // Чтение ADPCM файла
//        using (var reader = new WaveFileReader(inputPath))
//        {
//            // Проверяем формат
//            if (reader.WaveFormat.Encoding != WaveFormatEncoding.Adpcm)
//            {
//                Console.WriteLine("Файл не в формате ADPCM");
//                return;
//            }

//            Console.WriteLine($"Исходный формат: {reader.WaveFormat}");

//            // Конвертируем в PCM
//            using (var conversionStream = new WaveFormatConversionStream(
//                new WaveFormat(reader.WaveFormat.SampleRate, 16, reader.WaveFormat.Channels),
//                reader))
//            {
//                var dir = Path.GetDirectoryName(outputPath);
//                if (!Directory.Exists(dir))
//                {
//                    Directory.CreateDirectory(dir);
//                }

//                // Сохраняем как PCM WAV
//                WaveFileWriter.CreateWaveFile(outputPath, conversionStream);
//            }

//            Console.WriteLine($"Конвертация завершена: {outputPath}");
//        }
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Ошибка: {ex.Message}");
//    }
//}