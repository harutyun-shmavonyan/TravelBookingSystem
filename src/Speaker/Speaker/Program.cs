using System;
using System.Speech.Synthesis;
using NAudio.Wave;
using Vosk;

class Program
{
    static void Main(string[] args)
    {
        using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
        {
            // Configure the audio output. 
            synthesizer.SetOutputToDefaultAudioDevice();

            // Create a prompt from a string.
            PromptBuilder builder = new PromptBuilder();
            builder.AppendText(@"ChatGPT is an AI language model developed by OpenAI. It's designed to understand and generate human-like text based on the prompts it receives. ChatGPT can assist with a variety of tasks, such as answering questions, helping users learn languages, writing essays, creating stories, and even generating code.");

            synthesizer.SpeakAsync(builder);

            // Keep the console window open.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Set the model path to the directory of the Vosk model you have downloaded
        var modelPath = @"C:\Users\Harutyun\Desktop\vosk-model-en-us-0.22-lgraph";
        Vosk.Vosk.SetLogLevel(0); // Set log level to warn
        var model = new Model(modelPath);

        // Capture microphone input
        using (var waveIn = new WaveInEvent())
        using (var recognizer = new VoskRecognizer(model, waveIn.WaveFormat.SampleRate))
        {
            waveIn.DataAvailable += (sender, e) =>
            {
                // Send data to recognizer when audio data is available
                if (recognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
                {
                    // Final result
                    string result = recognizer.Result();
                    Console.WriteLine(result);
                }
            };

            waveIn.StartRecording();
            Console.WriteLine("Start speaking now...");

            // Stop recording upon pressing Enter
            Console.ReadLine();
            waveIn.StopRecording();

            // Finalize recognition and print it
            string finalResult = recognizer.FinalResult();
            Console.WriteLine(finalResult);
        }
    }
}