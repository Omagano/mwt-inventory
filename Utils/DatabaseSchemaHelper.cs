using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace InventorySystem.Utils
{
    public static class DatabaseSchemaHelper
    {
        public static void ListIndexes(string connectionString, string tableName)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"PRAGMA index_list('{tableName}');";

            using var reader = command.ExecuteReader();
            Console.WriteLine($"Indexes on table {tableName}:");
            while (reader.Read())
            {
                var indexName = reader.GetString(1);
                var unique = reader.GetBoolean(2);
                Console.WriteLine($"- {indexName} (Unique: {unique})");
            }
        }

        public static void DropIndex(string connectionString, string indexName)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = $"DROP INDEX IF EXISTS \"{indexName}\";";
            command.ExecuteNonQuery();

            Console.WriteLine($"Index {indexName} dropped if it existed.");
        }
    }
}
