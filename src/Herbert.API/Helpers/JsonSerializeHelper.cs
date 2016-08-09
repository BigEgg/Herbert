namespace Herbert.API.Helpers
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The helper class for JSON Serialize/Deserialize extensions
    /// </summary>
    public static class JsonSerializeHelper
    {
        private static readonly JsonSerializerSettings serializerSettings;

        /// <summary>
        /// Initializes the <see cref="JsonSerializeHelper"/> class.
        /// </summary>
        static JsonSerializeHelper()
        {
            serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;
        }


        /// <summary>
        /// Deserializes the JSON to the specified .NET type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="jsonString">The JSON string to deserialize.</param>
        /// <returns>The deserialized object from the JSON string</returns>
        public static T Deserialize<T>(this string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString)) { return default(T); }

            try
            {
                return JsonConvert.DeserializeObject<T>(jsonString, serializerSettings);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Serializes the specified object to a JSON string
        /// </summary>
        /// <typeparam name="T">The type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string Serialize<T>(this T data)
        {
            if (data == null) { return "{}"; }

            return JsonConvert.SerializeObject(data, serializerSettings);
        }
    }
}
