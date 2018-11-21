using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SportFixtures.Data.Entities;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.FixtureGenerator;
using SportFixtures.Exceptions.FixtureSelectorExceptions;
using SportFixtures.Data.Enums;

namespace SportFixtures.BusinessLogic.Implementations
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

        public ICollection<string> GetAlgorithmNames(){
            LoadAlgorithms();
            ICollection<string> algorithms = new List<string>();
            foreach(Type type in implementations){
                algorithms.Add(type.Name);
            }
            return algorithms;
        }

        public void CreateInstance(string name, IEncounterBusinessLogic encounterBL)
        {
            if(implementations.Count() == 0)
            {
                throw new ThereAreNoAlgorithmsException();
            }
            Type fixtureToInstance = implementations.FirstOrDefault(f => f.Name.Equals(name));
            if(fixtureToInstance is null)
            {
                throw new AlgorithmDoesNotExistException();
            }
            this.fixtureGenerator = (IFixtureGenerator)Activator.CreateInstance(fixtureToInstance, new object[] { encounterBL });
        }

        private void LoadAlgorithms()
        {
            implementations.Clear();
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
                            implementations.Add(type);
                        }
                    }
                }
            }
            catch (Exception)
            {
              
            }
        }

        private List<string> GetAlgorithmsByEncounterMode(List<Type> implementations, EncounterMode encounterMode)
        {
            List<string> types = new List<string>();

            foreach (Type type in implementations)
                if(type.GetProperty("EncounterMode").Equals(encounterMode))
                {
                    types.Add(type.Name);
                }

            return types;
        }
    }
}
