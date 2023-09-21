using ArchLab1Lib.Model;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1.Model
{
    internal class CsvRepository<E, K> : Repository<E, K> where E : IEntity<K>
    {
        private readonly String _csvPath;

        public CsvRepository(String path)
        {
            _csvPath = path;
        }

        private void writeAll(IEnumerable<E> records)
        {
            using (var writer = new StreamWriter(_csvPath))
            using (var csvWriter = new CsvWriter(writer, System.Globalization.CultureInfo.InvariantCulture))
            {                
                csvWriter.WriteRecords(records);
            }
        }

        public void Create(E entity)
        {
            var records = ReadAll();
            records.Add(entity);
            writeAll(records);
        }

        public void DeleteById(K id)
        {
            var records = ReadAll();
            records.RemoveAll(rec => rec?.Id?.Equals(id) ?? false);
            writeAll(records);
        }

        public List<E> ReadAll()
        {
            using (var reader = new StreamReader(_csvPath))
            using (var csvReader = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                return csvReader.GetRecords<E>().ToList();
            }
        }

        public E? ReadById(K id)
        {
            var records = this.ReadAll();
            E? result;
            try
            {
                result = records
                .First(rec =>
                id?.Equals(rec.Id) ?? false);
            }
            catch (InvalidOperationException) {
                return default(E);
            }
            return result;
        }

        public bool ExistsById(K id)
        {
            return ReadAll().Any(task => task?.Id?.Equals(id) ?? false);
        }

        public void Update(E entity)
        {
            var records = ReadAll();
            var toUpdate = records.Find(rec => entity?.Id?.Equals(rec.Id) ?? false);
            toUpdate?.Update(entity);
            writeAll(records);
        }
    }
}
