using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FixtureEventMapper
{
    public class JSonSerializer
    {

        public static bool SaveJson(List<EventMapData> eventMapdata, string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                    serializer.Serialize(writer, eventMapdata);
            }

            return true;
        }


        public static List<EventMapData> LoadJson(string fileName)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader file = File.OpenText(fileName))
            {
                List<EventMapData> eventMapdata = (List<EventMapData>)serializer.Deserialize(file, typeof(List<EventMapData>));
                return eventMapdata;
            }           
        }
    }
}
