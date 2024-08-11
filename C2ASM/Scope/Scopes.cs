using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM.Scopes
{
    public abstract class Scope 
    {
        public string scope { get; set; }

        public override bool Equals(Object node)
        {
            String scope1 = this.scope;
            String scope2 = ((Scope)node).scope;

            if (this == null || node == null)
                return false;
            else if (this.scope == ((Scope)node).scope)
            {

                return true;
            }
            else
                return false;
            //else if (node1.GetElementScopeName() == node2.GetElementScopeName() && node1.GetElementScope(). == node2.GetElementScopeName())
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

    }

    // Scopes are static so the corresponding classes will be "immutable"
    public class GlobalScope : Scope
    {
        public GlobalScope()
        {
            this.scope = "global";
        }
    }

    public class LocalScope : Scope
    {
        public ASTElement element { get; }

        public LocalScope(string scope)
        {
            base.scope = scope;
        }
        
    }
}
