using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace hw11_Reflection.Services
{
    public class JsonDeserializer<T>
    { 
        public T Deserialize(string jsonString)
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
