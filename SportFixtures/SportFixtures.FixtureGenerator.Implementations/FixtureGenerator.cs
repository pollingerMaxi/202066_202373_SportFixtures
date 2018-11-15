using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SportFixtures.Data.Entities;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class FixtureGenerator : IFixtureGenerator
    {
        private List<Type> implementations;
        private IFixtureGenerator fixtureGenerator;
        private static string[] algorithms;
        private static readonly string DOUBLE_FOLDER_PATH = AppDomain.CurrentDomain.BaseDirectory + "AlgorithmsDouble";
        private static readonly string MULTIPLE_FOLDER_PATH = AppDomain.CurrentDomain.BaseDirectory + "AlgorithmsMultiple";

        public FixtureGenerator()
        {
            implementations = new List<Type>();
            algorithms = SetAlgorithms();
        }

        public FixtureGenerator(int algorithmId) : this()
        {
            GenerateInstanceByFixtureId(algorithmId);
        }

        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date)
        {
            if (fixtureGenerator == null)
                throw new Exception();

            return fixtureGenerator.GenerateFixture(teams, date);
        }

        public string[] GetFixtureAlgorithms()
        {
            return algorithms;
        }

        public void SetFixtureGenerator(int algorithmId)
        {
            GenerateInstanceByFixtureId(algorithmId);
        }

        private void GenerateInstanceByFixtureId(int id)
        {
            if (id >= algorithms.Length || id < 0)
                throw new Exception();

            string selectedAlgorithm = algorithms[id];
            Type fixtureToInstance = implementations.FirstOrDefault(f => f.Name.Equals(selectedAlgorithm));
            fixtureGenerator = (IFixtureGenerator)Activator.CreateInstance(fixtureToInstance);
        }

        private string[] SetAlgorithms()
        {
            List<Type> doubles = GetFolderImplementations(DOUBLE_FOLDER_PATH);
            List<string> doublesAlgorithms = SetAlgorithmType(doubles);

            List<Type> multiples = GetFolderImplementations(MULTIPLE_FOLDER_PATH);
            List<string> multiplesAlgorithms = SetAlgorithmType(multiples);

            doubles.AddRange(multiples);
            implementations = doubles;
            doublesAlgorithms.AddRange(multiplesAlgorithms);

            return doublesAlgorithms.ToArray();
        }

        private List<string> SetAlgorithmType(List<Type> implementations)
        {
            List<string> algorithms = new List<string>();

            foreach (Type type in implementations)
                algorithms.Add(type.Name);

            return algorithms;
        }

        private List<Type> GetFolderImplementations(string folderPath)
        {
            List<Type> implementations = new List<Type>();
            var directory = new DirectoryInfo(folderPath);
            var dllsInDirectory = directory.GetFiles("*.dll");
            foreach (var dll in dllsInDirectory)
            {
                Assembly myAssembly = Assembly.LoadFile(dll.FullName);
                implementations.AddRange(GetTypesInAssembly<IFixtureGenerator>(myAssembly));
            }
            return implementations;
        }

        private List<Type> GetTypesInAssembly<T>(Assembly assembly)
        {
            List<Type> types = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(type))
                {
                    types.Add(type);
                }
            }
            return types;
        }
    }
}
