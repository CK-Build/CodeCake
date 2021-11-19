using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCake
{
    /// <summary>
    /// Describes a Build class.
    /// </summary>
    public class CodeCakeBuildTypeDescriptor
    {
        readonly Type _type;

        /// <summary>
        /// initializes a new <see cref="CodeCakeBuildTypeDescriptor"/>.
        /// </summary>
        /// <param name="t">The type of the build object.</param>
        internal CodeCakeBuildTypeDescriptor( Type t )
        {
            _type = t;
        }

        /// <summary>
        /// Gets the type of the build object.
        /// </summary>
        public Type Type => _type;
    }
}
