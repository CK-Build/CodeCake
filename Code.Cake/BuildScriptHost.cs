using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.Scripting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeCake
{
    /// <summary>
    /// The script host used to execute Cake scripts.
    /// </summary>
    sealed class BuildScriptHost : ScriptHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildScriptHost"/> class.
        /// </summary>
        /// <param name="engine">The engine.</param>
        /// <param name="context">The context.</param>
        public BuildScriptHost( ICakeEngine engine,
                                ICakeContext context ) 
            : base( engine, context )
        {
        }

        /// <summary>
        /// This can never be called.
        /// </summary>
        /// <param name="target">The target to run.</param>
        /// <returns>The resulting report.</returns>
        public override Task<CakeReport> RunTargetAsync( string target )
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// This can never be called.
        /// </summary>
        /// <param name="targets">The targets to run.</param>
        /// <returns>The resulting report.</returns>
        public override Task<CakeReport> RunTargetsAsync( IEnumerable<string> targets )
        {
            throw new NotImplementedException();
        }
    }
}
