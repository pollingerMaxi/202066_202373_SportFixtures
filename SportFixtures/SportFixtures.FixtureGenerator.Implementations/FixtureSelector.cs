using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using SportFixtures.Data.Entities;
using SportFixtures.Data;

namespace SportFixtures.FixtureGenerator.Implementations
{
    public class FixtureSelector : IFixtureSelector
    {
        private List<Type> implementations;
        private IFixtureGenerator fixtureGenerator;
        private string assemblyPath;

        public FixtureSelector()
        {
            assemblyPath = AppDomain.CurrentDomain.BaseDirectory + "Algorithms";
            implementations = new List<Type>();
            LoadAlgorithms();
        }

        public ICollection<Encounter> GenerateFixture(IEnumerable<Team> teams, DateTime date)
        {
            if (fixtureGenerator == null)
                throw new Exception();

            return fixtureGenerator.GenerateFixture(teams, date);
        }

        public ICollection<string> GetAlgorithmNamesByEncounterMode(EncounterMode encounterMode){
            ICollection<string> algorithms = new List<string>();
            foreach(Type type in implementations){
                if(type.GetProperty("EncounterMode").Equals(encounterMode)){
                    algorithms.Add(type.Name);
                }
            }
            return algorithms;
        }

        private void CreateInstance(string name)
        {
            Type fixtureToInstance = implementations.FirstOrDefault(f => f.Name.Equals(name));
            this.fixtureGenerator = (IFixtureGenerator)Activator.CreateInstance(fixtureToInstance);
        }

        private void LoadAlgorithms()
        {
            try
            {
                DirectoryInfo assembliesDirectory = new DirectoryInfo(assemblyPath);
                FileInfo[] assemblies = assembliesDirectory.GetFiles("*.dll");

                foreach (var dll in assemblies)
                {
                    AssemblyName assemblyName = AssemblyName.GetAssemblyName(dll.FullName);
                    Assembly assem = Assembly.Load(assemblyName);
                    Type[] typesInAssembly = assem.GetTypes();

                    foreach (var type in typesInAssembly)
                    {
                        if (typeof(IFixtureGenerator).IsAssignableFrom(type))
                        {
                            //IFixtureGenerator algorithm = (IFixtureGenerator)Activator.CreateInstance(type);
                            implementations.Add(type);
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        private List<string> GetAlgorithms(List<Type> implementations, EncounterMode encounterMode)
        {
            List<string> types = new List<string>();

            foreach (Type type in implementations)
                // if(type.GetProperty("EncounterMode").Equals(encounterMode))
                // {
                    types.Add(type.Name);
                // }

            return types;
        }
    }
}
