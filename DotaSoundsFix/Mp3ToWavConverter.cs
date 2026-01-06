using NAudio.Wave;

public class Mp3ToWavConverter
{
    public static void ConvertMp3ToWav(string mp3FilePath, string wavFilePath)
    {
        try
        {
            using (var reader = new Mp3FileReader(mp3FilePath))
            {
                // Конвертируем в стандартный PCM формат
                WaveFileWriter.CreateWaveFile(wavFilePath, reader);
            }

            Console.WriteLine($"Convert: {wavFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error convert: {ex.Message}");
        }
    }
}