using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UI;
using UnityEngine;

namespace Data
{
    public static class Save
    {
        public static void SaveData(PlayerStats player)
        {
            var formatter = new BinaryFormatter();
            var path = Application.persistentDataPath + $"{player.name}.dat";
            var stream = new FileStream(path, FileMode.Create);

            var data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PlayerData LoadPlayer(PlayerStats player)
        {
            var path = Application.persistentDataPath + $"{player.name}.dat";
            if (File.Exists(path))
            {
                var formatter = new BinaryFormatter();
                var stream = new FileStream(path, FileMode.Open);
                var data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();

                return data;
            }

            Debug.LogWarning("File doesn't exist");
            return null;
        }
    }
}