using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Common
{
    public struct BigFiveTrait
    {
        public double Openness { get; set; }
        public double Conscientiousness { get; set; }
        public double Extraversion { get; set; }
        public double Agreeableness { get; set; }
        public double Neuroticism { get; set; }

        public static explicit operator PADTrait(BigFiveTrait b5)
        {
            PADTrait pad = new PADTrait();

            pad.Pleasure = ((0.21 * b5.Extraversion) + (0.59 * b5.Agreeableness) + (0.19 * b5.Neuroticism));
            pad.Arousal = ((0.15 * b5.Openness) + (0.30 * b5.Agreeableness) + (0.57 * b5.Neuroticism));
            pad.Dominance = ((0.25 * b5.Openness) + (0.17 * b5.Conscientiousness) + (0.60 * b5.Extraversion) + (-0.32 * b5.Agreeableness));
            return pad;
        }


        public  PADTrait ToPAD()
        {
            return (PADTrait)this;
        }
    }
}
