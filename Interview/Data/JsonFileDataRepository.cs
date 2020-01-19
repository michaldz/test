using Interview.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Interview.Data.Interfaces
{
    public class JsonFileDataRepository<TEntity> : IGenericDataRepository<TEntity> where TEntity : class, IIdable
    {
        private readonly string dataFilePath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')) + @"\data.json";

        public TEntity Add(TEntity entity)
        {
            Guid guid;
            if (!Guid.TryParse(entity.Id, out guid))
            {
                entity.Id = Guid.NewGuid().ToString();
            }
            var entities = ReadFile().ToList();
            if (!entities.Any(e => e.Id == entity.Id))
            {
                entities.Add(entity);
                WriteFile(entities);
                return Get(entity.Id);
            }
            throw new ArgumentException();
        }

        public bool Delete(string id)
        {
            var entities = ReadFile().ToList();
            if (entities.RemoveAll(e => e.Id == id) > 0)
            {
                WriteFile(entities);
                return true;
            }
            else
            {
                return false;
            }
        }

        public TEntity Get(string id)
        {
            return ReadFile().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return ReadFile();
        }

        public TEntity Update(TEntity entity)
        {
            var entities = ReadFile().ToList();
            var entityIndex = entities.FindIndex(e => e.Id == entity.Id);
            if (entityIndex == -1)
            {
                return null;
            }
            entities[entityIndex] = entity;
            WriteFile(entities);
            return Get(entity.Id);
        }

        private IEnumerable<TEntity> ReadFile()
        {
            if (File.Exists(dataFilePath))
            {
                var fileContent = File.ReadAllText(dataFilePath);
                return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(fileContent);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
        private void WriteFile(IEnumerable<TEntity> entities)
        {
            if (File.Exists(dataFilePath))
            {
                var fileContent = JsonConvert.SerializeObject(entities).ToString();
                File.WriteAllText(dataFilePath, fileContent);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}