using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeCalculator.Data
{

    /// <summary>
    /// Represents a tree structure with a root on the top and an level of children.
    /// </summary>
    public class TreeStructure
    {
        /// <summary>
        /// The root node of the tree
        /// </summary>
        public TreeNode RootNode { get; private set; }

        /// <summary>
        /// Lines of horizontal lines in the tree. The first line will contain the root node, and then the two children of the parent will follow, etc.
        /// </summary>
        public List<List<TreeNode>> Lines { get; private set; } = new List<List<TreeNode>>();

        /// <summary>
        /// Reads all data for the tree from an input file.
        /// </summary>
        /// <param name="fileName"></param>
        public void ReadTree(string fileName)
        {
            var lines = System.IO.File.ReadAllLines(fileName);
            RootNode = null;
            List<TreeNode> previousItems = null;

            for (int i = 0; i<lines.Length; i++)
            {
                var line = lines[i];
                List<TreeNode> thisItems = new List<TreeNode>();
                var items = line.Split(',');

                // Check the right number of items. Would expect for the first line to have one, and second to have two, etc.
                if (items.Length!=i+1)
                {
                    throw new Exception(string.Format("Expected for line with index {0} to have exactly {0} items, but found {1}. The line was: '{2}'", (i + 1), items.Length, line));
                }

                foreach(var item in items)
                {
                    if (!int.TryParse(item, out var val))
                    {
                        throw new Exception("Failed to convert item '" + item + "' in line " + (i+1) + " to an integer.");
                    }
                    var node = new TreeNode() { Value = val };
                    thisItems.Add(node);
                }
                if (thisItems.Count==1)
                {
                    RootNode = thisItems[0];
                }
                else
                {
                    for(int j = 0; j<previousItems.Count; j++)
                    {
                        var parent = previousItems[j];
                        var child1 = thisItems[j];
                        var child2 = thisItems[j + 1];
                        parent.Child1OrNull = child1;
                        child1.Parent2OrNull = parent;
                        parent.Child2OrNull = child2;
                        child2.Parent1OrNull = parent;
                    }
                }
                previousItems = thisItems;
                Lines.Add(thisItems);

            }
        }

    }
}
