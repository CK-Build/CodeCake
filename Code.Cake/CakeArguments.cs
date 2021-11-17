using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;

namespace CodeCake
{
    public sealed class CakeArguments : ICakeArguments
    {
        private readonly Dictionary<string, List<string>> _arguments;

        public CakeArguments( IDictionary<string, string> arguments )
        {
            _arguments = new Dictionary<string, List<string>>( StringComparer.OrdinalIgnoreCase );
            foreach( var a in arguments )
            {
                _arguments[a.Key] = new List<string>() { a.Value };
            }
        }

        /// <inheritdoc/>
        public bool HasArgument( string name )
        {
            return _arguments.ContainsKey( name );
        }

        /// <inheritdoc/>
        public ICollection<string> GetArguments( string name )
        {
            _arguments.TryGetValue( name, out var arguments );
            return arguments ?? (ICollection<string>)Array.Empty<string>();
        }

        /// <inheritdoc/>
        public IDictionary<string, ICollection<string>> GetArguments()
        {
            var arguments = _arguments
                .ToDictionary( x => x.Key, x => (ICollection<string>)x.Value.ToList() );

            return arguments;
        }
    }
}
