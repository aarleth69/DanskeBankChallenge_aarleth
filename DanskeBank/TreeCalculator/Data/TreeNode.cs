using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCalculator.Data
{

    /// <summary>
    /// Represents a node with optional parent/children.
    /// </summary>
    public class TreeNode
    {

        /// <summary>
        /// Unique id of this item.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Represents the value of a given node.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// First child node or null
        /// </summary>
        public TreeNode Child1OrNull { get; set; }

        /// <summary>
        /// Second child node or null
        /// </summary>
        public TreeNode Child2OrNull { get; set; }

        /// <summary>
        /// First parent or null
        /// </summary>
        public TreeNode Parent1OrNull { get; set; }

        /// <summary>
        /// Second parent or null
        /// </summary>
        public TreeNode Parent2OrNull { get; set; }


        public override string ToString()
        {
            return base.ToString() + ", value: " + Value + ", id: " + Id;
        }

    }
}
