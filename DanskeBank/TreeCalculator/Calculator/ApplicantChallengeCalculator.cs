using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeCalculator.Data;
using TreeCalculator.Interfaces;

namespace TreeCalculator.Calculator
{
    /// <summary>
    /// In search for a better name, let us just call this the applicant challenge.
    /// </summary>
    public class ApplicantChallengeCalculator : ICalculator
    {
        /// <summary>
        /// Keeps the intermidiate or final result relevant to a particular node.
        /// </summary>
        private class ResultPerNode
        {
            internal Guid NodeId { get; set; }
            internal string PathSoFar { get; set; }
            internal int TotalSumSoFar { get; set; }

            public override string ToString()
            {
                return "Path: " + PathSoFar + ", Sum: " + TotalSumSoFar;
            }
        }

        private static bool IsOdd(int v)
        {
            return v % 2 == 1;
        }

        private bool TryGetValueFromParent(
            TreeNode node,
            TreeNode parentOrNull,
            Dictionary<Guid, ResultPerNode> resultPerNode,
            out ResultPerNode resultFromParent)
        {
            if (parentOrNull == null ||
                IsOdd(node.Value) == IsOdd(parentOrNull.Value) ||
                !resultPerNode.TryGetValue(parentOrNull.Id, out resultFromParent))
            {
                resultFromParent = null;
                return false;
            }
            return true;
        }

        Dictionary<string, CalculationResult> ICalculator.Calculator(TreeStructure treeStructure)
        {
            Dictionary<string, CalculationResult> result = new Dictionary<string, CalculationResult>();

            // Register intermidiate results in this dictionary (keyed by node id).
            // I could have stored this result with the actual tree nodes, but didn't want to "polute" the
            // tree node class with data that is only relevant to this calculation. Hence put it in a local
            // dictionary.
            Dictionary<Guid, ResultPerNode> resultsByNodeIds = new Dictionary<Guid, ResultPerNode>();

            // Iterate through the tree structure, starting at the root node.
            // Iterate through each lines and determine suitable candidates.
            for (int i = 0; i < treeStructure.Lines.Count; i++)
            {
                var line = treeStructure.Lines[i];

                foreach (var node in line)
                {
                    if (node.Parent1OrNull == null && node.Parent2OrNull == null)
                    {
                        // If no parent, then we must be the root
                        ResultPerNode rpn = new ResultPerNode()
                        {
                            NodeId = node.Id,
                            TotalSumSoFar = node.Value,
                            PathSoFar = node.Value.ToString()
                        };
                        resultsByNodeIds[rpn.NodeId] = rpn;
                    }
                    else
                    {
                        bool didGetValueFromFirstParent = TryGetValueFromParent(node, node.Parent1OrNull, resultsByNodeIds, out var resultFromFirstParent);
                        bool didGetValueFromSecondParent = TryGetValueFromParent(node, node.Parent2OrNull, resultsByNodeIds, out var resultFromSecondParent);
                        ResultPerNode previousResultPerNode = null;

                        if (didGetValueFromFirstParent && didGetValueFromSecondParent)
                        {
                            // There is a good result from both parents, so pick the one with the greatets total value so far.
                            previousResultPerNode = resultFromFirstParent.TotalSumSoFar > resultFromSecondParent.TotalSumSoFar ? resultFromFirstParent : resultFromSecondParent;
                        }
                        else if (didGetValueFromFirstParent)
                        {
                            previousResultPerNode = resultFromFirstParent;
                        }
                        else if (didGetValueFromSecondParent)
                        {
                            previousResultPerNode = resultFromSecondParent;
                        }

                        // This is a good candidate, so let us build a result for this one.
                        if (previousResultPerNode != null)
                        {
                            ResultPerNode rpn = new ResultPerNode()
                            {
                                NodeId = node.Id,
                                TotalSumSoFar = node.Value + previousResultPerNode.TotalSumSoFar,
                                PathSoFar = previousResultPerNode.PathSoFar + " " + node.Value
                            };
                            resultsByNodeIds[rpn.NodeId] = rpn;
                        }
                    }
                }
            }

            // If there are any results, then it should be found in the last line with the greatest value of TotalSumSoFar.
            ResultPerNode bestCandidate = null;
            foreach (var treeNode in treeStructure.Lines[treeStructure.Lines.Count - 1])
            {
                if (resultsByNodeIds.TryGetValue(treeNode.Id, out var candidate))
                {
#if DEBUG
                    System.Console.WriteLine("Final candidate: " + candidate);
#endif
                    if (bestCandidate == null) bestCandidate = candidate;
                    else if (candidate.TotalSumSoFar > bestCandidate.TotalSumSoFar) bestCandidate = candidate;
                }
            }

            if (bestCandidate != null)
            {
                result.Add("Max sum", new CalculationResultInt("Max sum", bestCandidate.TotalSumSoFar));
                result.Add("Path", new CalculationResultString("Path", bestCandidate.PathSoFar));
            }

            return result;
        }
    }
}