using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Scopes
{
    public abstract class Scope
    {
        public string scope { get; protected set; }  // Change to a settable property
    }

    public class GlobalScope : Scope
    {
        public List<ASTElement>[] m_children { get; set; }
        public ASTElement[] m_contents { get; set; }

        public GlobalScope()
        {
            this.scope = "global";  // Set the scope directly
            this.m_children = null;
            this.m_contents = null; // Initialize m_contents as well if necessary
        }
    }

    public class LocalScope : Scope // Ranges from funcitons to compound statements : (e.g.: { ... } )
    {
        public List<ASTElement>[] m_children { get; set; }
        public ASTElement[] m_contents { get; set; }

        public LocalScope(string scope)
        {
            this.scope = scope;
            this.m_children = null;
        }
        
    }
}
