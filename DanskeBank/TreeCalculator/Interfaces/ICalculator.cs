using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeCalculator.Data;

namespace TreeCalculator.Interfaces
{

    /// <summary>
    /// interface for a calculator. In financial applications one would normally want to calculate many metrices, so
    /// this allows for a generic calculation.
    /// </summary>
    public interface ICalculator
    {
        /// <summary>
        /// Does some calculation on the tree structure.
        /// </summary>
        /// <param name="treeStructure"></param>
        /// <returns></returns>
        Dictionary<string, CalculationResult> Calculator(TreeStructure treeStructure);
    }
}
