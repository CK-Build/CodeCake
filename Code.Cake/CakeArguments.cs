using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;

namespace CodeCake
{
    internal sealed class CakeArguments : ICakeArguments
    {
        private readonly Dictionary<string, string[]> _arguments;

        public CakeArguments( IDictionary<string, string> arguments )
        {
            _arguments = new Dictionary<string, string[]>( StringComparer.OrdinalIgnoreCase );
            foreach( var kv in arguments )
            {
                _arguments.Add( kv.Key, new[] { kv.Key } );
            }
        }

        public bool HasArgument( string name )
        {
            return _arguments.ContainsKey( name );
        }

        public ICollection<string> GetArguments( string name )
        {
            return _arguments.TryGetValue( name, out var a ) ? a : Array.Empty<string>();
        }
    }

}
