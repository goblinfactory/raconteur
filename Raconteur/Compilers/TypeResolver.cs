using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Raconteur.Helpers;

namespace Raconteur.Compilers
{
    public interface TypeResolver
    {
        Type TypeOf(string Name, string AssemblyName);
    }

    public class TypeResolverClass : TypeResolver
    {
        public Type TypeOf(string Name, string AssemblyName)
        {
            if (Name.IsEmpty() || AssemblyName.IsEmpty()) return null;

//            System.Diagnostics.Debugger.Launch();

            InitAssemblyPath(AssemblyName);

            return
                (from Type in Load(AssemblyName).GetTypes()
                where Type.Name == Name
                select Type).FirstOrDefault();
        }

        string DefaultPath;
        string AssemblyPath;
        void InitAssemblyPath(string Assembly)
        {
            try
            {
                DefaultPath = DefaultPath ?? Path.GetDirectoryName(Assembly);
                AssemblyPath = Path.GetDirectoryName(Assembly);
            } 
            catch { AssemblyPath = DefaultPath; }
        }

        Assembly Load(string AssemblyName)
        {
            var Name = Path.GetFileNameWithoutExtension(AssemblyName);

            AppDomain.CurrentDomain.AssemblyResolve += LoadFromFile;

            try
            {
                return Assembly.Load(Name);
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= LoadFromFile;
            }
        }

        Assembly LoadFromFile(object Sender, ResolveEventArgs Args)
        {
            var Parts = Args.Name.Split(',');
            var FileName = Path.Combine(AssemblyPath, Parts[0].Trim() + ".dll");

            try
            {
                return Assembly.Load(File.ReadAllBytes(FileName));
            }
            catch { return null; }
        }
    }    
}