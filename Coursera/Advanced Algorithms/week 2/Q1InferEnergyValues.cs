﻿using System;
using System.Linq;

namespace A9 {
    public class Q1InferEnergyValues {

        static void Main(string[] args) {
            long n = Convert.ToInt64(Console.ReadLine());
            double[,] matrix = new double[n, n + 1];
            for (int i = 0; i < n; i++) {
                long[] nums = Console.ReadLine().Split().Select(d => Convert.ToInt64(d)).ToArray();
                for (int j = 0; j < n + 1; j++) {
                    matrix[i, j] = nums[j];
                }
            }
            Solve(n, matrix).ToList().ForEach(d => Console.Write(d + " "));
        }
        public static double[] Solve(long n, double[,] matrix) {
            bool[] mark = new bool[matrix.GetLength(0)]; //lodum satr ha 1 shodan

            long[] order = new long[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(1) - 1; ++i) { // sefr kardan e sotun i om
                for (int j = 0; j < matrix.GetLength(0); ++j) { // peyda kardan satr qeir e sefr
                    if (!mark[j] && matrix[j, i] != 0) {
                        mark[j] = true;
                        order[i] = j;
                        double temp = matrix[j, i];
                        for (int k = i; k < matrix.GetLength(1); ++k) {
                            matrix[j, k] /= temp;
                        }
                        for (int k = 0; k < matrix.GetLength(0); ++k) {
                            if (k == j) continue;
                            double mazrab = matrix[k, i] / matrix[j, i];
                            for (int l = i; l < matrix.GetLength(1); ++l) {
                                matrix[k, l] -= matrix[j, l] * mazrab;
                            }
                        }
                        break;
                    }
                }
            }

            double[] ans = new double[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); ++i) {
                double t = matrix[order[i], matrix.GetLength(1) - 1];
                double abst = Math.Abs(t);
                //if (abst - ((int)abst) < .25) abst = (int)abst;
                //else if (((int)abst + 1) - abst < .25) abst = (int)abst + 1;
                //else abst = (int)abst + .5;
                if (Math.Abs(t) == 0) ans[i] = 0;
                else ans[i] = (t / Math.Abs(t)) * abst;
                if (ans[i] == -0) ans[i] = 0;
            }
            return ans;
        }
    }
}
