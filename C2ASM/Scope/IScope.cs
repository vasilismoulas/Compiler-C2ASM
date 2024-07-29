using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C2ASM
{
    public interface IScope // Elements scope management by SymbolTable 
    {
        public String getScopeName(); // do I have a name?
        public IScope getEnclosingScope(); // am I nested in another?
        public void define(ASTElement sym); // define sym in this scope
        public ASTElement resolve(String name); // look up name in scope
    }

}
