using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json.Linq;

namespace NFAuthenicationKeyCli
{
    class Program
    {
        private const string OUTPUT_FILE = "NFAuthentication.key";

        static void Main(string[] args)
        {
            var cmd = new CommandLineApplication();
            var file = cmd.Option("-f | --file <value>", "Netscape HTTP Cookie File", CommandOptionType.SingleValue);

            cmd.OnExecute(() =>
            {
                Console.WriteLine("Cookies file: " + file.Value());

                var cookies = new List<Cookie>();
                foreach (var line in File.ReadLines(file.Value()))
                {
                    if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
                        continue;

                    var values = line.Split("\t");
                    if (values.Length != 7)
                        throw new InvalidDataException($"Invalid cookie at line: {line}");

                    cookies.Add(new Cookie(values));
                }

                var pin = GenerateKeyFile(JArray.FromObject(cookies));

                Console.WriteLine($"Generated: {OUTPUT_FILE}, PIN: {pin}");

                return 0;             
            });

            cmd.HelpOption("-? | -h | --help");
            cmd.Execute(args);
        }

        private static string GenerateKeyFile(JArray cookies)
        {
            // Get all cookies
            Helper.AssertCookies(cookies);

            // Generate a random PIN for access to "NFAuthentication.key" file
            string pin = new Random().Next(1000, 9999).ToString();

            // Create file data structure
            JObject data_content = new JObject();
            data_content["cookies"] = cookies;
            JObject data = new JObject();
            data["app_name"] = "NFAuthenticationKey";
            data["app_version"] = "1.2.0.0";
            data["app_system"] = "Windows";
            data["app_author"] = "CastagnaIT";
            data["timestamp"] = (int)DateTime.UtcNow.AddDays(5).Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            data["data"] = data_content;

            // Save the "NFAuthentication.key" file
            Helper.SaveData(data, pin, OUTPUT_FILE);

            return pin;
        }
    }
}
