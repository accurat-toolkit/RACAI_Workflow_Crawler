using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Agora.Serialization
{
    [Serializable]
    public class Serialization<Type>
    {
        public static bool Serialize(string fileName, Type instance)
        {
            bool responce = true;
            FileStream fileStream = new FileStream(fileName, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fileStream, instance);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to serialize. Reason: " + e.Message);
                responce = false;
            }
            finally
            {
                fileStream.Close();
            }
            return responce;
        }
        public static Type Deserialize(string fileName)
        {
            Type instance = default(Type);
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                instance = (Type)formatter.Deserialize(fileStream);
            }
            catch (SerializationException e)
            {
                MessageBox.Show("Failed to deserialize. Reason: " + e.Message);
                instance = default(Type);
            }
            finally
            {
                fileStream.Close();
            }
            return instance;
        }
    }
}