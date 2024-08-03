using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Scopes
{
    public abstract class Scope 
    {
        public string scope { get; }
    }

    // Scopes are static so the corresponding classes will be "immutable"
    public class GlobalScope : Scope
    {
        public string scope { get; }

        public GlobalScope()
        {
            this.scope = "global";
        }
  
    }

    public class LocalScope : Scope
    {
        public string scope { get; }
        public ASTElement element { get; }

        public LocalScope(string scope)
        {
            this.scope = scope;
        }
        
    }
}
