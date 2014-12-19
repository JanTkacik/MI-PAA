using System;
using System.Collections;
using W3SAT.Model;

namespace W3SAT.Solvers
{
    interface IW3SATSolver
    {
        Tuple<int, BitArray> Solve(Formula problem);
    }
}
