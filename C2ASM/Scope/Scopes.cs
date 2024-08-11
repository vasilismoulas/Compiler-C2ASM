using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Scopes
{
    public abstract class Scope // Factory
    {
        public string scope { get; }
    }

    // Scopes are static so the corresponding classes will be "immutable"
    public class GlobalScope : Scope
    {
        public string scope { get; }
        public List<ASTElement>[] m_children { get; set; }
        public ASTElement[] m_contents { get; set; }

        public GlobalScope()
        {
            this.scope = "global";
            this.m_children = null;
        }
  
    }

    public class LocalScope : Scope // Ranges from funcitons to compound statements : (e.g.: { ... } )
    {
        public string scope { get; }
        public List<ASTElement>[] m_children { get; set; }
        public ASTElement[] m_contents { get; set; }

        public LocalScope(string scope)
        {
            this.scope = scope;
            this.m_children = null;
        }
        
    }
}
