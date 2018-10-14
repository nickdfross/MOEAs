using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MOEAPlat.Encoding;

namespace MOEAPlat.Problems
{
    public class F4 : AbstractMOP
    {
        private static F4 instance;
        private F4(int pd)
        {
            this.parDimension = pd;
            Init();
        }
        public override void Evaluate(MoChromosome chromosome)
        {
            double[] sp = chromosome.realGenes;
            double[] obj = chromosome.objectivesValue;

            for (int i = 0; i < this.parDimension; i++)
                sp[i] = domain[i, 0] + sp[i] * (domain[i, 1] - domain[i, 0]); 

            double g = 0;
		    for(int i = 3;i < this.parDimension;i++) g += Math.Pow(sp[i] - 2, 2);
		    obj[0] = (1 + g)*sp[0]/Math.Sqrt(sp[1] * sp[2]);
		    obj[1] = (1 + g)*sp[1]/Math.Sqrt(sp[0] * sp[2]);
		    obj[2] = (1 + g)*sp[2]/Math.Sqrt(sp[0] * sp[1]);

            for (int i = 0; i < this.parDimension; i++)
                sp[i] = (sp[i] - domain[i,0]) / (domain[i, 1] - domain[i, 0]);
        }

        public override void Init()
        {
            this.domain = new double[this.parDimension,2];
            for (int i = 0; i < parDimension; i++)
            {
                domain[i,0] = 1;
                domain[i,1] = 4;
            }
            this.objDimension = 3;
            this.range = new double[objDimension,2];
        }

        public static F4 GetInstance(int pd)
        {
            if (instance == null)
            {
                instance = new F4(pd);
                instance.name = "F4";
            }
            return instance;
        }
    }
}
